import { Button, ConfigProvider, Divider, Form, Input, InputNumber, Radio, Select, Splitter } from "antd"
import { useCartItem } from "../contexts/hooks"
import logo from "/vite.svg"
import { Link } from "react-router-dom"
import { useState } from "react";
import { ButtonWarning } from "../custom/button";

type LayoutType = Parameters<typeof Form>[0]['layout'];

const CheckoutList: React.FC = () => {
    const { cartItems, dispatchCartItems } = useCartItem()

    const [form] = Form.useForm();
    const [formLayout, setFormLayout] = useState<LayoutType>('horizontal');

    const onFormLayoutChange = ({ layout }: { layout: LayoutType }) => {
        setFormLayout(layout);
    };

    return (
        <>
            <div className="Wrapper bg-white text-black shadow-xl p-5 rounded-md w-[60%] animate-fade">
                <p className="text-center">Checkout</p>

                <Divider orientation="left" style={{ borderColor: '#000000' }} plain>Information</Divider>

                <Form
                    layout='vertical'
                    form={form}
                    initialValues={{ layout: formLayout }}
                    onValuesChange={onFormLayoutChange}

                >
                    <Form.Item label="Email" required>
                        <Input type="email" placeholder="input placeholder" />
                    </Form.Item>
                    <Form.Item label="Phone" required>
                        <InputNumber placeholder="Example: 0123456789" style={{ width: "100%" }} />
                    </Form.Item>
                    <Form.Item label="Address" required>
                        <Input placeholder="input placeholder" />
                    </Form.Item>
                    <Form.Item label="Delivery Method" required>
                        <Select>
                            <Select.Option value="pickup">Pick up at store</Select.Option>
                            <Select.Option value="cod">COD</Select.Option>
                        </Select>
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
                    cartItems.map((cartItem) => cartItem.isChecked && <>
                        <div className="flex w-[100%] px-5 py-2 mb-2 text-1xl-black text-black bg-[#ffffff] shadow-xl">
                            <span className="w-[40%] flex gap-2 items-center">
                                <img className="w-[60px] h-[60px]" src={cartItem.thumbnail || logo} />
                                <span>
                                    {cartItem.name} <br />
                                    (<span className="text-sm text-red-500">{cartItem.option}</span>)
                                </span>

                            </span>
                            <span className="w-[30%] flex justify-center items-center relative"><p>{cartItem.price}</p></span>
                            <span className="w-[30%] flex justify-center items-center">
                                <InputNumber disabled min={0} max={cartItem.stock} defaultValue={cartItem.quantity} />
                            </span>
                            <span className="w-[10%] flex justify-center items-center">{cartItem.quantity * cartItem.price}</span>
                        </div>
                    </>)
                }

                <span className="flex text-white rounded-sm justify-between items-center sticky bottom-0 gap-1 bg-[#3b82f6] p-5">
                    <p className="text-1xl">
                        Total Price: <span>
                            {cartItems.reduce((p, c) => {
                                if (c.isChecked) {
                                    return p + c.price * c.quantity
                                }
                                return 0
                            }, 0)}
                        </span>
                    </p>
                    <span className="self-end flex items-center gap-2 justify-between">
                        <Link className="" to="/cart"><Button type="primary" danger>Back</Button></Link>
                        <Link className="sticky bottom-0" to="/payment"><ButtonWarning type="primary">Continue</ButtonWarning></Link>
                    </span>
                </span>
            </div>
        </>
    )
}

export default CheckoutList