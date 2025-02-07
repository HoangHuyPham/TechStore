import { CheckOutlined, EditFilled, KeyOutlined, ManOutlined, PhoneOutlined, UploadOutlined, UserOutlined, WomanOutlined } from "@ant-design/icons"
import { Avatar, Button, Divider, Input, InputNumber, message, Modal, Space } from "antd"
import { useUserContext } from "../../contexts/hooks"
import { useEffect, useState } from "react"
import { Endpoint, get, post, update } from "../../request/AppRequest"
import { USER_ACTION } from "../../contexts/UserContext"
import { IUser } from "../../interfaces"
import { toast } from "react-toastify"
import { useNavigate } from "react-router-dom"

const Profile: React.FC = () => {
    const { user, dispatchUser } = useUserContext()
    const [uploadLoading, setUploadLoading] = useState(false)
    const [saveLoading, setSaveLoading] = useState(false)
    const [needLogin, setNeedLogin] = useState(false)
    const [isChangePasswordOpen, setChangePasswordOpen] = useState(false);
    const [oldPassword, setOldPassword] = useState<string>()
    const [newPassword, setNewPassword] = useState<string>()
    const [changePasswordLoading, setChangePasswordLoading] = useState(false)
    const navigate = useNavigate()

    useEffect(() => {
        fetchUser()
    }, [])


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
                setNeedLogin(false)
            } else {
                toast.error(`Error: ${data}`)
            }
        } catch (error) {
            setNeedLogin(true)
            toast.error(`Error: ${error}`)
        }
    }

    const updateAvatar = async (id) => {
        const { status, data } = await update({
            url: Endpoint.PROFILE_URL,
            payload: {
                name: user?.name,
                address: user?.address,
                phone: user?.phone,
                avatarId: id,
            }
        })

        if (status === 200) {
            message.success("Update avatar success.")
            dispatchUser({
                type: USER_ACTION.UPDATE,
                payload: data as IUser
            })
        }
    }

    const uploadAvatar = async (e) => {
        setUploadLoading(true)
        const f = e.target.files[0]

        try {
            const { status, data } = await uploadToServer(f)

            if (status === 200) {
                message.success("upload success.")
                updateAvatar(data?.id)
            } else {
                message.error(`Error ${data}`)
            }
        } catch (error) {
            message.error(`Error ${error}`)
        } finally {
            setUploadLoading(false)
        }
    }

    const uploadToServer = (file) => {
        const form = new FormData();
        form.append("file", file)
        return post({
            url: Endpoint.UPLOAD_URL,
            payload: form
        })
    
    }

    const handleChangeName = (e) => {
        dispatchUser({
            type: USER_ACTION.UPDATE,
            payload: {
                name: e?.target?.value
            } as IUser
        })
    }

    const handleChangeAddress = (e) => {
        dispatchUser({
            type: USER_ACTION.UPDATE,
            payload: {
                address: e?.target?.value
            } as IUser
        })
    }

    const handleChangePhone = (e) => {
        dispatchUser({
            type: USER_ACTION.UPDATE,
            payload: {
                phone: e?.target?.value
            } as IUser
        })
    }

    const handleSave = async () => {
        setSaveLoading(true)
        const { status, data } = await update({
            url: Endpoint.PROFILE_URL,
            payload: {
                name: user?.name,
                address: user?.address,
                phone: user?.phone,
                avatarId: user?.avatar?.id,
            }
        })

        if (status === 200) {
            message.success("Save profile success.")
        } else {
            message.error(`Save profile failed. ${data}`)
        }
        setSaveLoading(false)
    }

    const handleLogout = () => {
        dispatchUser({
            type: USER_ACTION.DELETE,
            payload: null
        })
        navigate("/login")
    }

    const handleChangePassword = async () => {
        setChangePasswordLoading(true)
        try{
            const { status, data } = await post({
                url: Endpoint.CHANGE_PASSWORD_URL,
                payload: {
                    oldPassword,
                    newPassword
                }
            })

            if (status === 200){
                if (data){
                    message.success("Change password success.")
                }else{
                    message.error(`Change password failed.`)
                }
            }
        }catch (err){
            toast.error(`Error ${err}`)
        }finally {
            setChangePasswordLoading(false)
        }
    };

    const handleCancel = () => {
        setChangePasswordOpen(false);
    };

    return <>
        <div className='Profile flex flex-col items-center p-5 gap-2 animate-fade w-[100%]'>
            {!needLogin && <>
                <div className="flex flex-col items-center bg-white rounded-md shadow-md p-5 w-[60%]">
                    <Divider orientation="left" style={{ borderColor: '#000000' }} >Information</Divider>

                    <div className="flex justify-center gap-20">
                        <span className="flex flex-col items-center gap-2">
                            <Avatar className="flex-shrink-0" size={128} src={user?.avatar?.url} icon={<UserOutlined />} />
                            <Button loading={uploadLoading} onClick={() => document.getElementById("upload-inp")?.click()} icon={<UploadOutlined />}>Change avatar</Button>
                        </span>

                        <div className="flex flex-col items-center justify-center gap-2">
                            <Input maxLength={12} style={{ width: 256 }} addonBefore="Name" value={user?.name} onChange={handleChangeName} />
                            <Input type="number" style={{ width: 256 }} addonBefore={<PhoneOutlined />} value={user?.phone} placeholder={!user?.phone && "No phone"} onChange={handleChangePhone} />
                            <Input disabled style={{ width: 256 }} addonBefore="Email" value={user?.email} />
                            <Input style={{ width: 256 }} addonBefore="Address" value={user?.address} placeholder={!user?.address && "No address"} onChange={handleChangeAddress} />
                            <Input disabled style={{ width: 256 }} addonBefore="Gender" addonAfter={user?.gender ? <ManOutlined style={{ color: 'blue' }} /> : <WomanOutlined style={{ color: 'pink' }} />} value={user?.gender ? "Male" : "Female"} placeholder={!user?.phone && "No gender"} />
                            <Button type="primary" loading={saveLoading} onClick={handleSave}>Save</Button>
                            <input className="hidden" id="upload-inp" type="file" onChange={uploadAvatar}></input>
                        </div>
                    </div>

                    <Divider orientation="left" style={{ borderColor: '#000000' }} >Privacy</Divider>
                    <div className="flex gap-2">
                        <Button type="primary" onClick={() => setChangePasswordOpen(true)} icon={<EditFilled />}>Change password</Button>
                        <Button type="primary" danger onClick={handleLogout}>Logout</Button>
                    </div>
                </div>
            </> || <p className="text-black">You need to login to see this.</p>}
        </div>

        <Modal width={320} title="Change Password" 
        open={isChangePasswordOpen} 
        onCancel={handleCancel}
        footer={[
            <Button key="back" onClick={handleCancel}>
              Return
            </Button>,
            <Button key="submit" type="primary" loading={changePasswordLoading} onClick={handleChangePassword}>
              Confirm
            </Button>
          ]}
        >
            <div className="flex flex-col gap-2 p-2">
                <Input.Password style={{ width: 240 }} prefix={<KeyOutlined />} placeholder={"Old Password"} visibilityToggle={false} onChange={(e => setOldPassword(e.target.value))} value={oldPassword} />
                <Input.Password style={{ width: 240 }} prefix={<CheckOutlined />} placeholder={"New Password"} visibilityToggle={false} onChange={(e => setNewPassword(e.target.value))} value={newPassword} />
            </div>
        </Modal>
    </>
}

export default Profile