import React, { useState } from "react";
import "./home.css";
import Header from "../../components/Header/Header";
import BrowsMenus from "../../components/ExploreMenus/BrowsMenus";
import RestaurantDisplay from "../../components/RestuarantsDisplay/RestaurantsDisplay";
import AppDownload from "../../components/AppDownload/AppDownload";
const home = () => {

  const [category, setCategory] = useState("All");
  return <div>
    <Header />
    <BrowsMenus category={category} setCategory={setCategory} />
    < RestaurantDisplay  />
    <AppDownload />
  </div>;
};

export default home;
