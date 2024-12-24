import { useState } from "react"
import AppSteps from "../components/AppSteps"
import CartItem from "../components/CartItem"
import { ICart } from "../interfaces/ICart"

const Cart: React.FC = () => {
  const [cartList, setCartList] = useState<ICart[]>([
    {
      id : "123",
      name : "Xiaomi redmi note 10",
      beforePrice: 12000,
      price: 10000,
      quantity: 5,
      category: "Phone",
      stock: 10,
      thumbnail: "",
      option: "red 95%"

    }
  ])

  const removeCart: (id: string) => void = (id:string)=>{
    const newCartList = cartList.filter(v=>v.id !== id)
    setCartList(newCartList)
  }

  return (
    <>
      <AppSteps currentStep={0} />
      <div className='Cart flex flex-col items-center px-24 gap-2'>
        <div className="flex bg-[#3b82f6] w-[100%] px-5 py-2 m-2 text-1xl shadow-xl">
          <span className="w-[40%]">Product</span>
          <span className="w-[20%] text-center">Unit Price</span>
          <span className="w-[10%] text-center">Quantity</span>
          <span className="w-[20%] text-center">Total Price</span>
          <span className="w-[10%] text-center">Action</span>
        </div>

        {
            (cartList?.length) > 0 && cartList?.map((v)=><CartItem cart={v} removeCart={removeCart} />) 
        }

      </div>
    </>
  )
}

export default Cart
