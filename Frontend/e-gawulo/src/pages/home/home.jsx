import React, { useState } from "react";
import "./home.css";
import Header from "../../components/Header/Header";
import BrowsMenus from "../../components/ExploreMenus/BrowsMenus";
import RestaurantDisplay from "../../components/RestuarantsDisplay/RestaurantsDisplay";
const home = () => {

  const [category, setCategory] = useState("All");
  return <div>
    <Header />
    <BrowsMenus category={category} setCategory={setCategory} />
    < RestaurantDisplay  />
  </div>;
};

export default home;
