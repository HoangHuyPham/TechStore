import { Button } from 'antd'
import logo from '/vite.svg'
import { ShoppingCartOutlined, UserOutlined } from '@ant-design/icons'
import { Link } from 'react-router-dom'
const Header:React.FC = ()=>{
  return (
    <>
      <header className="Header flex fixed z-50 w-[100%] justify-between py-2 px-5">
        <Link to={"/home"}><img src={logo} /></Link>
        <section className='flex gap-2'>
          <Button type="primary"><ShoppingCartOutlined /></Button>
          <Link to={"/login"}><Button to={"/login"} type="primary" danger icon={<UserOutlined />}>Login</Button></Link>
        </section>
      </header>
    </>
  )
}

export default Header
