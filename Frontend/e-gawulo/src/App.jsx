import React from "react";
import Navbar from "./components/Navbar/Navbar";
import { Route, Routes } from "react-router-dom";
import Home from "./pages/home/home";
import Cart from "./pages/Cart/Cart";
import Footer from "./components/Footer/Footer";

import PlaceOrder from "./pages/PlaceOrder/PlaceOrder";
const App = () => {
  return (
    <>
      <div className="app">
      <Navbar />
      <Routes>
        < Route path='/' element={<Home/>} />
        < Route path='/cart' element={<Cart/>} />
        < Route path='/place-order' element={<PlaceOrder/>} />
      </Routes>
    </div>
    <Footer />
    </>
  )
}

export default App;
