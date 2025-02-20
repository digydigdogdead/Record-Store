# Record Store
This WebAPI built with ASP.NET and Blazor will allow you to manage your very own imaginary Record Store.

On the backend you may:
- Make Get requests to see all the albums on record, all the albums by a given artist, or a specific album.
- Make Patch requests to update the details of an album.
- Make Delete requests to get rid of an album.
- Purchase an album to decrease its stock.

On the front end you may:
- Add new albums
- Edit existing ones
- See all available albums


This project makes heavy use of Test-Driven Development including Moq. The technology used includes: 
- .NET 8
- ASP.NET 9.0.2
- Microsoft WebAssembly 8.0.12
- Entity Framework Sqlite and Entity Framework SqlServer 9.0.0
- AspNetCore Health Checks 7.0.2
- Moq 4.20.72
- Fluent Assertions 7.0.0
- NUnit 3.14.0

### Notes:
- The purchase pipeline is currently not thoroughly tested in the Service layer. I hope to further research mocking to be able to add more tests.
