import { Avatar, Button, message, Modal, Select, Spin, Table } from "antd"
import { IOrder, IUser } from "../../interfaces";
import { useEffect, useState } from "react";
import { useUserContext } from "../../contexts/hooks";
import { del, Endpoint, get, update } from "../../request/AppRequest";
import { toast } from "react-toastify";
import { USER_ACTION } from "../../contexts/UserContext";
import { LoadingOutlined, UserOutlined } from "@ant-design/icons";
import moment from "moment";
import Number from "../../helper/Number";
import { useNavigate } from "react-router-dom";

const OrderManagement: React.FC = () => {
    const [orders, setOrders] = useState<IOrder[]>([])
    const [loading, setLoading] = useState(false)
    const { user, dispatchUser } = useUserContext()
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [currentOrder, setCurrentOrder] = useState<IOrder>()

    useEffect(() => {
        fetchOrders()
    }, [user])

    const columns = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            render: (text) => text
        },
        {
            title: 'Note',
            dataIndex: 'description',
            key: 'description',
            render: (text) => text || "No Note"
        },
        {
            title: 'Status',
            dataIndex: 'orderType',
            key: 'orderType',
            render: (orderType, order) => <Select
                onChange={v => handleChangeStatus(v, order)}
                defaultValue={orderType?.id}
                style={{ width: 120 }}
                options={[
                    { value: '4f235200-957f-4051-83fa-93f0b7d5e2d3', label: 'confirmed' },
                    { value: 'f7e5cbee-e5de-4a7d-b688-5e56fdf96573', label: 'pending' },
                    { value: 'afb8d8b7-4035-42aa-a157-03f56d67c314', label: 'cancelled' },
                ]}
            />
        },
        {
            title: 'BuyerId',
            dataIndex: 'buyerId',
            key: 'buyerId',
            render: (buyerId) => buyerId || "Unknown"
        },
        {
            title: 'Discount',
            dataIndex: 'voucher',
            key: 'voucher',
            render: (voucher) => voucher ? voucher?.factor * 100 + "%" : "0%"
        },
        {
            title: 'CreatedAt',
            dataIndex: 'createdOn',
            key: 'createdOn',
            render: (date) => moment(date).format("DD/MM/YYYY hh:mm:ss") || "Null"
        },
        {
            title: 'Action',
            key: 'action',
            render: (_, record) => (
                <>
                    <span className="flex gap-2">
                        <Button type="primary" onClick={() => handleShowOrder(record)}>
                            ...
                        </Button>
                        <Button type="primary" danger onClick={() => handleRemoveUser(record)}>
                            X
                        </Button>
                    </span>
                </>
            ),
        },
    ];

    const handleChangeStatus = async (value: string, order: IOrder) => {
        try {
            const { status, data } = await update({
                url: Endpoint.ORDER_URL,
                payload: {
                    "id": order?.id,
                    "orderTypeId": value
                }
            })

            if (status === 200) {
                message.success(`Change status order ${order.id} success.`)
                fetchOrders()
            } else {
                message.error(`Change status order ${order.id} failed.`)
                message.error(data)
            }
        } catch (error) {
            message.error(`Error: ${error}`)
        }
    }

    const handleShowOrder = (order: IOrder) => {
        setCurrentOrder(order)
        setIsModalOpen(true);
    };

    const handleOk = () => {
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const handleRemoveUser = async (order: IOrder) => {
        try {
            const { status, data } = await del({
                url: Endpoint.ORDER_URL + "/" + order.id
            })
            if (status === 200) {
                message.success(`Remove order ${order.id} success.`)
                fetchOrders()
            } else {
                message.error(`Remove order ${order.id} failed.`)
                message.error(data)
            }
        } catch (error) {
            message.error(`Error: ${error}`)
        }
    }

    const navigate = useNavigate()
    const fetchUser = async () => {
        try {
            const { status, data } = await get({
                url: Endpoint.PROFILE_URL
            })

            if (status === 200) {
                dispatchUser({
                    type: USER_ACTION.ADD,
                    payload: data as IUser
                })

                const { role } = data as IUser
                if (role?.name !== "Admin"){
                    navigate("/forbidden")
                }
            } else {
                toast.error(`Error: ${data}`)
            }
        } catch (error) {
            console.error(`Error: ${error}`)
        }
    }
    const fetchOrders = async () => {
        if (!user) {
            await fetchUser()
        }
        setLoading(true)
        try {
            const { status, data }: { status: number, data: IOrder[] } = await get({
                url: Endpoint.ORDER_URL
            })

            if (status === 200) {
                setOrders(data)
            } else {
                message.error(`Error: ${data}`)
            }

        } catch (error) {
            console.error(`Error: ${error}`)
        } finally {
            setTimeout(() => {
                setLoading(false)
            }, 200);
        }
    }

    const calcPrice = () => {
        let price = currentOrder?.cartItems?.reduce((p, c) => {
            if (c.isSelected) {
                return p + c?.product?.productDetail?.price * c?.quantity
            }
            return 0
        }, 0) || 0

        if (currentOrder?.voucher) {
            price = (price * (1 - currentOrder.voucher.factor))
        }
        return Number.formatPrice(price)
    }

    return <>
        <div className="w-[100%] animate-fade">
            <Modal title={"Order detail"} open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
                <p><span className="font-bold">ID:</span> <span>{currentOrder?.id}</span></p>
                <p><span className="font-bold">Note:</span> <span>{currentOrder?.description}</span></p>
                <p><span className="font-bold">Status:</span> <span>{currentOrder?.orderType?.name}</span></p>
                <p><span className="font-bold">Voucher redeemed:</span> <span>{currentOrder?.voucher?.code || "No voucher"}</span></p>
                <p><span className="font-bold">Created at:</span> <span>{moment(currentOrder?.createdOn).format("DD/MM/YYYY hh:mm:ss")}</span></p>
                <p><span className="font-bold">Items:</span> {
                    currentOrder?.cartItems?.map(v => <span>{v?.product?.name} (x{v?.quantity})</span>)
                }</p>
                <p><span className="font-bold">Price:</span> <span>{calcPrice()}</span></p>
            </Modal>
            <p className="text-center p-5">Order (General)</p>
            <Spin spinning={loading} indicator={<LoadingOutlined />}>
                <Table dataSource={orders} columns={columns} pagination={false} />
            </Spin>
        </div>
    </>
}

export default OrderManagement