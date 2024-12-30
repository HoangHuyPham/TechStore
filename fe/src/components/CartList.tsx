import { Button, Checkbox, CheckboxChangeEvent } from "antd";
import CartItem from "./CartItem";
import { useCartItem } from "../contexts/hooks";
import { CART_ACTION } from "../contexts/CartContext";
import { ICartItem } from "../interfaces";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ButtonWarning } from "../custom/button";


const CartList: React.FC = () => {
    const { cartItems, dispatchCartItems } = useCartItem()
    const [ checkAll, setCheckAll ] = useState(false)

    const removeCartItem = (itemItem: ICartItem) => {
        dispatchCartItems({
            type: CART_ACTION.DELETE,
            payload: itemItem
        })
    }

    const updateCartItem = (itemItem: ICartItem) => {
        dispatchCartItems({
            type: CART_ACTION.UPDATE,
            payload: itemItem
        })
    }

    const toggleCheckAll = (e: CheckboxChangeEvent) => {
        const val = e.target.checked 
        e.target.checked = val
        if (val){
            cartItems.forEach((v) => {
                v.isChecked = true
                dispatchCartItems({
                    type: CART_ACTION.UPDATE,
                    payload: v
                })
            })
        }else{
            cartItems.forEach((v) => {
                v.isChecked = false
                dispatchCartItems({
                    type: CART_ACTION.UPDATE,
                    payload: v
                })
            })
        }
    }

    return (
        <>
            <div className="flex bg-[#3b82f6] w-[100%] px-5 py-2 m-2 text-1xl shadow-xl animate-fade">
                <span className="w-[40%]"><Checkbox onChange={toggleCheckAll} /> Product</span>
                <span className="w-[20%] text-center">Unit Price</span>
                <span className="w-[10%] text-center">Quantity</span>
                <span className="w-[20%] text-center">Total Price</span>
                <span className="w-[10%] text-center">Action</span>
            </div>

            {cartItems && cartItems.length > 0 &&

                (cartItems?.length) > 0 && cartItems?.map((v) => <CartItem key={v.id} cartItem={v} removeCartItem={removeCartItem} updateCartItem={updateCartItem} />)

                || <p className="text-black"> No items...</p>}

            {
                cartItems &&
                cartItems?.length > 0 &&
                cartItems?.reduce((res, current) => res || current.isChecked, false) &&
                <Link className="sticky bottom-0" to="/checkout"><ButtonWarning type="primary">Checkout</ButtonWarning></Link>
            }
        </>
    )
}

export default CartList
