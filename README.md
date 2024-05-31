# PROG 3A Part 2 Attempt 3

This project is a web application developed in ASP.NET Core. It includes various features such as user authentication, product management, and more.
[GitHub](https://github.com/ST10204902/ST10204902-PROG-3A-Part-2)

### Login Details
Farmers can apply to join Agri-Energy Connect via the login page. An employee can then review the application and approve it, granting the farmer access to the site.

Default Employee Information:
- Email/username: employee@example.com
- Password: Password123!

Default Farmer1 Information:
- Email/username: farmer1@example.com
- Password: Password123!

Default Farmer2 Information
- Email/username: employee@example.com
- Password: Password123!

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

### Hardware Requirements
- **Processor:** 1.8 GHz or faster (dual-core or better recommended)
- **RAM:** 4 GB (8 GB or more recommended)
- **Hard Disk:** At least 5 GB of free space
- **Screen Resolution:** 1280x720 or higher

### Software Requirements
- **Operating System:** Windows 10 or later, macOS, or a Linux distribution
- **.NET SDK:** [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Visual Studio:** Visual Studio 2022 (Community, Professional, or Enterprise)
- **SQL Server Express** [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Steps to Setup

1. **Download and Install .NET 8.0 SDK**
   - Visit the [.NET download page](https://dotnet.microsoft.com/download/dotnet/8.0) and download the installer for your operating system.
   - Follow the instructions on the website to complete the installation.

2. **Download and Install Visual Studio 2022**
   - Visit the [Visual Studio download page](https://visualstudio.microsoft.com/downloads/) and download the installer.
   - During the installation, ensure you include the "ASP.NET and web development" workload.

3. **Clone the Repository**
   - Open a terminal and run the following command:
     ```sh
     git clone <repository_url>
     ```
   - Navigate to the project directory:
     ```sh
     cd <project_directory>
     ```

4. **Download the ZIP File and Extract**
   - Alternatively, you can download the project as a ZIP file from the repository page.
   - Extract the contents of the ZIP file to your desired location.

5. **Open the Project in Visual Studio 2022**
   - Launch Visual Studio 2022.
   - Click on "Open a project or solution".
   - Navigate to the folder where you cloned/extracted the project and select the solution file (`.sln`).

6. **Run the Project**
   - In Visual Studio, ensure the solution is loaded.
   - Set the startup project if it's not already set by right-clicking on the project in Solution Explorer and selecting "Set as Startup Project".
   - Press `F5` or click on the "Run" button to build and run the project.

## Contributing

Contributions are welcome. Please feel free to submit a pull request or open an issue if you find any bugs or have any suggestions for improvements.
