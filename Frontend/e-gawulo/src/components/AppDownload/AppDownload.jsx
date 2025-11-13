import React from 'react'
import "./AppDownload.css"
import { assets } from '../../assets/assets'

const AppDownload = () => {
  return (
    <div className='app-download' id="app-download">
      <div className="app-download-image">
          <h2>📱 Mobile App Coming Soon!</h2>
          <img className='mobile-icon' src={assets.mobile_icon} alt="Mobile App Preview" />
        </div>
      <div className="app-download-platforms">
        <img src={assets.play_store} alt=""/>
        <img  src={assets.app_store} alt=""/>
      </div>
      <hr/>
    </div>
  )
}

export default AppDownload
