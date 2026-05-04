const BASE_URL = import.meta.env.VITE_BASE_URL;
export const getMenuItemsByRestaurant = async (restaurantId) => {
  // Implementation for fetching menu items by restaurant ID   
    const response = await fetch(`${BASE_URL}/Restaurants/menu-items?restaurantId=${restaurantId}`);
    if (!response.ok) {
        throw new Error("Failed to fetch menu items by restaurant");
    }
    return await response.json();
};