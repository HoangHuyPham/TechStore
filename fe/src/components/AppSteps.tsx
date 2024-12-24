import { Steps } from "antd"

type AppStepProps = {
    currentStep: number,
}

const AppSteps: React.FC<AppStepProps> = ( {currentStep} ) => {
    return (
        <>
            <Steps
                className="flex pt-24 pb-12 px-24"
                current = {currentStep}
                items={[
                    {
                        title: 'Cart',
                    },
                    {
                        title: 'Checkout',
                    },
                    {
                        title: 'Payment',
                    },
                ]}
            />
        </>
    )
}

export default AppSteps