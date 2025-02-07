
import AppSteps from "../../components/AppSteps"
import { useCheckoutContext } from "../../contexts/hooks"
import { Button, Divider, Input, message } from "antd"
import TextArea from "antd/es/input/TextArea"
import { useNavigate, useParams } from "react-router-dom"
import { Endpoint, post } from "../../request/AppRequest"
import { useState } from "react"
import { toast } from "react-toastify"
import { CheckCircleFilled } from "@ant-design/icons"

const Order: React.FC = () => {
  const params = useParams()
  const navigate = useNavigate()
  const { checkoutItems, dispatchCheckoutItems } = useCheckoutContext()
  const [checkoutLoading, setCheckoutLoading] = useState(false)
  const [note, setNote] = useState()
  const [successOrder, setSuccessOrder] = useState(false)
  const [orderId, setOrderId] = useState()
  const [createdOn, setCreatedOn] = useState()
  const [appStep, setAppStep] = useState(2)

  const handleOrder = async () => {
    setCheckoutLoading(true)
    try {
      const { status, data } = await post({
        url: Endpoint.ORDER_URL,
        payload: {
          description: note,
          orderTypeId: "f7e5cbee-e5de-4a7d-b688-5e56fdf96573", //pending
          voucherId: params.voucherId,
          items: checkoutItems.map(v => v.id)
        }
      })

      if (status === 200) {
        toast.success("Place order success")
        setAppStep(3)
        setOrderId(data.id)
        setCreatedOn(data.createdOn)
        setSuccessOrder(true)
      } else {
        toast.error(`Error: ${data}`)
        setSuccessOrder(false)
      }

    } catch (err) {
      message.error(`Error: ${err}`)
    } finally {
      setTimeout(() => {
        setCheckoutLoading(false)
      }, 200);
    }
  }
  return (
    <>
      <AppSteps currentStep={appStep} />
      <div className='Order flex flex-col items-center px-24 py-5 gap-2'>
        {
          (checkoutItems.length > 0) && (!successOrder && (<div className="p-5 text-black bg-white rounded-md shadow-md animate-fade">
            <p className="text-2xl text-center">Place order</p>
            <Divider orientation="left" style={{ borderColor: '#000000' }} plain>Product</Divider>
            {checkoutItems && checkoutItems.map((v, i) => <p key={v.id} className="text-sm"> {i + 1}. {v.product.name} ({v.productOption?.name}) x{v.quantity}</p>)}
            {params.code && <p className="text-sm">Voucher: <span>{params.code}</span></p>}
            <Divider orientation="left" style={{ borderColor: '#000000' }} plain></Divider>
            <Divider orientation="left" style={{ borderColor: '#000000' }} plain>Extra</Divider>
            <TextArea rows={4} placeholder="Note here" onChange={e => setNote(e.target.value)} value={note} />
            <div className="flex justify-center p-2">
              <span className="flex gap-2">
                <Button onClick={() => navigate(-1)}> Back</Button>
                <Button type="primary" onClick={handleOrder} loading={checkoutLoading}> Confirm</Button>
              </span>
            </div>
          </div>) || (<div className="p-5 text-black bg-white rounded-md shadow-md animate-fade">
            <p className="text-2xl text-center">Place order success <CheckCircleFilled style={{color: "green"}} /></p>
            <p>Thank you, Have a good day!</p>
            <p>Please wait for minutes, we will contact with you as soon as possible</p>
            <p><span className="font-bold">Your order ID:</span> {orderId}</p>
            <p><span className="font-bold">Created at:</span> {createdOn}</p>
            <p><span className="font-bold">Hotline:</span> 123456789</p>
            <span className="flex justify-center"><Button onClick={() => navigate("/home")}> Back</Button></span>
          </div>)) || (<>
            <p className="text-black">No item to order :(</p>
            <Button onClick={() => navigate(-1)} type="primary">Go back</Button>
          </>)
        }
      </div>
    </>
  )
}

export default Order
