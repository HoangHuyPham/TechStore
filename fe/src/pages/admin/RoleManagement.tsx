import { Avatar, Button, message, Spin, Table } from "antd"
import { IRole, IUser } from "../../interfaces";
import { useEffect, useState } from "react";
import { useUserContext } from "../../contexts/hooks";
import { del, Endpoint, get } from "../../request/AppRequest";
import { toast } from "react-toastify";
import { USER_ACTION } from "../../contexts/UserContext";
import { LoadingOutlined, UserOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

const RoleManagement: React.FC = () => {
    const [roles, setRoles] = useState<IRole[]>([])
    const [loading, setLoading] = useState(false)
    const { user, dispatchUser } = useUserContext()

    useEffect(() => {
        fetchRoles()
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
            title: 'Action',
            key: 'action',
            render: (_, record) => (
              <Button type="primary" danger onClick={() => handleRemoveRole(record)}>
               X
              </Button>
            ),
          },
    ];

    const handleRemoveRole = async (role:IRole)=>{
        try {
            const {status, data} = await del({
                url: Endpoint.ROLE_URL + "/" + role.id
            })
            if (status === 200){
                message.success(`Remove role ${role.id} success.`)
                fetchRoles()
            }else{
                message.error(`Remove role ${role.id} failed.`)
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
    const fetchRoles = async () => {
        if (!user) {
            await fetchUser()
        }
        setLoading(true)
        try {
            const { status, data }: { status: number, data: IRole[] } = await get({
                url: Endpoint.ROLE_URL
            })

            if (status === 200) {
                setRoles(data)
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
                <Table dataSource={roles} columns={columns} pagination={false} />
            </Spin>
        </div>
    </>
}

export default RoleManagement