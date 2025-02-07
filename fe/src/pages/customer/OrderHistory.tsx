import { useEffect, useState } from "react"
import { IOrder, IUser } from "../../interfaces"
import { Button, message, Modal, Space, Spin, Table, TableProps, Tag } from "antd"
import { Endpoint, get } from "../../request/AppRequest"
import { useUserContext } from "../../contexts/hooks"
import { LoadingOutlined } from "@ant-design/icons"
import moment from "moment"
import { USER_ACTION } from "../../contexts/UserContext"
import { toast } from "react-toastify"
import Number from "../../helper/Number"


const OrderHistory: React.FC = () => {
  const [orders, setOrders] = useState<IOrder[]>([])
  const [loading, setLoading] = useState(false)
  const { user, dispatchUser } = useUserContext()
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentOrder, setCurrentOrder] = useState<IOrder>()

  const columns: TableProps<IOrder>['columns'] = [
    {
      title: 'Id',
      dataIndex: 'id',
      key: 'id',
      render: (text) => <a>{text}</a>,
    },
    {
      title: 'Note',
      dataIndex: 'description',
      key: 'description',
      render: (text) => text || "No note"
    },
    {
      title: 'Status',
      dataIndex: 'orderType',
      key: 'orderType',
      render: (status) => <Tag color={status?.id === "f7e5cbee-e5de-4a7d-b688-5e56fdf96573" ? "yellow" : status?.id === "afb8d8b7-4035-42aa-a157-03f56d67c314" ? "red" : "green"}>{status?.name}</Tag>
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
        <Button type="primary" onClick={() => handleShowOrder(record)}>
          ...
        </Button>
      ),
    },
  ];

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

  useEffect(() => {
    fetchOrder()
  }, [user])


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
      } else {
        toast.error(`Error: ${data}`)
      }
    } catch (error) {
      console.error(`Error: ${error}`)
    }
  }
  const fetchOrder = async () => {
    if (!user) {
      await fetchUser()
    }
    setLoading(true)
    try {
      const { status, data }: { status: number, data: IOrder[] } = await get({
        url: Endpoint.ORDER_BUYER_URL + "/" + user?.id
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

    if (currentOrder?.voucher){
      price = (price * (1 - currentOrder.voucher.factor))
    }
    return Number.formatPrice(price)
  }

  return (
    <>
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
      <div className='OrderHistory flex flex-col items-center px-24 py-5 gap-2 text-black'>

        <Spin spinning={loading} indicator={<LoadingOutlined />}>
          <div className="p-5 text-black bg-white rounded-md shadow-md animate-fade">
            <p className="text-3xl">Order History</p>
            <Table<IOrder> dataSource={orders} pagination={false} columns={columns} />
          </div>
        </Spin>
      </div>
    </>
  )
}

export default OrderHistory
