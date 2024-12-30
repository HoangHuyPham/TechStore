import './styles/App.scss'
import Header from './components/Header'
import Footer from './components/Footer'
import { Outlet } from 'react-router-dom'
import { CartProvider } from './contexts'
import { ConfigProvider } from 'antd'

function App() {

  return (
    <>
      <ConfigProvider>
        <CartProvider>
          <div className='App'>
            <Header />
            <Outlet />
            <Footer />
          </div>
        </CartProvider>
      </ConfigProvider>
    </>
  )
}

export default App
