import { Spin } from "antd";
import { IProductItem } from "../interfaces"
import ProductItem from "./ProductItem";
import { LoadingOutlined } from "@ant-design/icons";

type ProductListProps = {
    products: IProductItem[] | undefined;
}

const ProductList: React.FC<ProductListProps> = ({ products }) => {
    return (
        <>
            { products && (<div className="ProductList flex flex-wrap w-[80%] gap-10">
                {
                    products?.map((v) => <ProductItem product={v} />)
                }
            </div>) || <Spin indicator={<LoadingOutlined spin />} size="large" spinning={true} />}
            
        </>
    )
}

export default ProductList
