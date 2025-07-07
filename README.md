# InventorySystem
Setup Database:

1) Ensure you have an MSSQL server installed and running locally.

2) Open Package Manager Console in Visual Studio.

3) Set the Default Project to: InventorySystem.Infrastructure

4) Run the following commands one by one:
   - update-database -context InventorySystemDbContext
   - update-database -context InventorySystemAuthDbContext

5) Run the "SP_and_TypeTable.sql" script to create stored procedures and table types.

6) Run the "DamoData.sql" script to insert sample data.

-------------------------------------------

Setup Application:

1) Run the project from Visual Studio.

2) Register a user by calling this API:
   POST /api/Auth/Register/register
   Request Body:
   {
     "username": "admin",
     "password": "admin123"
   }

3) Login to get a JWT token:
   POST /api/Auth/Login/login
   Request Body:
   {
     "username": "admin",
     "password": "admin123"
   }

4) Copy the token from the login response.

5) Use this token in the Authorization header as:
   Authorization: Bearer <your_token>

6) Now you can access all protected APIs.

Base URL:
https://localhost:5032

