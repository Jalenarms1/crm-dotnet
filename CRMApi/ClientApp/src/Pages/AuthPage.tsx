import { useRef, useState } from 'react'
import { API_ROOT } from '../constants';
import loginImg from "../../public/login-img.jpg"


const AuthPage = () => {
    const emailRef = useRef<HTMLInputElement>(null)
    const nameRef = useRef<HTMLInputElement>(null)
    const passRef = useRef<HTMLInputElement>(null)
    const cpassRef = useRef<HTMLInputElement>(null)
    const [emailErr, setEmailErr] = useState<string | null>(null);
    const [nameErr, setNameErr] = useState<string | null>(null);
    const [passErr, setPassErr] = useState<string | null>(null);
    const [cpassErr, setCPassErr] = useState<string | null>(null);
    const [submissionErr, setSubmissionErr] = useState(false)
    const [isLogin, setIsLogin] = useState(true)
  
    const submitLogin = async () => {
      setEmailErr(null)
      setPassErr(null)
      setNameErr(null)
      setCPassErr(null)
      setSubmissionErr(false)


      const email = emailRef.current?.value
      const pass = passRef.current?.value
      if (!email || !pass) {
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!email || !emailRegex.test(email as string)) {
          setEmailErr("Please enter a valid email.")
        }
    
        if (!pass) {
          setPassErr("Please enter a valid password.")
        }
        return
      }
  
      const resp = await fetch(`${API_ROOT}/user/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify({
          email: email,
          password: pass
        })
      })
  
      console.log(resp.status);
      
      if (resp.status !== 200) {
        setSubmissionErr(true)
      }
  
  }

  const submitSignup = async () => {
    setEmailErr(null)
    setPassErr(null)
    setNameErr(null)
    setCPassErr(null)
    setSubmissionErr(false)

    const name = nameRef.current?.value
    const email = emailRef.current?.value
    const pass = passRef.current?.value
    const cpass = cpassRef.current?.value
    let isErr = false

    if (!name || name.length < 2) {
      setNameErr("Please enter a valid name")
      isErr = true
    }


  const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    if (!email || !emailRegex.test(email as string)) {
      setEmailErr("Please enter a valid email.")
      isErr = true
    }

    if (!pass) {
      setPassErr("Please enter a valid password.")
      isErr = true
    }

    if (cpass != pass || !cpass) {
      setCPassErr("Passwords do not match")
      isErr = true
    }
    
    if (isErr) return

    const resp = await fetch(`${API_ROOT}/user/newuser`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        name,
        email: email,
        password: pass
      })
    })

    console.log(resp.status);
    
    if (resp.status !== 200) {
      setSubmissionErr(true)
    }
  
  }

  return (
     <div className='w-full min-h-screen '>
        <div className="w-full flex ">
          <div className="w-[45vw] h-screen relative">
            <img src={loginImg} alt="login-img" className='w-full object-fill h-full' />
            <div className="inset-0 absolute bg-slate-700/90 px-20 py-10">
              <svg className="text-white/50" xmlns="http://www.w3.org/2000/svg" width="50" height="50" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" ><path d="M11 17a4 4 0 0 1-8 0V5a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2Z"/><path d="M16.7 13H19a2 2 0 0 1 2 2v4a2 2 0 0 1-2 2H7"/><path d="M 7 17h.01"/><path d="m11 8 2.3-2.3a2.4 2.4 0 0 1 3.404.004L18.6 7.6a2.4 2.4 0 0 1 .026 3.434L9.9 19.8"/></svg>
              <div className="flex items-center gap-2">
                {/* <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide text-white/50 lucide-book-heart-icon lucide-book-heart"><path d="M16 8.2A2.22 2.22 0 0 0 13.8 6c-.8 0-1.4.3-1.8.9-.4-.6-1-.9-1.8-.9A2.22 2.22 0 0 0 8 8.2c0 .6.3 1.2.7 1.6A226.652 226.652 0 0 0 12 13a404 404 0 0 0 3.3-3.1 2.413 2.413 0 0 0 .7-1.7"/><path d="M4 19.5v-15A2.5 2.5 0 0 1 6.5 2H19a1 1 0 0 1 1 1v18a1 1 0 0 1-1 1H6.5a1 1 0 0 1 0-5H20"/></svg> */}
                <p className="font-semibold text-white/50 text-lg">UpCare</p>
              </div>
            </div>
          </div>
          <div className='p-10 flex flex-col justify-between  w-[55vw]'>
            <a href="/" className="flex text-gray-400 gap-2">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" className="lucide lucide-arrow-left-icon lucide-arrow-left"><path d="m12 19-7-7 7-7"/><path d="M19 12H5"/></svg>
              <p>Back</p>
            </a>
            <div className="flex flex-col gap-7 mx-auto w-96">
              <p className="text-3xl font-semibold">{isLogin ? "Login" : "Sign up"}</p>

             {!isLogin && <div className="flex flex-col gap-1">
                <label htmlFor="email" className="text-sm text-gray-500">Name</label>
                <input ref={nameRef} required name="Email" id="email" type="text" className="border border-gray-200 rounded-sm w-96 p-1 shadow-sm shadow-gray-300 focus:outline-none" />
               {nameErr && <p className="text-sm text-red-500">{nameErr}</p>}
              </div>}

              <div className="flex flex-col gap-1">
                <label htmlFor="email" className="text-sm text-gray-500">Email</label>
                <input ref={emailRef} required name="Email" id="email" type="text" className="border border-gray-200 rounded-sm w-96 p-1 shadow-sm shadow-gray-300 focus:outline-none" />
               {emailErr && <p className="text-sm text-red-500">{emailErr}</p>}
              </div>

              <div className="flex flex-col gap-1">
                <label htmlFor="password" className="text-sm text-gray-500">Password</label>
                <input ref={passRef} required name="Password" id="password" type="password" className="border border-gray-200 rounded-sm w-96 p-1 shadow-sm shadow-gray-300 focus:outline-none" />
                {passErr && <p className="text-sm text-red-500">{passErr}</p>}
              </div>

              {!isLogin && <div>
                <label htmlFor="cpassword" className="text-sm text-gray-500">Confirm Password</label>
                <input ref={cpassRef} required name="ConfirmPassword" id="cpassword" type="password" className="border border-gray-200 rounded-sm w-96 p-1 shadow-sm shadow-gray-300 focus:outline-none" />
                {cpassErr && <p className="text-sm text-red-500">{cpassErr}</p>}
              </div>}

              <div className="flex flex-col gap-5">
                {submissionErr && <p className='text-xs text-red-500 text-center'>{isLogin ? "Please make sure you have an existing account and have entered the right credentials" : "Please make sure you have provided a valid email address that is not associated with an existing account"}.</p>}
                <button onClick={isLogin ? submitLogin : submitSignup} className='cursor-pointer w-full bg-slate-700 text-white font-semibold py-2 rounded-sm active:opacity-75'>Continue</button>

              </div>

            
              <div className="w-60 mx-auto flex flex-col gap-10">
                <p className="text-center text-xs">By continuing, you agree to our <a href="" className='underline text-blue-500'>Terms of Service</a> and <a href="" className='underline text-blue-500'>Privacy Policy</a></p>

                <p className='text-xs'>{isLogin ? "Need to create an account" : "Already have an account"}? Click <button onClick={(() => setIsLogin(l => !l))} className='text-blue-500 underline'>here</button></p>
              </div>
            </div>
            <div className='h-1 w-full'></div>
          </div>

        </div>
    </div>
  )
}

export default AuthPage
