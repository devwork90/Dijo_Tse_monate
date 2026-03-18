import React, { useState } from "react";
import "./home.css";
import Header from "../../components/Header/Header";
import BrowsMenus from "../../components/ExploreMenus/BrowsMenus";
import RestaurantsByCategories from "../../components/RestaurantsCategories/RestaurantsByCategories";
import RestaurantMenuItems from "../../components/RestaurantMenuItems/RestaurantMenuItems";
import RestaurantDisplay from "../../components/RestuarantsDisplay/RestaurantsDisplay";
import AppDownload from "../../components/AppDownload/AppDownload";
const home = () => {

  const [category, setCategory] = useState(null);
  const [selectedRestaurant, setSelectedRestaurant ] = useState(null);
  return <div>
    <Header />
    <BrowsMenus category={category} setCategory={setCategory}/>
    {category && !selectedRestaurant && <RestaurantsByCategories category={category}
    setSelectedRestaurant={setSelectedRestaurant}/>}
    {selectedRestaurant && <RestaurantMenuItems restaurant={selectedRestaurant} 
    onBack={() => setSelectedRestaurant(null)}/>}
    < RestaurantDisplay />
    <AppDownload />
  </div>;
};

export default home;
