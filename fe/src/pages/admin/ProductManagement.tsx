import { Button, Tag, Divider, Flex, Image, Input, InputRef, message, Modal, Select, Spin, Table, theme, Tooltip } from "antd"
import { IImage, IOrder, IProductItem, IUser } from "../../interfaces";
import { useEffect, useRef, useState } from "react";
import { useUserContext } from "../../contexts/hooks";
import { del, Endpoint, get, post } from "../../request/AppRequest";
import { toast } from "react-toastify";
import { USER_ACTION } from "../../contexts/UserContext";
import { LoadingOutlined, PlusOutlined, UploadOutlined, UserOutlined } from "@ant-design/icons";
import { useNavigate } from "react-router-dom";

const ProductManagement: React.FC = () => {
    const [products, setProducts] = useState<IProductItem[]>([])
    const [loading, setLoading] = useState(false)
    const { user, dispatchUser } = useUserContext()
    const [isModalOpen, setIsModalOpen] = useState(false);

    useEffect(() => {
        fetchProducts()
    }, [user])

    const columns = [
        {
            title: 'Id',
            dataIndex: 'id',
            key: 'id',
            render: (text) => text
        },
        {
            title: 'Name',
            dataIndex: 'name',
            key: 'name',
            render: (text) => text || "No Note"
        },
        {
            title: 'Thumbnail',
            dataIndex: 'thumbnail',
            key: 'thumbnail',
            render: (thumbnail) => <Image
                preview={false}
                key={thumbnail?.id}
                width={64}
                height={64}
                src={thumbnail?.url}
                fallback="data:image/png;base64,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"
            />
        },
        {
            title: 'Category',
            dataIndex: 'category',
            key: 'category',
            render: (category) => category?.name
        },
        {
            title: 'Description',
            dataIndex: 'productDetail',
            key: 'productDetail',
            render: (productDetail) => productDetail?.description
        },
        {
            title: 'Stock',
            dataIndex: 'productDetail',
            key: 'productDetail',
            render: (productDetail) => productDetail?.stock
        },
        {
            title: 'Before price',
            dataIndex: 'productDetail',
            key: 'productDetail',
            render: (productDetail) => productDetail?.beforePrice
        },
        {
            title: 'Price',
            dataIndex: 'productDetail',
            key: 'productDetail',
            render: (productDetail) => productDetail?.price
        },

        {
            title: 'Action',
            key: 'action',
            render: (_, record) => (
                <>
                    <span className="flex gap-2">
                        <Button type="primary" onClick={() => handleShowOrder(record)}>
                            ...
                        </Button>
                        <Button type="primary" danger onClick={() => handleRemoveUser(record)}>
                            X
                        </Button>
                    </span>
                </>
            ),
        },
    ];

    const handleShowAddNew = () => {
        setIsModalOpen(true);
    };

    const handleOk = async () => {
        try {
            const { status, data } = await post({
                url: Endpoint.PRODUCT_URL,
                payload: {
                    "name": name,
                    "thumbnailId": thumbnail?.id,
                    "categoryId": category,
                    "productDetail": {
                        "description": description,
                        "stock": stock,
                        "beforePrice": beforePrice,
                        "price": price,
                        "totalRating": 0,
                        "previews": [],
                        "productOptions": options?.length > 0 && options.map(x=> {
                            return {
                                name: x, 
                                isSelected: true
                            }
                        }) || []
                    }
                }
            })
            if (status === 200) {
                message.success(`Add product ${data.id} success.`)
                fetchProducts()
            } else {
                message.error(`Add product failed.`)
                message.error(data)
            }
        } catch (error) {
            message.error(`Error: ${error}`)
        }
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        setIsModalOpen(false);
    };

    const handleRemoveUser = async (product: IOrder) => {
        try {
            const { status, data } = await del({
                url: Endpoint.ORDER_URL + "/" + product.id
            })
            if (status === 200) {
                message.success(`Remove product ${product.id} success.`)
                fetchProducts()
            } else {
                message.error(`Remove product ${product.id} failed.`)
                message.error(data)
            }
        } catch (error) {
            message.error(`Error: ${error}`)
        }
    }

    const navigate = useNavigate()
    const fetchUser = async () => {
        try {
            const { status, data } = await get({
                url: Endpoint.PROFILE_URL
            })

            if (status === 200) {
                dispatchUser({
                    type: USER_ACTION.ADD,
                    payload: data as IUser
                })

                const { role } = data as IUser
                if (role?.name !== "Admin"){
                    navigate("/forbidden")
                }
            } else {
                toast.error(`Error: ${data}`)
            }
        } catch (error) {
            console.error(`Error: ${error}`)
        }
    }
    const fetchProducts = async () => {
        if (!user) {
            await fetchUser()
        }
        setLoading(true)
        try {
            const { status, data }: { status: number, data: IProductItem[] } = await get({
                url: Endpoint.PRODUCT_URL
            })

            if (status === 200) {
                setProducts(data?.items)
            } else {
                message.error(`Error: ${data}`)
            }

        } catch (error) {
            console.error(`Error: ${error}`)
        } finally {
            setTimeout(() => {
                setLoading(false)
            }, 200);
        }
    }

    const uploadThumbnail = async (e) => {
        const f = e.target.files[0]

        try {
            const { status, data } = await uploadToServer(f)

            if (status === 200) {
                message.success("upload success.")
                setThumbnail(data)
            } else {
                message.error(`Error ${data}`)
            }
        } catch (error) {
            message.error(`Error ${error}`)
        }
    }

    const uploadToServer = (file) => {
        const form = new FormData();
        form.append("file", file)
        return post({
            url: Endpoint.UPLOAD_URL,
            payload: form
        })
    }

    const [name, setName] = useState<string>()
    const [thumbnail, setThumbnail] = useState<IImage>()
    const [category, setCategory] = useState<string>("b0326961-b2c9-4681-8e36-d892cc1ca7ed")
    const [description, setDescription] = useState<string>()
    const [stock, setStock] = useState<number>()
    const [beforePrice, setBeforePrice] = useState<number>()
    const [price, setPrice] = useState<number>()
    

    //options
    const tagInputStyle: React.CSSProperties = {
        width: 64,
        height: 22,
        marginInlineEnd: 8,
        verticalAlign: 'top',
    };

    const { token } = theme.useToken();
    const [options, setOptions] = useState<string[]>([])
    const [inputVisible, setInputVisible] = useState(false);
    const [inputValue, setInputValue] = useState('');
    const [editInputIndex, setEditInputIndex] = useState(-1);
    const [editInputValue, setEditInputValue] = useState('');
    const inputRef = useRef<InputRef>(null);
    const editInputRef = useRef<InputRef>(null);

    useEffect(() => {
        if (inputVisible) {
            inputRef.current?.focus();
        }
    }, [inputVisible]);

    useEffect(() => {
        editInputRef.current?.focus();
    }, [editInputValue]);

    const handleClose = (removedTag: string) => {
        const newTags = tags.filter((tag) => tag !== removedTag);
        console.log(newTags);
        setTags(newTags);
    };

    const showInput = () => {
        setInputVisible(true);
    };

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(e.target.value);
    };

    const handleInputConfirm = () => {
        if (inputValue && !options.includes(inputValue)) {
            setOptions([...options, inputValue]);
        }
        setInputVisible(false);
        setInputValue('');
    };

    const handleEditInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEditInputValue(e.target.value);
    };

    const handleEditInputConfirm = () => {
        const newOptions = [...options];
        newOptions[editInputIndex] = editInputValue;
        setOptions(newTags);
        setEditInputIndex(-1);
        setEditInputValue('');
    };

    const tagPlusStyle: React.CSSProperties = {
        height: 22,
        background: token.colorBgContainer,
        borderStyle: 'dashed',
    };


    return <>
        <div className="w-[100%] animate-fade">
            <Modal title={"Add new product"} open={isModalOpen} onOk={handleOk} onCancel={handleCancel}>
                <span className="flex flex-col gap-2">
                    <Divider>Product</Divider>
                    <Input addonBefore="Name" onChange={e => setName(e.target.value)} />
                    <span><Input addonBefore="Thumbnail" value={thumbnail?.url} onChange={e => setThumbnail(e.target.value)} /><Button loading={false} onClick={() => document.getElementById("upload-inp")?.click()} icon={<UploadOutlined />}>Add image</Button></span>
                    <input className="hidden" id="upload-inp" type="file" onChange={uploadThumbnail}></input>
                    <span>Category: <Select
                        onChange={v => setCategory(v)}
                        defaultValue={category}
                        style={{ width: 120 }}
                        options={[
                            { value: '442f967f-1aa0-4aeb-8d35-c94fea58c98f', label: 'Smart Phone' },
                            { value: '54733eda-9e2f-4675-a25c-1e46a5d4a347', label: 'Tablet' },
                            { value: '0241b743-ca16-4c25-96da-a2cf73dc008e', label: 'Laptop' },
                            { value: 'e75c26e7-ce66-4fd7-a55e-fee9eb20b3a4', label: 'Smart Watch' },
                            { value: 'b0326961-b2c9-4681-8e36-d892cc1ca7ed', label: 'Other' },
                        ]}
                    /></span>
                    <Divider>Product detail</Divider>
                    <Input addonBefore="Description" onChange={e => setDescription(e.target.value)} />
                    <Input type="number" addonBefore="Stock" onChange={e => setStock(e.target.value)} />
                    <Input type="number" addonBefore="Before Price" onChange={e => setBeforePrice(e.target.value)} />
                    <Input type="number" addonBefore="Price" onChange={e => setPrice(e.target.value)} />
                    <Flex gap="4px 4px" wrap>
                        <span className="font-bold">Options: </span>
                        {options.map<React.ReactNode>((option, index) => {
                            if (editInputIndex === index) {
                                return (
                                    <Input
                                        ref={editInputRef}
                                        key={option}
                                        size="small"
                                        style={tagInputStyle}
                                        value={editInputValue}
                                        onChange={handleEditInputChange}
                                        onBlur={handleEditInputConfirm}
                                        onPressEnter={handleEditInputConfirm}
                                    />
                                );
                            }
                            const isLongTag = option.length > 20;
                            const tagElem = (
                                <Tag
                                    key={option}
                                    closable={true}
                                    style={{ userSelect: 'none' }}
                                    onClose={() => handleClose(option)}
                                >
                                    <span
                                        onDoubleClick={(e) => {
                                                setEditInputIndex(index);
                                                setEditInputValue(option);
                                                e.preventDefault();
                                        }}
                                    >
                                        {isLongTag ? `${option.slice(0, 20)}...` : option}
                                    </span>
                                </Tag>
                            );
                            return isLongTag ? (
                                <Tooltip title={option} key={option}>
                                    {tagElem}
                                </Tooltip>
                            ) : (
                                tagElem
                            );
                        })}
                        {inputVisible ? (
                            <Input
                                ref={inputRef}
                                type="text"
                                size="small"
                                style={tagInputStyle}
                                value={inputValue}
                                onChange={handleInputChange}
                                onBlur={handleInputConfirm}
                                onPressEnter={handleInputConfirm}
                            />
                        ) : (
                            <Tag style={tagPlusStyle} icon={<PlusOutlined />} onClick={showInput}>
                                New Options
                            </Tag>
                        )}
                    </Flex>

                </span>
            </Modal>
            <p className="text-center p-5">Product (General)</p>
            <Spin spinning={loading} indicator={<LoadingOutlined />}>
                <Button type="primary" className="mb-5" danger onClick={() => handleShowAddNew()}>New</Button>
                <Table dataSource={products} columns={columns} pagination={false} />
            </Spin>
        </div>
    </>
}

export default ProductManagement