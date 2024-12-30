import { EditFilled, ManOutlined, PhoneOutlined, UserOutlined } from "@ant-design/icons"
import { Avatar, Button, Divider, Form, Input, InputNumber } from "antd"

const Profile: React.FC = () => {
    return <>
        <div className='Profile flex flex-col items-center p-5 gap-2 animate-fade w-[100%]'>
            <div className="flex flex-col items-center bg-white rounded-md shadow-md p-5 w-[60%]">
                <Divider orientation="left" style={{ borderColor: '#000000' }} >Information</Divider>

                <div className="flex justify-center gap-20">
                    <Avatar className="flex-shrink-0" size={128} src="https://scontent.fsgn5-5.fna.fbcdn.net/v/t39.30808-1/469379505_2926724357502318_8686123281838179960_n.jpg?stp=dst-jpg_s200x200_tt6&_nc_cat=108&ccb=1-7&_nc_sid=e99d92&_nc_ohc=Sznzu4hI4GgQ7kNvgEzy2c8&_nc_zt=24&_nc_ht=scontent.fsgn5-5.fna&_nc_gid=AvRr_7qxQRdZYdl4ej94pn9&oh=00_AYBERwU2GE7q3b9F5JA5UEyQ9Bu70ZLnueq3JElk0vZP9w&oe=6776DFED" icon={<UserOutlined />} />
                    <div className="flex flex-col items-center justify-center gap-2">
                        <Input maxLength={12} style={{ width: 256 }} addonBefore="Name" value="Huy Pham" />
                        <InputNumber style={{ width: 256 }} addonBefore={<PhoneOutlined />} value="0123456789" />
                        <Input style={{ width: 256 }} addonBefore="Email" value="abc@gmail.com" />
                        <Input style={{ width: 256 }} addonBefore="Address" value="415abc" />
                        <Input style={{ width: 256 }} addonBefore="Gender" addonAfter={<ManOutlined />} value="Male" />
                    </div>
                </div>

                <Divider orientation="left" style={{ borderColor: '#000000' }} >Privacy</Divider>
                <div className="flex gap-2">
                    <Button type="primary" icon={<EditFilled/>}>Change password</Button>
                    <Button type="primary" danger>Logout</Button>
                </div>
            </div>
        </div>
    </>
}

export default Profile