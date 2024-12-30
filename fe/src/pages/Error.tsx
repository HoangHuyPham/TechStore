import { FacebookFilled, GithubFilled, SettingOutlined, TwitterCircleFilled } from "@ant-design/icons"
import { Button } from "antd"
import { useNavigate } from "react-router-dom"

const Error: React.FC = () => {
    const navigate = useNavigate()
    return <>
        <div className="flex flex-col gap-2 justify-center items-center bg-slate-600 min-w-[100%] min-h-[100vh]">
            <span>
                <SettingOutlined style={{ fontSize: '240px', color: '#08c' }} spin />
            </span>
            <p className="text-2xl font-bold">Error 404</p>
            <p>Try Again / Contact With Us</p>
            <p className="flex gap-2">
                <FacebookFilled/>
                <GithubFilled/>
                <TwitterCircleFilled/>
            </p>
            <Button onClick={()=>{navigate(-1)}} type='primary'>Back</Button>
        </div>
    </>
}

export default Error