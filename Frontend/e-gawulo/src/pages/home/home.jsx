import React, { useState } from "react";
import "./home.css";
import { food_list } from "../../assets/assets";
import Header from "../../components/Header/Header";
import BrowsMenus from "../../components/ExploreMenus/BrowsMenus";
import RestaurantsByCategories from "../../components/RestaurantsCategories/RestaurantsByCategories";
import RestaurantDisplay from "../../components/RestuarantsDisplay/RestaurantsDisplay";
import AppDownload from "../../components/AppDownload/AppDownload";
const home = () => {

  const [category, setCategory] = useState(null);
  const[showFilteredRestaurants, setShowFilteredRestaurants] = useState(true);
  return <div>
    <Header />
    <BrowsMenus category={category} setCategory={setCategory} setShowFilteredRestaurants={setShowFilteredRestaurants} />
    {category && <RestaurantsByCategories category={category} /> }
    < RestaurantDisplay />
    <AppDownload />
  </div>;
};

export default home;
