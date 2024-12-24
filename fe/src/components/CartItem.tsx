import { useState } from "react"
import { ICart } from "../interfaces/ICart"
import logo from "/vite.svg"
import { Button, InputNumber } from "antd"


type CartItemProps = {
    cart: ICart
    removeCart: (id: string) => void
}

const CartItem: React.FC<CartItemProps> = ({cart, removeCart}) => {

    const [totalPrice, setTotalPrice] = useState(cart?.price * cart?.quantity)

    const handleChange = (value:number) => {
        if (value <= 0)
            removeCart(cart.id)
        setTotalPrice(cart.price * value)
    };

    return (
        <>
        <div className="flex w-[100%] px-5 py-2 mb-2 text-1xl-black text-black bg-[#ffffff] shadow-xl">
          <span className="w-[40%] flex gap-2 items-center">
            <img className="w-[120px] h-[120px]" src={cart.thumbnail || logo} />
            <span>
                {cart.name} <br/>
                (<span className="text-sm text-red-500">{cart.option}</span>)
            </span>
            
          </span>
          <span className="w-[20%] flex justify-center items-center">{cart.price}</span>
          <span className="w-[10%] flex justify-center items-center">
          <InputNumber min={0} max={cart.stock} defaultValue={cart.quantity} onChange={handleChange} />
          </span>
          <span className="w-[20%] flex justify-center items-center">{totalPrice}</span>
          <span className="w-[10%] flex justify-center items-center">
            <Button type="primary" danger >Remove</Button>
          </span>
        </div>    
        </>
    )
}

export default CartItem
