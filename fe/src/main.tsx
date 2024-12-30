import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './styles/index.scss'
import App from './App.tsx'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Home from './pages/customer/Home.tsx'
import Login from './pages/customer/Login.tsx'
import Signup from './pages/customer/Signup.tsx'
import Cart from './pages/customer/Cart.tsx'
import Checkout from './pages/customer/Checkout.tsx'
import Profile from './pages/customer/Profile.tsx'
import Dashboard from './Dashboard.tsx'
import UserManagement from './pages/admin/UserManagement.tsx'
import RoleManagement from './pages/admin/RoleManagement.tsx'
import Error from './pages/Error.tsx'

const router = createBrowserRouter([
  {
    errorElement: <Error/>,
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
        path: "signup",
        Component: Signup,
      },
      {
        path: "cart",
        Component: Cart,
      },
      {
        path: "checkout",
        Component: Checkout,
      },
      {
        path: "me",
        Component: Profile,
      },
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
    ],
    
  }
]);

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
)
