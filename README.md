# Basic API

The solution shows a basic API application for managing websites. It has an API for CRUD operations, validation, exception handling and code first design pattern.
It uses latest stable .NET Core version and some parts are covered with Unit tests. 

### Database

The application uses local database. If needed change the connection string in the appsettings.Development.json file in the root folder of the API project.
The application used Code first design pattern so the database will be created and seeded with data on the first run of the application. 

### Tests

BasicAPI.UnitTests contains unit tests for some important functionalities of the application. 
