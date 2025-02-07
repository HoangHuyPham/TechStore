import { useContext } from "react"
import { CartContext } from "../CartContext"
import { UserContext } from "../UserContext";
import { CheckoutContext } from "../CheckoutContext";

const useCartItem = ()=> useContext(CartContext);
const useUserContext = ()=> useContext(UserContext);
const useCheckoutContext = ()=> useContext(CheckoutContext);

export {
    useCartItem, useUserContext, useCheckoutContext
}