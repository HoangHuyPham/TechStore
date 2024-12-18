import { Button } from 'antd'
import logo from '/vite.svg'
import { ShoppingCartOutlined, UserOutlined } from '@ant-design/icons'
const Header:React.FC = ()=>{
  return (
    <>
      <header className="Header flex fixed z-50 w-[100%] justify-between py-2 px-5">
        <img src={logo} />
        <section className='flex gap-2'>
          <Button type="primary"><ShoppingCartOutlined /></Button>
          <Button type="primary" danger icon={<UserOutlined />}>Login</Button>
        </section>
      </header>
    </>
  )
}

export default Header
