import { createContext } from "react";
import { food_list } from "../assets/assets";
import { getAllRestaurants } from "../api/restaurantApi";
export const StoreContext = createContext(null)

const StoreContextProvider = (props) => {

    const contextValue = {

        getAllRestaurants
    }
    
    return (
        <StoreContext.Provider value={contextValue}>
            {props.children}
        </StoreContext.Provider>
    )
}

export default StoreContextProvider;