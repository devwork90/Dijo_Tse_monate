import React, { use } from 'react'
import "./RestaurantMenuItems.css"
import { assets } from '../../assets/assets'
import { getMenuItemsByRestaurant } from '../../api/menuItemsByRestaurantApi'


const RestaurantMenuItems = ({ restaurant, onBack }) => {
    const [menuItems, setMenuItems] = React.useState([]);
    const [loading, setLoading] = React.useState(true);
    const [error, setError] = React.useState(null);

    React.useEffect(() => {
        const fetchMenuItems = async () => {
            setLoading(true);
            try {
                const response = await getMenuItemsByRestaurant(restaurant);
                setMenuItems(response);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        }
        if (restaurant) {
            fetchMenuItems();
        }
    }, [restaurant]);

    return (
        <div>
            <>
                <div className='restaurant-menu-items'>
                    <img src={assets.circle_back_icon}
                        alt='' className='restaurant-back-icon'
                        onClick={onBack} />
                    <div className='restuarant-menu-item-banner'>
                        <img src={`${import.meta.env.VITE_BASE_URL}${menuItems?.restaurant?.logo_url}`} alt='' className='logo' />
                    </div>
                    <div className='restaurant-details'>
                        <h2>{menuItems?.restaurant?.name}, location</h2>
                        <p>{menuItems?.restaurant?.description}</p>
                    </div>
                    <div className='map-view'>
                        {/* <h3>Map View</h3> */}
                        <div className='map-placeholder'>
                            <img src={assets.map_view} alt='' className='map-image' />
                        </div>
                    </div>

                    <div className='menu-items-list'>
                        {loading && <p>Loading...</p>}
                        {error && <p>Error: {error}</p>}
                        {menuItems?.menu?.length > 0 && menuItems.menu.map((menuCategory) => {
                            return (
                                    <div className='menu-item-details'>
                                        <h4 className='menu-category-title'>{menuCategory.name}</h4>
                                        <div className='menu-items-container'>
                                            {menuCategory?.items?.map((item) => (
                                                    <div className='menu-item-child-container'>
                                                        <div className='menu-item-info'>
                                                            <h5>{item.name}</h5>
                                                            <p>{item.description}</p>
                                                            <div className='menu-item-price'>
                                                                R {item.price}
                                                            </div>
                                                        </div>
                                                        <div className='menu-item-image-container'>
                                                            <img src={assets.add_icon_white} alt='' className='add-item-btn'/>
                                                            <img src={`${import.meta.env.VITE_BASE_URL}${item.imageUrl}`} alt='' className='menu-item-image'/>
                                                        </div>
                                                    </div>
                                            ))}
                                        </div>
                                    </div>
                            )
                        })}
                    </div>
                </div>
            </>
        </div>
    )
}

export default RestaurantMenuItems
