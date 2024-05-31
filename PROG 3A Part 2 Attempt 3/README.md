# PROG 3A Part 2 Attempt 3# PROG 3A Part 2 Attempt 3

This project is a web application developed in ASP.NET Core. It includes various features such as user authentication, product management, and more.

## Features

### User Authentication

The application uses ASP.NET Core Identity for user authentication. It includes features for registering new users, logging in, and applying for a farmer account. The related files for these features are:

- `Areas/Identity/Pages/Account/ApplyFarmer.cshtml.cs`: This file contains the logic for applying for a farmer account. It includes a constructor for initializing the logger and the database context, and an `InputModel` for binding the input data from the form.

### Product Management

The application includes features for creating, editing, and viewing products. The related files for these features are:

- `Controllers/ProductsController.cs`: This file contains the methods for handling product-related requests. It includes methods for displaying the create view, creating a new product, and getting a product for editing.

- `Views/Products/Create.cshtml.cs`: This file contains the model for the create view of the product.

- `Models/ProductViewModel.cs`: This file contains the view model for a product. It includes a property for the photo of the product.

### Other Features

- `Helpers/EnumExtensions.cs`: This file contains an extension method for converting an enum value to a category display name.

- `Controllers/UsersController.cs`: This file contains a method for displaying the create view for users.

## Models

The application uses several models to represent the data in the application. Here's a brief overview of each model:

### AppUser

The `AppUser` model represents a user in the application. It inherits from `IdentityUser` and includes additional properties such as `FirstName`, `LastName`, `MiddleNames`, and a collection of `Products`.

### AppRole

The `AppRole` model represents a role in the application. It inherits from `IdentityRole`. No additional properties are needed as `IdentityRole` already has appropriate properties.

### DbInitializer

The `DbInitializer` class provides a method to initialize the database with default data. It includes a static `Initialize` method that creates the database if it doesn't exist, populates the roles, and adds some default users and products.

### FarmerApplication

The `FarmerApplication` model represents a farmer application. It inherits from `IdentityUser` and includes additional properties such as `FirstName`, `LastName`, and `MiddleNames`. It is used to separate a farmer application from a regular user, so that employees can approve the farmer before being added to the DB.

### Product

The `Product` model represents a product in the application. It includes properties such as `ProductID`, `Name`, `Category`, `ProductionDate`, `UserId`, `User`, `Photo`, and `Cost`.

The `Category` is an enum that represents the category of a product. It includes values such as `RenewableEnergy`, `WaterConservation`, `SoilHealthProducts`, `PestControl`, and `Other`.
## Setup

To run this project, you will need to have .NET Core installed on your machine. Once you have that, you can clone this repository and run the project using the `dotnet run` command in the terminal.

## Contributing

Contributions are welcome. Please feel free to submit a pull request or open an issue if you find any bugs or have any suggestions for improvements.
