import React, { useEffect, useState } from 'react'
import "./BrowsMenus.css"
// import { restaurants_list } from '../../assets/assets'
import { getAllMenus } from '../../api/menuApi'

const BrowsMenus = ({category, setCategory, setShowFilteredRestaurants}) => {
const [menus, setMenus] = useState([]);
const [loading, setLoading] = useState(true)
const [error, setError] = useState(null);

useEffect(() =>{
  async function loadMenus(){
    try{
      const response = await getAllMenus()
      setMenus(response.menuList)
    }catch (err){
      setError(err.message);
    }finally{
      setLoading(false)
    }
  }
  loadMenus()
}, [])
  return (
    <div className="explore-menus" id="explore-res-menus">
      <h2>Explore Our Menus</h2>
      <div className='explore-menus-list'>
        {menus.map((item, index) => {
          return (
            <div onClick={()=>setCategory(prev=>prev===item.name?setShowFilteredRestaurants(false):item.name)} key={index} className='explore-menus-list-item'>
              <img className={category===item.name?"active": ""} src ={item.menuIcon.filePath} alt=''/>
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
