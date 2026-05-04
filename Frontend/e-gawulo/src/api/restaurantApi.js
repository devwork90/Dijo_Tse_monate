const BASE_URL = import.meta.env.VITE_BASE_URL;
// example: https://localhost:5001/api

export async function getAllRestaurants() {
  const response = await fetch(`${BASE_URL}/Restaurants`);

  if (!response.ok) {
    throw new Error("Failed to fetch restaurants");
  }

  return await response.json();
}
