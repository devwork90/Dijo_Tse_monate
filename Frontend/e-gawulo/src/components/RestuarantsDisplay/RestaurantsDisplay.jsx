import React from 'react'
import "./restaurantDisplay.css"
import { useContext } from 'react'
import {StoreContext} from '../../context/StoreContext'

const RestaurantDisplay = () => {
    const {food_list} = useContext(StoreContext);
  return (
    <div className='restuarant-display' id="restuarant-display">
        <h2>Popular Food Joints</h2> 
        <div className="reastaurants-display-options">
        {food_list.map((item, index)=>{
            return(
                <div key={index} className="restaurant-display-option-circle"> 
                    <img src={item.image} alt="" />
                    <p>{item.name}</p>
                </div>
            )
        })}
        </div>
        {/* <hr /> */}
    </div>
  )
}

export default RestaurantDisplay;
