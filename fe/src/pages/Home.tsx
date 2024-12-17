import { useEffect, useState } from 'react'
import axios from 'axios'
import { Badge, Button, Card, Carousel, Pagination, Rate } from 'antd';
import { Product } from '../interfaces/Product';
import { ShoppingCartOutlined } from '@ant-design/icons';

const { Meta } = Card;

const Home: React.FC = () => {
  const [products, setProducts] = useState<Product[]>()
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    fetchProducts()
    setLoading(false)
  }, [products?.length])

  const fetchProducts = async () => {
    const { data } = await axios.get("https://67160f3933bc2bfe40bc344a.mockapi.io/api/v1/products")
    setProducts(data)
  }

  return (
    <>


      <div className='Home flex py-24 px-32 flex-wrap gap-5 justify-center'>

        <div className="min-w-[1024px] px-32">
          <Carousel autoplay>
            <div className='flex justify-center'>
              <img className='mx-auto' src='https://d1csarkz8obe9u.cloudfront.net/posterpreviews/smart-phone-banner-design-template-caa98978d25e965873a22b01acb99ba7_screen.jpg' />
            </div>
            <div className='flex justify-center'>
              <img className='mx-auto' src='https://d1csarkz8obe9u.cloudfront.net/posterpreviews/smart-phone-banner-design-template-caa98978d25e965873a22b01acb99ba7_screen.jpg' />
            </div>
          </Carousel>
        </div>

        {
          products?.map((v, i) => (
            <Badge.Ribbon text="News">
              <Card
                key={i}
                hoverable
                loading={loading}
                className='w-[240px]'
                cover={<img lazy-loading alt={v?.name} src={v?.thumbnail} />}
              >
                <Meta title={v?.name} />
                <div className="Price flex gap-2">
                  <div className="BeforePrice text-red-600 text-2xl">{v?.productDetail_beforePrice}đ</div>
                  <div className="AfterPrice line-through">{v?.productDetail_beforePrice}đ</div>
                </div>
                <div className="flex justify-between items-center">
                  <Rate disabled defaultValue={v?.productDetail_totalRating} />
                  <Button><ShoppingCartOutlined /></Button>
                </div>

              </Card>
            </Badge.Ribbon>
          ))
        }
      </div>

      <div className="flex justify-center mb-5">
        <Pagination simple defaultCurrent={2} total={products?.length} />
      </div>
    </>
  )
}

export default Home
