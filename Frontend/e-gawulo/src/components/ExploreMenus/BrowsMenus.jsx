import React from 'react'
import "./BrowsMenus.css"
import { restaurants_list } from '../../assets/assets'

const BrowsMenus = ({category, setCategory}) => {

  return (
    <div className="explore-menus" id="explore-res-menus">
      <h2>Explore Our Menus</h2>
      <div className='explore-menus-list'>
        {restaurants_list.map((item, index) => {
          return (
            <div onClick={()=>setCategory(prev=>prev===item.name?"All":item.name)} key={index} className='explore-menus-list-item'>
              <img className={category===item.name?"active": ""} src ={item.menu_icon} alt=''/>
              <p>{item.name}</p>
            </div>
          )
        })}
      </div>
      {/* <hr /> */}
    </div>
  )
}

export default BrowsMenus
