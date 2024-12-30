import { Button, ConfigProvider } from "antd"

const ButtonWarning: React.FC<any> = ({children, ...props})=>{
    return <ConfigProvider theme={{
        token: {
          colorPrimary: '#f08437'
        },
      }}>
        <Button {...props}>
            {children}
        </Button>
    </ConfigProvider>
}

export default ButtonWarning