import { useEffect, useState } from 'react'
import axios from 'axios'
import { Card, Carousel, Dropdown, Input, InputNumber, MenuProps, Pagination, Space, Typography } from 'antd';
import { Product } from '../interfaces/Product';
import ProductList from '../components/ProductList';
import { DownOutlined } from '@ant-design/icons';

const { Meta } = Card;

const Home: React.FC = () => {
  const [products, setProducts] = useState<Product[]>()
  const items: MenuProps['items'] = [
    {
      key: '1',
      label: 'Item 1',
    },
    {
      key: '2',
      label: 'Item 2',
    },
    {
      key: '3',
      label: 'Item 3',
    },
  ];

  useEffect(() => {
    fetchProducts()
  }, [products?.length])

  const fetchProducts = async () => {
    const { data } = await axios.get("https://67160f3933bc2bfe40bc344a.mockapi.io/api/v1/products")
    setProducts(data)
  }

  return (
    <>
      <div className='Home flex flex-col items-center py-24 gap-5'>
        <div className="min-w-[1024px] max-w-[1024px]">
          <Carousel autoplay>
            <div className='flex justify-center'>
              <img className='mx-auto' src='https://d1csarkz8obe9u.cloudfront.net/posterpreviews/smart-phone-banner-design-template-caa98978d25e965873a22b01acb99ba7_screen.jpg' />
            </div>
            <div className='flex justify-center'>
              <img className='mx-auto' src='https://d1csarkz8obe9u.cloudfront.net/posterpreviews/smart-phone-banner-design-template-caa98978d25e965873a22b01acb99ba7_screen.jpg' />
            </div>
          </Carousel>
        </div>

        <div className="FilterWrapper flex gap-5 items-center">
          <div className="SearchWrapper w-[240px]">
            <Input.Search size="small" placeholder="search here" />
          </div>


          <Dropdown
            menu={{
              items,
              selectable: true,
              defaultSelectedKeys: ['3'],
            }}
          >
            <Typography.Link>
              <Space>
                Category
                <DownOutlined />
              </Space>
            </Typography.Link>
          </Dropdown>

          <Dropdown
            menu={{
              items,
              selectable: true,
              defaultSelectedKeys: ['3'],
            }}
          >
            <Typography.Link>
              <Space>
                Brand
                <DownOutlined />
              </Space>
            </Typography.Link>
          </Dropdown>

          <div className="PriceFilterWrapper flex text-black items-center">
            <InputNumber
              placeholder="min"
              value={0}
              style={{ margin: '0 16px', width: '120px' }}
            />
            -
            <InputNumber
              placeholder="max"
              value={999999999}
              style={{ margin: '0 16px', width: '120px'}}
            />
          </div>

        </div>



        <ProductList products={products} />
        <div className="flex justify-center">
          <Pagination simple defaultCurrent={2} total={products?.length} />
        </div>
      </div>


    </>
  )
}

export default Home
