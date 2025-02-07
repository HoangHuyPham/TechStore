import { createContext, Dispatch, ReactNode, Reducer, useReducer } from "react"
import { ICartItem } from "../interfaces"

interface CheckoutType {
    checkoutItems: ICartItem[],
    dispatchCheckoutItems: Dispatch<Action>
}

interface Action {
    type: string,
    payload: ICartItem[] | ICartItem
}

const CHECKOUT_ACTION = {
    ADD: "add",
    DELETE: "delete",
    UPDATE: "update",
    INIT: "init"
}

const CheckoutContext = createContext<CheckoutType>({} as CheckoutType)

const CheckoutReducer: Reducer<ICartItem[], Action> = (state, action) => {
    switch (action?.type) {
        case CHECKOUT_ACTION.INIT:
            return action.payload as ICartItem[]
        case CHECKOUT_ACTION.UPDATE:
            return state.map((v)=>{
                const checkoutItem = action.payload as ICartItem
                if (checkoutItem.id === v.id) return checkoutItem
                return v
            })
        case CHECKOUT_ACTION.DELETE:
            return state.filter(v => {
                const checkoutItem = action.payload as ICartItem
                return checkoutItem.id !== v.id   
            })
        default:
            return state
    }
}

const CheckoutProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [checkoutItems, dispatchCheckoutItems] = useReducer(CheckoutReducer, [])

    return (
        <CheckoutContext.Provider value={{ checkoutItems, dispatchCheckoutItems } as CheckoutType}>
            {children}
        </CheckoutContext.Provider>
    )
}

export {
    CheckoutContext, CheckoutProvider, 
    // eslint-disable-next-line react-refresh/only-export-components
    CHECKOUT_ACTION
}