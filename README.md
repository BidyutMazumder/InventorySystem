# InventorySystem
Setup db:
Ensure you have a MySQL server installed locally.
1): Open Package Manager Console
2): Select “InventorySystem.Infrastructure” as a default project
3): Run “update-database -context InventorySystemDbContext”
4): Run “update-database -context InventorySystemAuthDbContext”
5): Run “SP_and_TypeTable” script
6): Run “DamoData” script

SetupApplication:
1): Run provided Code 
2): /api/Auth/Register/register 
run this API with this request


{
  "username": "admin",
  "password": "admin123"
}
3): /api/Auth/Login/login
Now run this api with this request and get the JWT token
{
  "username": "admin",
  "password": "admin123"
}
baseurl “https://localhost:5032”
Use this token and run all api 
