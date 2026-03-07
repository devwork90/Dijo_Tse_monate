import React from 'react'
import "./restaurantDisplay.css"
import { getAllRestaurants } from '../../api/restaurantApi';

const RestaurantDisplay = ({category, setCategory}) => {
    const [restaurants, setRestaurants] = React.useState([]);

    React.useEffect(() => {
        const fetchRestaurants = async () => {
            try {
                const data = await getAllRestaurants();
                setRestaurants(data.restaurants);
            } catch (error) {
                console.error("Error fetching restaurants:", error);
            }
        };

        fetchRestaurants();
    }, []);

  return (
    <div className='restuarant-display' id="restuarant-display">
            <h2>Popular Food Joints</h2> 
            <div className="reastaurants-display-options">
            {restaurants.map((item, index)=>{
                return(
                    <div onClick={()=>setCategory(prev=>prev===item.name?"All":item.name)} key={index} className="restaurant-display-option-circle"> 
                        <img className={category===item.name?"active": ""} src={item.restaurantIconImage.filePath} alt='' />
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
