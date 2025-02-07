import { Button, Divider, Form, Input, InputNumber, message, Select, Spin } from "antd"
import { useCartItem, useCheckoutContext, useUserContext } from "../contexts/hooks"
import { Link, useNavigate } from "react-router-dom"
import { useEffect, useState } from "react";
import { ButtonWarning } from "../custom/button";
import Number from "../helper/Number";
import { Endpoint, get } from "../request/AppRequest";
import { useDebounce } from "../custom/hooks";
import { IVoucher } from "../interfaces";
import { LoadingOutlined } from "@ant-design/icons";
import moment from "moment";
import CheckoutItem from "./CheckoutItem";

type LayoutType = Parameters<typeof Form>[0]['layout'];

const CheckoutList: React.FC = () => {
    const { checkoutItems } = useCheckoutContext()

    const [form] = Form.useForm();
    const [formLayout, setFormLayout] = useState<LayoutType>('horizontal');
    const { user } = useUserContext()
    const [discount, setDiscount] = useState()
    const [discountLoading, setDiscountLoading] = useState(false)
    const [voucher, setVoucher] = useState<IVoucher>()
    const discountDebounced = useDebounce(discount, 1000)
    const navigate = useNavigate()

    const onFormLayoutChange = ({ layout }: { layout: LayoutType }) => {
        setFormLayout(layout);
    };

    const handleChangeDiscount = (e: Event) => {
        setDiscount(e?.target?.value)
    }

    useEffect(() => {
        fetchVoucher()
    }, [discountDebounced])

    const fetchVoucher = async () => {
        if (!discountDebounced) {
            setVoucher(undefined)
            return
        }
        setDiscountLoading(true)
        try {
            const { status, data }: { status: number, data: IVoucher } = await get({
                url: Endpoint.CHECK_VOUCHER_URL + "/" + discountDebounced
            })

            if (status === 200) {  
                if (data.expiredAt && moment(data.expiredAt).valueOf() > moment.now() && !data.isActive){
                    message.success("Voucher valid.")
                    setVoucher(data)
                }else{
                    message.error("Voucher invalid.")
                    setVoucher(undefined)
                }
            } else {
                message.error(`Error: ${data}`)
                setVoucher(undefined)
            }

        } catch (error) {
            message.error("Voucher invalid.")
            console.error(`Error: ${error}`)
            setVoucher(undefined)
        } finally {
            setTimeout(() => {
                setDiscountLoading(false)
            }, 200);
        }
    }

    const handleNext = ()=>{
        if (voucher){
            navigate(`/order/${voucher.id}`)
        }else{
            navigate('/order')
        }
    }

    return (
        <>
            {!user && <p className="text-black">You need to login to see this.</p> || <div className="Wrapper bg-white text-black shadow-xl p-5 rounded-md w-[60%] animate-fade">
                <p className="text-center">Checkout</p>

                <Divider orientation="left" style={{ borderColor: '#000000' }} plain>Information</Divider>

                <Form
                    layout='vertical'
                    form={form}
                    initialValues={{ layout: formLayout }}
                    onValuesChange={onFormLayoutChange}

                >
                    <Form.Item label="Phone" required>
                        <Input disabled type="number" placeholder="Ex: 0123456789" style={{ width: "100%" }} value={user?.phone} />
                    </Form.Item>
                    <Form.Item label="Address" required>
                        <Input disabled placeholder="Ex: 121a, ..." value={user?.address} />
                    </Form.Item>
                    <Form.Item label="Delivery Method" required>
                        <Select defaultValue={1} disabled>
                            <Select.Option key={1} value={1}>Pick up at store</Select.Option>
                            <Select.Option key={2} value={2}>COD</Select.Option>
                        </Select>
                    </Form.Item>
                    <Form.Item label="Discount">
                        <Input loading={true} onChange={handleChangeDiscount} suffix={discountLoading && <Spin indicator={<LoadingOutlined spin />} size="small" />} placeholder="Ex: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" />
                    </Form.Item>
                </Form>

                <Divider orientation="left" style={{ borderColor: '#000000' }} plain>Detail</Divider>
                <div className="flex text-white bg-[#3b82f6] w-[100%] px-5 py-2 mb-2 text-[0.8rem] shadow-xl">
                    <span className="w-[40%]">Product</span>
                    <span className="w-[30%] text-center">Unit Price</span>
                    <span className="w-[30%] text-center">Quantity</span>
                    <span className="w-[10%] text-center">Total Price</span>
                </div>
                {
                    checkoutItems.map((checkoutItem) => <CheckoutItem key={checkoutItem.id} checkoutItem={checkoutItem} />)
                }

                <span className="flex text-white rounded-sm justify-between items-center sticky bottom-0 gap-1 bg-[#3b82f6] p-5">
                    <p className="text-1xl">
                        Total Price: {
                            voucher && (
                                <span className="text-yellow-400">
                                    {
                                        Number.formatPrice(checkoutItems.reduce((p, c) => {
                                            if (c.isSelected) {
                                                return p + c.product.productDetail.price * c.quantity
                                            }
                                            return 0
                                        }, 0) * (1- voucher?.factor))
                                    }
                                    ({- voucher?.factor * 100}%)
                                </span>
                            ) || (
                                <span>
                                    {Number.formatPrice(checkoutItems.reduce((p, c) => {
                                        if (c.isSelected) {
                                            return p + c.product.productDetail.price * c.quantity
                                        }
                                        return 0
                                    }, 0))}
                                </span>
                            )
                        }
                    </p>
                    <span className="self-end flex items-center gap-2 justify-between">
                        <Link className="" to="/cart"><Button type="primary" danger>Back</Button></Link>
                        <ButtonWarning type="primary" onClick={handleNext}>Next</ButtonWarning>
                    </span>
                </span>
            </div>}
        </>
    )
}

export default CheckoutList