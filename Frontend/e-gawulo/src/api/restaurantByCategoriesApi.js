const BASE_URL = import.meta.env.VITE_BASE_URL;
export const getRestaurantsByCategories = async (category) => {
  // Implementation for fetching restaurants by category
  const response = await fetch(`${BASE_URL}/api/Restaurants?menuName=${category}`
    
  );
    if (!response.ok) {
        throw new Error("Failed to fetch restaurants by category");
    }
    
    return await response.json();
};