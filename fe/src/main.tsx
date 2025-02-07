import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles/index.scss'
import App from './App.tsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Home from './pages/customer/Home.tsx'
import Login from './pages/customer/Login.tsx'
import Signup from './pages/customer/Register.tsx'
import Cart from './pages/customer/Cart.tsx'
import Checkout from './pages/customer/Checkout.tsx'
import Profile from './pages/customer/Profile.tsx'
import Dashboard from './Dashboard.tsx'
import UserManagement from './pages/admin/UserManagement.tsx'
import RoleManagement from './pages/admin/RoleManagement.tsx'
import Error from './pages/Error.tsx'
import { UserContext, UserProvider } from './contexts/UserContext.tsx'
import ProductDetail from './pages/customer/ProductDetail.tsx'
import Register from './pages/customer/Register.tsx'
import Order from './pages/customer/Order.tsx'
import OrderHistory from './pages/customer/OrderHistory.tsx'
import OrderManagement from './pages/admin/OrderManagement.tsx'
import ProductManagement from './pages/admin/ProductManagement.tsx'
import VoucherManagement from './pages/admin/VoucherManagement.tsx'
import Forbidden from './pages/Forbidden.tsx'

const router = createBrowserRouter([
  {
    path: "/forbidden",
    Component: Forbidden
  },
  {
    errorElement: <Error />,
    path: "/",
    Component: App,
    children: [
      {
        path: "",
        Component: Home,
      },
      {
        path: "home",
        Component: Home,
      },
      {
        path: "login",
        Component: Login,
      },
      {
        path: "register",
        Component: Register,
      },
      {
        path: "cart",
        Component: Cart,
      },
      {
        path: "checkout",
        Component: Checkout,
        children: [
          {
            path: ":anonymousCart",
            Component: Checkout,
          }
        ]
      },
      {
        path: "order",
        Component: Order,
        children: [
          {
            path: ":voucherId",
            Component: Order,
          }
        ]
      },
      {
        path: "order_history",
        Component: OrderHistory,
      },
      {
        path: "me",
        Component: Profile,
      },
      {
        path: "product/:productId/",
        Component: ProductDetail,
      }
    ],
  },
  {
    path: "/admin",
    Component: Dashboard,
    children: [
      {
        path: "",
        Component: UserManagement
      },
      {
        path: "user-general",
        Component: UserManagement
      },
      {
        path: "role-general",
        Component: RoleManagement,
      },
      {
        path: "order-general",
        Component: OrderManagement,
      },
      {
        path: "product-general",
        Component: ProductManagement,
      },
      {
        path: "voucher-general",
        Component: VoucherManagement,
      },
    ],

  }
]);

createRoot(document.getElementById('root')!).render(
  <UserProvider>
    <RouterProvider router={router} />
  </UserProvider>
)
