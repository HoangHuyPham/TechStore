import axios from "axios"

type APIResponse = {
    status: number,
    data: any
}

const Endpoint = {
    LOGIN_URL : 'api/Auth/Login',
    PRODUCT_URL : 'api/Product',
    CATEGORY_URL : 'api/Category',
    CART_URL : 'api/Cart',
    PROFILE_URL : 'api/Profile',
    UPLOAD_URL : 'api/Image/Upload',
    CHECK_EMAIL_URL : 'api/Auth/CheckEmail',
    REGISTER_URL : 'api/Auth/Register',
    CHANGE_PASSWORD_URL : 'api/Auth/ChangePassword',
    CHECK_VOUCHER_URL : 'api/Voucher/CheckVoucher',
    VOUCHER_URL : 'api/Voucher',
    ORDER_URL : 'api/Order',
    ORDER_BUYER_URL : 'api/Order/ByBuyer',
    USER_URL : 'api/User',
    ROLE_URL : 'api/Role'
 }

let appRequest = axios.create({
    baseURL: "https://localhost:5000/", headers: {
    Authorization : `Bearer ${localStorage.getItem("jwt")}`,}
})

const updateAppRequest = () => {
    appRequest = axios.create({
        baseURL: "https://localhost:5000/", headers: {
        Authorization : `Bearer ${localStorage.getItem("jwt")}`,}
    })
}

const post = async ({url, payload} : {url: any, payload: any}) : Promise<APIResponse>=>{
    const { data }  = await appRequest.post(url, payload) 
    return data
}

const get = async ({url, params} : {url: any, params?: any}) : Promise<APIResponse> =>{
    const { data } = await appRequest.get(url, {params})
    return data
}

const del = async ({url} : {url: any}) : Promise<APIResponse>=>{
    const { data }  = await appRequest.delete(url)
    return data
}

const update = async ({url, payload} : {url: any, payload: any}) : Promise<APIResponse>=>{
    const { data }  = await appRequest.put(url, payload)
    return data
}

export {
    get, post, del,update, Endpoint, updateAppRequest, appRequest
}