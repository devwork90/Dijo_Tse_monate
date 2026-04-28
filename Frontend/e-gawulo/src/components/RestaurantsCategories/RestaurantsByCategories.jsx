import React from 'react'
import {assets} from '../../assets/assets'
import "./restaurantsByCategories.css"
import {getRestaurantsByCategories} from '../../api/restaurantByCategoriesApi'

  const RestaurantsByCategories = ({category, setSelectedRestaurant}) => {
  const [restaurants, setRestaurants] = React.useState([]);
  const [loading, setLoading] = React.useState(true);
  const [error, setError] = React.useState(null);
  
  React.useEffect(() => {
    const fetchRestaurants = async () => {
      setLoading(true);
      try {
        const data = await getRestaurantsByCategories(category);
        setRestaurants(data.restaurants);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    if (category) {
      fetchRestaurants();
    }
  }, [category]);


  return (
      <div className='restaurant-category'>
        <h2>{category} Restaurants</h2>
        {loading && <p>Loading...</p>}
        {error && <p>Error: {error}</p>}
        <div className='restaurant-category-list'>
          {restaurants.map((item, index) => {
            return (
              <>
              <div className='restaurant-category-item' onClick={() => setSelectedRestaurant(item.id)} key={index}>
                <img className={category===item.name?"active": ""} src={item.restaurantIconImage.filePath} alt=''/>
                <p className='restaurant-category-name'><strong>{item.name}</strong></p>
              </div>  
              </>
            )
          })}
        </div>
        {/* <hr /> */}
      </div>
  )
}

export default RestaurantsByCategories
