import { Product } from "../interfaces/Product"
import ProductItem from "./ProductItem";

type ProductListProps = {
    products: Product[] | undefined;
}

const ProductList: React.FC<ProductListProps> = ({ products }) => {
    return (
        <>
            <div className="ProductList flex flex-wrap w-[80%] gap-10">
                {
                    products?.map((v) => <ProductItem product={v} />)
                }
            </div>
        </>
    )
}

export default ProductList
