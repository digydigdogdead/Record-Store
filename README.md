# Record Store
This WebAPI built with ASP.NET will allow you to manage your very own imaginary Record Store.

- Make Get requests to see all the albums on record, all the albums by a given artist, or a specific album.
- Make Patch requests to update the details of an album.
- Make Delete requests to get rid of an album.
- Purchase an album to decrease its stock.

This project makes heavy use of Test-Driven Development.

# Frontend!
Launch the Blazor-FrontEnd branch to experience a website where you can:
- Add new albums
- Edit existing ones
- Look at all the albums in glorious HD

Notes:
- The purchase pipeline is currently not thoroughly tested in the Service layer. I hope to further research mocking to be able to add more tests.
