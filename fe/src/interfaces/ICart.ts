interface ICart {
    id: string,
    name: string,
    thumbnail: string,
    quantity: number,
    beforePrice: number,
    price: number,
    category: string,
    stock: number,
    option: string
}

export type {ICart}