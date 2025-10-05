import React from "react";
import "./Navbar.css";
import { assets } from "../../assets/assets";

const Navbar = () => {
  return (
    <div className="navbar">
      <h1>hello </h1>
      <img src={assets.logo} alt="" />
    </div>
  );
};

export default Navbar;
