import { Button } from 'antd'
import logo from '/vite.svg'
import { UserOutlined } from '@ant-design/icons'
function Header() {

  return (
    <>
      <header className="Header flex fixed z-50 w-[100%] justify-between py-2 px-5">
        <img src={logo}/>
        <Button type="primary" danger icon={<UserOutlined />}>Login</Button>
      </header>
    </>
  )
}

export default Header
