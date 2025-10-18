import React from 'react'
import "./Header.css"

const Header = () => {
  return (
    <div className='header'>
      <div className='header-contents'>
        <h2>Ekse! Order from you favourite restaurants</h2>
        <p>From your kasi favorites to top restaurants — good food’s just a tap away!</p>
        <button>View Resturants</button>
        <div className='overlay'></div>
      </div>
    </div>
  )
}

export default Header
