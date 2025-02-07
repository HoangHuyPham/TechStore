import { Avatar, Button, ConfigProvider, DatePicker, Input, message, Modal, Spin, Table } from "antd"
import { IUser, IVoucher } from "../../interfaces";
import { useEffect, useState } from "react";
import { useUserContext } from "../../contexts/hooks";
import { del, Endpoint, get, post } from "../../request/AppRequest";
import { toast } from "react-toastify";
import { USER_ACTION } from "../../contexts/UserContext";
import { LoadingOutlined } from "@ant-design/icons";
import moment from "moment-timezone";
import { useNavigate } from "react-router-dom";

const VoucherManagement: React.FC = () => {
    const [vouchers, setVouchers] = useState<IVoucher[]>([])
    const [loading, setLoading] = useState(false)
    const { user, dispatchUser } = useUserContext()
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [name, setName] = useState<string>()
    const [code, setCode] = useState<string>()
    const [factor, setFactor] = useState<number>()
    const [expired, setExpired] = useState<Date>()

    useEffect(() => {
        fetchVouchers()
    }, [user])

    const columns = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            render: (text) => text
        },
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
            render: (text) => text
        },
        {
            title: 'Code',
            dataIndex: 'code',
            key: 'code',
            render: (text) => text
        },
        {
            title: 'IsActive',
            dataIndex: 'isActive',
            key: 'isActive',
            render: (text) => text ? "true" : "false"
        },
        {
            title: 'ExpiredAt',
            dataIndex: 'expiredAt',
            key: 'expiredAt',
            render: (text) => moment(text).format("DD/MM/YYYY hh:mm:ss") || "Null"
        },
        {
            title: 'Factor',
            dataIndex: 'factor',
            key: 'factor',
            render: (text) => text
        },
        {
            title: 'CreatedAt',
            dataIndex: 'createdAt',
            key: 'createdAt',
            render: (text) => moment(text).format("DD/MM/YYYY hh:mm:ss") || "Null"
        },
        {
            title: 'Action',
            key: 'action',
            render: (_, record) => (
                <Button type="primary" danger onClick={() => handleRemoveVoucher(record)}>
                    X
                </Button>
            ),
        },
    ];

    const handleRemoveVoucher = async (voucher: IVoucher) => {
        try {
            const { status, data } = await del({
                url: Endpoint.VOUCHER_URL + "/" + voucher.id
            })
            if (status === 200) {
                message.success(`Remove voucher ${voucher.id} success.`)
                fetchVouchers()
            } else {
                message.error(`Remove voucher ${voucher.id} failed.`)
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
    const fetchVouchers = async () => {
        if (!user) {
            await fetchUser()
        }
        setLoading(true)
        try {
            const { status, data }: { status: number, data: IVoucher[] } = await get({
                url: Endpoint.VOUCHER_URL
            })

            if (status === 200) {
                setVouchers(data)
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

    const handleShowAddNew = () => {
        setIsModalOpen(true);
    };

    const handleOk = async () => {
        try {
            const { status, data } = await post({
                url: Endpoint.VOUCHER_URL,
                payload: {
                    name,
                    code,
                    expiredAt: expired,
                    factor,
                    isActive: false
                } as IVoucher
            })
            if (status === 200) {
                message.success(`Added voucher ${data.id} success.`)
                fetchVouchers()
            } else {
                message.error(`Add voucher failed.`)
                message.error(data)
            }
        } catch (error) {
            message.error(`Error: ${error}`)
        }

        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };



    return <>
        <div className="w-[100%] animate-fade">
            <p className="text-center p-5">Voucher (General)</p>
            <Modal title={"Add New Voucher"} open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
                <span className="flex flex-col gap-2">
                    <Input addonBefore="Name" onChange={e => setName(e.target.value)} />
                    <Input addonBefore="Code" onChange={e => setCode(e.target.value)} />
                    <DatePicker placeholder="Expired at" onChange={v => setExpired(v.toDate())} />
            
                    <Input addonBefore="Factor" onChange={e => setFactor(e.target.value)} />
                </span>
            </Modal>
            <Spin spinning={loading} indicator={<LoadingOutlined />}>
                <Button type="primary" className="mb-5" danger onClick={() => handleShowAddNew()}>New</Button>
                <Table dataSource={vouchers} columns={columns} pagination={false} />
            </Spin>
        </div>
    </>
}

export default VoucherManagement