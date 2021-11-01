# CSVimport
- Asp.Net Core
- Entity Framework Core
- Sql or MemoryDb
- Memory cache stores loaded Excel results
- Dependency Injection
Data content Key is Unique
DB relation with SQL Express or InMemory Db. There is ConnectionString attribute in appsettings. 
"ConnectionStrings": {
    "DefaultConnection": "Server=inMemory; Database=csvimport;"
    //"DefaultConnection": "Server=.\\SQLEXPRESS; Database=csvimport; Trusted_Connection=True;"
  },
 
Developer can switch between storing options. DbContextConfigure class has a Configure method which managing the Database options with the connectionstring property. 
Code has been written compatiple to Web API. Therefore, projects have view, controller sections. 
Controller is inherited from NetCore.Mvc.Controller class.
When program loaded, index file is run under Host and View and Home. Controller is run 
