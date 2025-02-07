import { Button, Dropdown, MenuProps, Space } from 'antd'
import logo from '/vite.svg'
import { DownOutlined, ShoppingCartOutlined, UserOutlined } from '@ant-design/icons'
import { Link, useNavigate } from 'react-router-dom'
import { useUserContext } from '../contexts/hooks'
import { useEffect } from 'react'
import { IUser } from '../interfaces'
import { USER_ACTION } from '../contexts/UserContext'
import { Endpoint, get } from '../request/AppRequest'
import { toast } from 'react-toastify'
import JWTUtil from '../helper/JWTUtil'

const Header: React.FC = () => {
  const { user, dispatchUser } = useUserContext()
  const navigate = useNavigate()

  useEffect(() => {
    if (JWTUtil.isValidJWT()){
      fetchUser()
    }
  }, [])

  const fetchUser = async () => {
    const { status, data } = await get({
      url: Endpoint.PROFILE_URL
    })
    
    if (status === 200){
        dispatchUser({
            type: USER_ACTION.ADD,
            payload: data as IUser
        })
    }else{
        toast.error(`Error: ${data}`)
    }
      
  }

  const handleLogout = () => {
    dispatchUser({
      type: USER_ACTION.DELETE,
      payload: null
    })
    navigate("/login")
  }

  const adminControl = [
    {
      key: '99',
      danger: true,
      label: <Link to={'/admin'}>Dashboard</Link>
    },
    {
      key: '1',
      label: <Link to={'/me'}>Profile</Link>
    },
    {
      key: '2',
      label: <Link to={'/order_history'}>Order History</Link>
    },
    {
      key: '3',
      danger: true,
      label: <a onClick={handleLogout}>Logout</a>,
    },
  ]

  const customerControl = [
    {
      key: '1',
      label: <Link to={'/me'}>Profile</Link>
    },
    {
      key: '2',
      label: <Link to={'/order_history'}>Order History</Link>
    },
    {
      key: '3',
      danger: true,
      label: <a onClick={handleLogout}>Logout</a>,
    },
  ]

  const items: MenuProps['items'] = user?.role.name === "Admin"?adminControl:customerControl

  return (
    <>
      <header className="Header bg-[#3b82f6] flex sticky top-0 z-50 w-[100%] justify-between py-2 px-5">
        <Link to={"/home"}><img src={logo} /></Link>
        <section className='flex gap-2'>
          {
            user && (<>
              <Link to={"/cart"}><Button type="primary"><ShoppingCartOutlined /></Button></Link>
              <Button type="primary" danger>
                <Dropdown menu={{ items }}>
                  <a onClick={(e) => e.preventDefault()}>
                    <Space>
                      {`${user?.name} (${user?.role.name})`}
                      <DownOutlined />
                    </Space>
                  </a>
                </Dropdown>
              </Button>

            </>) || (<>
              <Link to={"/cart"}><Button type="primary"><ShoppingCartOutlined /></Button></Link>
              <Link to={"/login"}><Button to={"/login"} type="primary" danger icon={<UserOutlined />}>Login</Button></Link>
            </>)
          }
        </section>
      </header>
    </>
  )
}

export default Header
