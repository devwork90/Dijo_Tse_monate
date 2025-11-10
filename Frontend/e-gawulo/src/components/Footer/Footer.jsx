import React from 'react'
import "./footer.css"
import { assets } from '../../assets/assets'

const footer = () => {
  return (
    <div className='footer' id="footer">
      <div className="footer-content">
        <div className="footer-content-left">
          <img src={assets.logo_2} alt="E-Gawulo Logo" className='footer-logo' />
          {/* <p>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Eaque voluptatibus repudiandae dolor nostrum debitis. Dolorem delectus cupiditate perspiciatis officia, veniam reiciendis, corporis asperiores rem ipsum a doloribus omnis vero eaque.</p> */}
        
          <div className="footer-social-icons">
            <img src={assets.facebook_icon} alt="" />
            <img src={assets.twitter_icon} alt="" />
            <img src={assets.facebook_icon} alt="" />
          </div>
        </div>
        <dev className="footer-content-center">
          <h2>COMPANY</h2>
          <li>Home</li>
          <li>About us</li>
          <li>Delivery</li>
          <li>Private policy</li>
        </dev>

        <div className="footer-content-right">
          {/* <p>© 2024 E-Gawulo. All rights reserved.</p> */}
          <h2>CONTACT US</h2>
          <ul>
            <li>+123 456 7890</li>
            <li>contact@e-gawlo.com</li>
            <li></li>
          </ul>
        </div>
      </div>
      <hr/>
      <p className="footer-copyright">
        © 2025 E-Gawulo. All rights reserved.
      </p>
    </div>
  )
}

export default footer
