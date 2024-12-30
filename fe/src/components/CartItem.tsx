import { useState } from "react"
import { ICartItem } from "../interfaces"
import logo from "/vite.svg"
import { Button, Checkbox, InputNumber } from "antd"


type CartItemProps = {
    cartItem: ICartItem
    removeCartItem: (cartItem: ICartItem) => void
    updateCartItem: (cartItem: ICartItem) => void
}

const CartItem: React.FC<CartItemProps> = ({cartItem, removeCartItem, updateCartItem}) => {

    const [totalPrice, setTotalPrice] = useState(cartItem?.price * cartItem?.quantity)

    const handleChange = (value:number) => {
        if (value <= 0)
            removeCartItem(cartItem)
        setTotalPrice(cartItem.price * value)
    };

    return (
        <>
        <div className="flex w-[100%] px-5 py-2 mb-2 text-1xl-black text-black bg-[#ffffff] shadow-xl animate-fade">
          <span className="w-[40%] flex gap-2 items-center">
            <Checkbox onChange={()=>{
                cartItem.isChecked = !cartItem.isChecked
                updateCartItem(cartItem)
            }} checked={cartItem.isChecked}/>
            <img className="w-[120px] h-[120px]" src={cartItem.thumbnail || logo} />
            <span>
                {cartItem.name} <br/>
                (<span className="text-sm text-red-500">{cartItem.option}</span>)
            </span>
            
          </span>
          <span className="w-[20%] flex justify-center items-center relative"><p className="absolute text-red-500 text-sm line-through top-10 left-7">{cartItem.beforePrice > cartItem.price && cartItem.beforePrice}</p><p className="font-[500]">{cartItem.price}</p></span>
          <span className="w-[10%] flex justify-center items-center">
          <InputNumber min={0} max={cartItem.stock} defaultValue={cartItem.quantity} onChange={handleChange} />
          </span>
          <span className="w-[20%] flex justify-center items-center">{totalPrice}</span>
          <span className="w-[10%] flex justify-center items-center">
            <Button type="primary" danger onClick={()=>removeCartItem(cartItem)}>Remove</Button>
          </span>
        </div>    
        </>
    )
}

export default CartItem
