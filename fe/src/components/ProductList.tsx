import { Spin } from "antd";
import { IProductItem } from "../interfaces"
import ProductItem from "./ProductItem";
import { LoadingOutlined } from "@ant-design/icons";

type ProductListProps = {
    products: IProductItem[] | undefined;
    loading: boolean | undefined,
}

const ProductList: React.FC<ProductListProps> = ({ products, loading}) => {
    return (
        <> 
            {products && (<div className="ProductList flex flex-wrap w-[80%] gap-10">
                {
                    products?.map((v) => <ProductItem key={v.id} productItem={v} />)
                }
            </div>) || <Spin indicator={<LoadingOutlined spin />} size="large" spinning={true} />}

            {
                loading && <Spin indicator={<LoadingOutlined spin />} size="large" className="sticky bottom-0"></Spin>
            }
        </>
    )
}

export default ProductList
