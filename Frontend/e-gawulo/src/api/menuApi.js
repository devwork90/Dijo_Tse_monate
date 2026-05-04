const BASE_URL = import.meta.env.VITE_BASE_URL;
// example: https://localhost:5001/api

export async function getAllMenus() {
  const response = await fetch(`${BASE_URL}/Menu`);

  if (!response.ok) {
    throw new Error("Failed to fetch menus");
  }

  return await response.json();
}
