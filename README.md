# AppLocation
Backend Dev - Code Test
1. Clone the Repository

   Open a terminal and run the following command to clone the repository:

   git clone <repository-url>](https://github.com/shinrami/AppLocation.git)
   cd AppLocation

2. Dependencies
   
    dotnet restore

4. Database Connection
   
   In-Memory
   
  dotnet add package Microsoft.EntityFrameworkCore.InMemory

4. Run the Application (It will run in Swagger UI)
   dotnet run 


The ENDPOINTS are the following:

GET /api/locations - Retrieve all locations.
POST /api/locations - Create a new location.
GET /api/locations/{id} - Retrieve locations by ID.
PUT /api/locations/{id} - Update an existing location.
DELETE /api/locations/{id} - Delete a location by ID.
