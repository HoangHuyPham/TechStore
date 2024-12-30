import AppSteps from "../../components/AppSteps"
import CheckoutList from "../../components/CheckoutList"

const Checkout: React.FC = () => {
  return (
    <>
      <AppSteps currentStep={1} />
      <div className='Checkout flex flex-col items-center px-24 gap-2'>
          <CheckoutList/>
      </div>
    </>
  )
}

export default Checkout
