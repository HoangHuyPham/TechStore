import { useContext } from "react"
import { CartContext } from "../CartContext"

const useCartItem = ()=> useContext(CartContext);

export {
    useCartItem
}