import { Button, Checkbox, Divider, Form, FormProps, Input } from 'antd';
import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Endpoint, post, updateAppRequest } from '../../request/AppRequest';
import { toast } from 'react-toastify';
import RememberMe from '../../components/RememberMe';
import JWTUtil from '../../helper/JWTUtil';
import { KeyOutlined, MailOutlined } from '@ant-design/icons';

type FieldType = {
  username?: string;
  password?: string;
  remember?: string;
};

const Login: React.FC = () => {
  const [username, setUsername] = useState<string>();
  const [password, setPassword] = useState<string>();
  const [loading, setLoading] = useState<boolean>();
  const navigate = useNavigate();

  useEffect(() => {
    if (localStorage.getItem("rememberMe") === "true") {
      if (JWTUtil.isValidJWT()) {
        navigate("/home")
      }
    }
  }, [])

  const handleLogin = async () => {
    setLoading(true)
    try {
      const { data, status } = await post({
        url: Endpoint.LOGIN_URL,
        payload: {
          email: username,
          password
        }
      })

      if (status === 200) {
        localStorage.setItem("jwt", data)
        updateAppRequest()
        navigate("/home")
      } else {
        toast.error(`Error ${data}`)
      }
    } catch (error) {
      toast.error(`Error ${error}`)
    } finally {
      setLoading(false)
    }
  }

  return (
    <>
      <div className='Login flex flex-col items-center p-5 gap-5 animate-fade'>
        <div className="LoginContainer rounded bg-white flex">
          <p className='flex text-white text-center text-3xl bg-blue-500 px-5 py-2'>Login</p>
          <Form
            className="px-10 py-2"
            style={{ maxWidth: 600 }}
            autoComplete="off"
            layout={"vertical"}
          >

            <div className="flex flex-col gap-2 p-2">
              <Input style={{ width: 240 }} type={"email"} prefix={<MailOutlined />} placeholder={"Email"} onChange={(e => setUsername(e.target.value))} value={username}/>
              <Input.Password style={{ width: 240 }} prefix={<KeyOutlined />} placeholder={"Password"} visibilityToggle={false} onChange={(e => setPassword(e.target.value))} value={password}/>
              <RememberMe />
            </div>

            <div className="flex flex-col justify-center items-center mt-5">
              <Button loading={loading} type="primary" htmlType="submit" onClick={handleLogin}>
                Login
              </Button>
              <Divider style={{ borderColor: '#000000' }} variant="solid" plain>No have account ?</Divider>
              <Link className='text-orange-700' to={"/register"}>Sign up</Link>
            </div>
          </Form>
        </div>
      </div>
    </>
  )
}

export default Login
