interface ICart {
    id: string,
    userId: string,
    cartItems: ICartItem[]
}
interface ICartItem {
    id: string,
    quantity: number,
    isSelected: boolean,
    productOption?: IProductOption
    product: IProductItem
}

interface ICategory {
    id: string,
    name: string
}

interface IProductOption{
    id: string,
    name: string,
}

interface IPreview{
    id: string,
    image: IImage
}

interface IProductDetail{
    id: string,
    description: string,
    stock: number,
    beforePrice: number,
    price: number,
    totalRating: number,
    productOptions: IProductOption[],
    previews: IPreview[]
}

interface IProductItem {
    id: string,
    name:string,
    thumbnail: IImage,
    category: ICategory,
    productDetail: IProductDetail
    createOn: Date
}

interface IRole {
    id: string,
    name: string
}

interface IImage {
    id: string,
    name: string,
    url: string
}

interface IUser {
    id?: string,
    name?: string,
    address?: string,
    email?: string,
    phone?: number,
    avatar?: IImage,
    gender?: boolean,
    role?: IRole,
    createOn?: Date
}
interface IVoucher{
    id?: string,
    name?:string,
    code: string,
    isActive: boolean,
    expiredAt: Date,
    factor: number,
    createdAt: Date
}
interface IOrder {
    id?: string,
    description?: string,
    orderType?: IOrderType,
    buyerId?: string,
    voucher?: IVoucher,
    cartItems?: ICartItem[],
    createdOn?: Date
}

interface IOrderType{
    id?: string,
    name?: string
}

export type {ICartItem, IProductItem, IUser, ICategory, ICart, IProductOption, IImage, IVoucher, IOrder, IRole}