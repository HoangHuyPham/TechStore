import './styles/App.scss'
import Header from './components/Header'
import Footer from './components/Footer'
import { Outlet } from 'react-router-dom'

function App() {

  return (
    <>
      <div className='App'>
        <Header />
        <Outlet />
        <Footer />
      </div>
    </>
  )
}

export default App
