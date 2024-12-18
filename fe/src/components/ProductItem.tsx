import { ShoppingCartOutlined } from "@ant-design/icons";
import { Badge, Button, Card, Rate } from "antd";
import Meta from "antd/es/card/Meta";
import { Product } from "../interfaces/Product";

type ProductItemProps = {
    product:Product
}

const ProductItem:React.FC<ProductItemProps> = ( {product} )=>{
    return (
        <div className="ProductItemWrapper md:max-w-[180px] lg:max-w-[200px]">
            <Badge.Ribbon text="News">
              <Card
                key={product.id}
                hoverable
                cover={<img lazy-loading alt={product?.name} src={product?.thumbnail} />}
              >
                <Meta title={product?.name} />
                <div className="Price flex gap-2">
                  <div className="BeforePrice text-red-600 text-2xl">{product?.productDetail_beforePrice}đ</div>
                  <div className="AfterPrice line-through">{product?.productDetail_beforePrice}đ</div>
                </div>
                <div className="flex justify-between items-center">
                  <Rate disabled defaultValue={product?.productDetail_totalRating} />
                  <Button><ShoppingCartOutlined /></Button>
                </div>

              </Card>
            </Badge.Ribbon>
        </div>
    )
}

export default ProductItem;