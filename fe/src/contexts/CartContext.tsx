import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react"
import { ICartItem } from "../interfaces"

interface CartType {
    cartItems: ICartItem[],
    dispatchCartItems: Dispatch<Action>
}

interface Action {
    type: string,
    payload: ICartItem[] | ICartItem
}

const CART_ACTION = {
    ADD: "add",
    DELETE: "delete",
    UPDATE: "update"
}

const CartContext = createContext<CartType>({} as CartType)

const initData = ()=>{
    return [
        {
            id: "1",
            category: "Dien thoai",
            beforePrice: 10000,
            price: 8000,
            name: "Xiaomi redmi note 10",
            stock: 10,
            quantity: 2,
            isChecked: false,
            thumbnail: "https://viostore.vn/wp-content/uploads/2024/01/21.png",
            option: "red"
        },
        {
            id: "2",
            category: "Dien thoai",
            beforePrice: 10000,
            price: 8000,
            name: "Xiaomi redmi note 11",
            stock: 12,
            quantity: 5,
            isChecked: false,
            thumbnail: "https://viostore.vn/wp-content/uploads/2024/01/21.png",
            option: "blue"
        }
    ] as ICartItem[]
}

const CartReducer: Reducer<ICartItem[], Action> = (state, action) => {
    switch (action.type) {
        case CART_ACTION.ADD:
            return [...state, action.payload as ICartItem]
        case CART_ACTION.UPDATE:
            return state.map((v)=>{
                const cartItem = action.payload as ICartItem
                if (cartItem.id === v.id) return cartItem
                return v
            })
        case CART_ACTION.DELETE:
            return state.filter(v => {
                const cartItem = action.payload as ICartItem
                return cartItem.id !== v.id   
            })
        default:
            return state
    }
}


const CartProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [cartItems, dispatchCartItems] = useReducer(CartReducer, [], initData)

    return (
        <CartContext.Provider value={{ cartItems, dispatchCartItems } as CartType}>
            {children}
        </CartContext.Provider>
    )
}

export {
    CartContext, CartProvider, 
    // eslint-disable-next-line react-refresh/only-export-components
    CART_ACTION
}