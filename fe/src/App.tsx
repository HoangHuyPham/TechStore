import './styles/App.scss'
import Header from './components/Header'
import Footer from './components/Footer'
import { Outlet } from 'react-router-dom'
import { CartProvider, CheckoutProvider } from './contexts'
import { ConfigProvider } from 'antd'
import { Bounce, ToastContainer } from 'react-toastify'

function App() {

  return (
    <>
      <ConfigProvider>
        <CartProvider>
          <CheckoutProvider>
            <div className='App'>
              <ToastContainer
                position="bottom-right"
                autoClose={3000}
                hideProgressBar={false}
                newestOnTop={true}
                closeOnClick={true}
                rtl={false}
                pauseOnFocusLoss={false}
                draggable
                pauseOnHover
                theme="colored"
                transition={Bounce}
              />
              <Header />
              <Outlet />
              <Footer />
            </div>
          </CheckoutProvider>
        </CartProvider>
      </ConfigProvider>
    </>
  )
}

export default App
