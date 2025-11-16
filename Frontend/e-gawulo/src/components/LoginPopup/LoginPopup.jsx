import React, { useState } from 'react'
import "./LoginPopup.css"
import { assets } from '../../assets/assets'

const LoginPopup = ({setShowLogin}) => {
    const [currentState, setCurrentState] = useState("Login")
  return (
    <div className='login-popup'>
        <form className="login-popup-wrapper">
            <div className="login-popup-title">
                <h2>{currentState}</h2>
                <img onClick={() => setShowLogin(false)} src={assets.cross_icon} alt=''/>
            </div>
            <div className="login-pop-inputs">
                {currentState==="Login"?<></>: <input type="text" placeholder='Your name' required/>}
                <input type="email" placeholder='Your email' required/>
                <input type="password" placeholder="password" required />
            </div>
            <button>{currentState==="Sign Up"? "Create Account": "Login"}</button>
            <div className="login-popup-condition">
                <input type="checkbox" required/>
                <p>By continuing, you agree to the Terms of Service and Privacy Policy and receiving promotional offers.</p>
            </div>
            {currentState==="Sign Up"?<p>Already have an account? <span onClick={()=>setCurrentState('Login')}>Login here</span></p>
            :<p>Create a new account? <span span onClick={()=>setCurrentState('Sign Up')}>Click here</span></p>}
            
            
        </form>
    </div>
  )
}

export default LoginPopup
