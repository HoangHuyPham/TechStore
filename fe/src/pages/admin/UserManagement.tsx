import { Avatar, Button, message, Spin, Table } from "antd"
import { IUser } from "../../interfaces";
import { useEffect, useState } from "react";
import { useUserContext } from "../../contexts/hooks";
import { del, Endpoint, get } from "../../request/AppRequest";
import { toast } from "react-toastify";
import { USER_ACTION } from "../../contexts/UserContext";
import { LoadingOutlined, UserOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

const UserManagement: React.FC = () => {
    const [users, setUsers] = useState<IUser[]>([])
    const [loading, setLoading] = useState(false)
    const { user, dispatchUser } = useUserContext()

    useEffect(() => {
        fetchUsers()
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
            title: 'Address',
            dataIndex: 'address',
            key: 'address',
            render: (text) => text || "No Address"
        },
        {
            title: 'Phone',
            dataIndex: 'phone',
            key: 'phone',
            render: (text) => text || "No Phone"
        },
        {
            title: 'Gender',
            dataIndex: 'gender',
            key: 'gender',
            render: (gender) => gender?"Male":"Female"
        },
        {
            title: 'Role',
            dataIndex: 'role',
            key: 'role',
            render: (role) => <span className={role?.name === "Admin" ?"text-red-400":"text-black"}>{role?.name}</span>
        },
        {
            title: 'Avatar',
            dataIndex: 'avatar',
            key: 'avatar',
            render: (avatar) => <Avatar className="flex-shrink-0" size={64} src={avatar?.url} icon={<UserOutlined />} />
        },
        {
            title: 'Action',
            key: 'action',
            render: (_, record) => (
              <Button type="primary" danger onClick={() => handleRemoveUser(record)}>
               X
              </Button>
            ),
          },
    ];

    const handleRemoveUser = async (user:IUser)=>{
        try {
            const {status, data} = await del({
                url: Endpoint.USER_URL + "/" + user.id
            })
            if (status === 200){
                message.success(`Remove user ${user.id} success.`)
                fetchUsers()
            }else{
                message.error(`Remove user ${user.id} failed.`)
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
    const fetchUsers = async () => {
        if (!user) {
            await fetchUser()
        }
        setLoading(true)
        try {
            const { status, data }: { status: number, data: IUser[] } = await get({
                url: Endpoint.USER_URL
            })

            if (status === 200) {
                setUsers(data)
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

    return <>
        <div className="w-[100%] animate-fade">
            <p className="text-center p-5">User (General)</p>
            <Spin spinning={loading} indicator={<LoadingOutlined />}>
                <Table dataSource={users} columns={columns} pagination={false} />
            </Spin>
        </div>
    </>
}

export default UserManagement