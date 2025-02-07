import { Checkbox } from "antd";
import { useState } from "react";

const RememberMe: React.FC = () => {
    const [remember, setRemember] = useState<boolean>(localStorage.getItem("rememberMe") === 'true');
    const handleRemember = () => {
        localStorage.setItem("rememberMe", !remember + "")
        setRemember(!remember)
    }
    return (
        <Checkbox checked={remember} onChange={handleRemember}>Remember me</Checkbox>
    )
}

export default RememberMe