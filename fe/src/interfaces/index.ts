interface ICartItem {
    id: string,
    name: string,
    thumbnail: string | null,
    quantity: number,
    beforePrice: number,
    price: number,
    category: string,
    stock: number,
    option: string,
    isChecked: boolean,
}

interface IProductItem {
    id: string,
    name:string,
    thumbnail: string,
    category: string,
    productDetail_description: string,
    productDetail_beforePrice: number,
    productDetail_price: number,
    productDetail_stock: number,
    productDetail_totalRating: number
}

export type {ICartItem, IProductItem}