import { Checkbox, Divider, Form, FormProps, Input } from 'antd';
import { Link } from 'react-router-dom';

const Signup: React.FC = () => {
  type FieldType = {
    username?: string;
    password?: string;
    confirmPassword?: string;
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
      <div className='Signup flex flex-col items-center py-24 gap-5'>
        <div className="SignupContainer rounded bg-white flex">
          <p className='flex text-white text-center text-3xl bg-orange-500 px-5 py-2'>Signup</p>
          <Form
            className="px-12 py-2"
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
              <Input.Password visibilityToggle = {false}/>
            </Form.Item>

            <Form.Item<FieldType>
              label="Confirm password"
              name="confirmPassword"
              rules={[{ required: true, message: 'Please input your password!' }]}
            >
              <Input.Password visibilityToggle = {false} />
            </Form.Item>

            <Form.Item<FieldType> name="remember" valuePropName="checked" label={null}>
              <Checkbox>Remember me</Checkbox>
            </Form.Item>
            <div className="flex flex-col justify-center items-center">
              <button className="bg-orange-400 text-white hover:bg-red-200 rounded-md px-3 py-1">
                Signup
              </button>
              <Divider style={{ borderColor: '#000000' }} variant="solid" plain>Have account ?</Divider>
              <Link className='text-orange-700' to={"/login"}>Login</Link>
            </div>
          </Form>
        </div>
      </div>
    </>
  )
}

export default Signup
