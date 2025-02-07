import { Button, Divider, Form, Input, Radio, RadioChangeEvent, Space, Spin, Typography } from 'antd';
import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Config, names, uniqueNamesGenerator } from 'unique-names-generator';
import RememberMe from '../../components/RememberMe';
import { BorderlessTableOutlined, CheckOutlined, CiCircleFilled, KeyOutlined, LoadingOutlined, MailOutlined, RedoOutlined, RightOutlined, UserOutlined } from '@ant-design/icons';
import { useDebounce } from '../../custom/hooks';
import { Endpoint, get, post } from '../../request/AppRequest';
import { toast } from 'react-toastify';

const config: Config = {
  dictionaries: [names],
  style: 'capital'
}

const Register: React.FC = () => {
  const [name, setName] = useState<string>(uniqueNamesGenerator(config))
  const [email, setEmail] = useState<string>()
  const [password, setPassword] = useState<string>()
  const [confirmPassword, setConfirmPassword] = useState<string>()
  const [emailExisted, setEmailExisted] = useState(false)
  const [emailLoading, setEmailLoading] = useState(false)
  const [instruction, setInstruction] = useState<string>()
  const [gender, setGender] = useState(true)
  const emailDebounced = useDebounce(email, 500)
  const navigate = useNavigate()

  useEffect(() => {
    checkEmailExisted()
  }, [emailDebounced])

  const checkEmailExisted = async () => {
    setEmailLoading(true)
    try {
      const { status, data } = await post({
        url: Endpoint.CHECK_EMAIL_URL,
        payload: {
          email: emailDebounced
        }
      })

      if (status === 200) {
        if (data) {
          setInstruction("Email is existed.")
          setEmailExisted(true)
        } else {
          setInstruction("")
          setEmailExisted(false)
        }
      }
    } catch (err) {
      console.error(err)
    } finally {
      setTimeout(() => {
        setEmailLoading(false)
      }, 200);
    }

  }

  const handleRegister = async () => {
    setInstruction("")
    if (password !== confirmPassword) {
      setInstruction("Confirm password and password is not equal.")
      return
    }

    if (!emailExisted) {
      const { status, data } = await post({
        url: Endpoint.REGISTER_URL,
        payload: {
          name,
          email,
          password,
          gender
        }
      })

      if (status === 200){
        toast.success("Register success")
        navigate("/login")
      }else{
        toast.error(`Error ${data}`)
      }
    }
  }

  const handleGender = (e: RadioChangeEvent)=>{
    setGender(e.target.value)
  }

  return (
    <>
      <div className='Register flex flex-col items-center p-5 gap-5 animate-fade'>
        <div className="RegisterContainer rounded bg-white flex">
          <p className='flex text-white text-center text-3xl bg-orange-500 px-5 py-2'>Register</p>
          <Form
            className="px-12 py-2"
            style={{ maxWidth: 600 }}
            autoComplete="off"
            layout={"vertical"}

          >
            <div className="flex flex-col gap-2 p-2">
              {instruction && <p className='text-red-500'>{instruction}</p>}
              <Space style={{ width: 240 }}>
                <Input prefix={<BorderlessTableOutlined />} placeholder={"Name"} onChange={(e => setName(e.target.value))} value={name} />
                <Button onClick={e => { setName(uniqueNamesGenerator(config)) }} icon={<RedoOutlined />} />
              </Space>
              <Input loading={true} status={emailExisted && "warning"} style={{ width: 240 }} type={"email"} prefix={<MailOutlined />} suffix={emailLoading && <Spin indicator={<LoadingOutlined spin />} size="small" />} placeholder={"Email"} onChange={(e => setEmail(e.target.value))} value={email} />
              <Input.Password style={{ width: 240 }} prefix={<KeyOutlined />} placeholder={"Password"} visibilityToggle={false} onChange={(e => setPassword(e.target.value))} value={password} />
              <Input.Password style={{ width: 240 }} prefix={<CheckOutlined />} placeholder={"Confirm Password"} visibilityToggle={false} onChange={(e => setConfirmPassword(e.target.value))} value={confirmPassword} />
              <Radio.Group value={gender} onChange={handleGender}>
                <div className="flex justify-center">
                  <Radio value={true}>Male</Radio>
                  <Radio value={false}>Female</Radio>
                </div>
              </Radio.Group>
              <RememberMe />
            </div>

            <div className="flex flex-col justify-center items-center">
              <button onClick={handleRegister} className="bg-orange-400 text-white hover:bg-red-200 rounded-md px-3 py-1">
                Register
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

export default Register
