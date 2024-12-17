interface Product {
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

export type {Product}