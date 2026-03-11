import React from 'react'
import {asserts, assets} from '../../assets/assets'
import "./restaurantsByCategories.css"
function restaurantsByCategories() {
  return (
      <div className='restaurant-category'>
        <div className='restaurant-category-img-container'>
          <img className='restaurant-category-img'  src={image} alt=''/>
        </div>
        <div className="restaurant-category-item-info">
          <div className='restaurant-item-name-rating'>
            <p>{name}</p>
            <img src={assets.rating_starts} alt=''/>

          </div>
        </div>
      </div>
  )
}

export default restaurantsByCategories
