import React, { useState } from "react";
import "./home.css";
import Header from "../../components/Header/Header";
import BrowsRestaurants from "../../components/ExploreMenus/BrowsMenus";
const home = () => {

  const [category, setCategory] = useState("All");
  return <div>
    <Header />
    <BrowsRestaurants category={category} setCategory={setCategory} />
  </div>;
};

export default home;
