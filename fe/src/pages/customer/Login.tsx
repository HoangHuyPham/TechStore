import { Button, Checkbox, Divider, Form, FormProps, Input } from 'antd';
import { Link } from 'react-router-dom';

const Login: React.FC = () => {
  
  type FieldType = {
    username?: string;
    password?: string;
    remember?: string;
  };

  const onFinish: FormProps<FieldType>['onFinish'] = (values) => {
    console.log('Success:', values);
  };

  const onFinishFailed: FormProps<FieldType>['onFinishFailed'] = (errorInfo) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <>
      <div className='Login flex flex-col items-center p-5 gap-5 animate-fade'>
        <div className="LoginContainer rounded bg-white flex">
          <p className='flex text-white text-center text-3xl bg-blue-500 px-5 py-2'>Login</p>
          <Form
            className="px-10 py-2"
            name="basic"
            style={{ maxWidth: 600 }}
            initialValues={{ remember: false }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
            layout={"vertical"}
          >
            <Form.Item<FieldType>
              label="Username"
              name="username"
              rules={[{ required: true, message: 'Please input your username!' }]}
            >
              <Input />
            </Form.Item>

            <Form.Item<FieldType>
              label="Password"
              name="password"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item<FieldType> name="remember" valuePropName="checked" label={null}>
              <Checkbox>Remember me</Checkbox>
            </Form.Item>
            <div className="flex flex-col justify-center items-center">
              <Button type="primary" htmlType="submit">
                Login
              </Button>
              <Divider style={{ borderColor: '#000000' }} variant="solid" plain>No have account ?</Divider>
              <Link className='text-orange-700' to={"/signup"}>Sign up</Link>
            </div>
          </Form>
        </div>
      </div>
    </>
  )
}

export default Login
