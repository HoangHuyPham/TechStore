import { Link, Outlet, useNavigate } from 'react-router-dom'
import { Button, ConfigProvider, Menu } from 'antd'
import { NumberOutlined, OrderedListOutlined, ProductOutlined, UserOutlined } from '@ant-design/icons';
import Header from './components/Header';
import Footer from './components/Footer';

const items: MenuItem[] = [

    {
        key: 'grp',
        label: 'Manager',
        type: 'group',
        children: [
            {
                key: 'sub1',
                label: 'User',
                icon: <UserOutlined />,
                children: [
                    { key: 'user-general', label: 'General' },
                    { key: 'role-general', label: 'Role' },
                ],
            },
            {
                key: 'sub2',
                label: 'Order',
                icon: <OrderedListOutlined />,
                children: [
                    { key: 'order-general', label: 'General' },
                ],
            },
            {
                key: 'sub3',
                label: 'Product',
                icon: <ProductOutlined />,
                children: [
                    { key: 'product-general', label: 'General' },
                ],
            },
            {
                key: 'sub4',
                label: 'Voucher',
                icon: <NumberOutlined />,
                children: [
                    { key: 'voucher-general', label: 'General' },
                ],
            },
        ],
    },
];

function Dashboard() {

    const navigate = useNavigate()

    const onClick: MenuProps['onClick'] = (e) => {
        navigate(e?.key)
    };
    return (
        <>
            <ConfigProvider>
                <div className='Dashboard bg-[#3b82f6] flex-col p-5 items-center gap-2 min-h-[100vh]'>
                    <span className='flex justify-between gap-5 m-5'>
                        <span className='flex gap-5'>
                            <Link to={"/"} className='text-orange-300 underline'>Home</Link>
                            <Link to={"/"} className='text-orange-300 underline'>Logout</Link>
                        </span>

                        <p className='text-red-300'>Hi admin</p>
                    </span>
                    <div className="flex gap-5">
                        <Menu
                            onClick={onClick}
                            style={{ width: 256 }}
                            defaultSelectedKeys={['1']}
                            mode="inline"
                            items={items}
                        />
                        <Outlet />
                    </div>
                </div>
            </ConfigProvider>
        </>
    )
}

export default Dashboard
