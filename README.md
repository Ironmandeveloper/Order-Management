# Order-Management
This project demonstrates the integration of PostgreSQL, Excel file parsing, and client-side filtering in a .NET Core MVC environment. The application allows you to upload an Excel file, parse its data, and store it either in a JSON file or a PostgreSQL database. Additionally, it includes a client-side filtering mechanism for enhanced user interactivity.

## Getting Started
## Project Name: FileDumpUsingPostgreSQL

### Configure PostgreSQL:

Open appsettings.json in the MVC project.
Modify the PostgreSQL connection string to match your database setup.

### Install Dependencies

### Ensure you have the necessary packages installed by running:
dotnet restore

### Run the Application:

Navigate to the MVC project directory and run:
dotnet run

Access the application at localhost

## Project Structure

### -	Controllers:
HomeController: Contains logic for Excel file parsing, data storage in JSON/PostgreSQL, and client-side filtering.

### -	Views:
Index.cshtml: Displays responsive orders list using jQuery.
LoadFile.cshtml: Handles file upload, data loading from JSON/PostgreSQL, and client-side filtering.

## File Locations

### -	Excel File:
### -	JSON File:

## Usage:


### Excel File Upload:
Open the application and navigate to the LoadFile page.
Upload an Excel file to parse and store data.
### Data Storage Options:

Choose between storing parsed data in a .json file or PostgreSQL database.

### Data Retrieval:
Use the LoadFile page to load data from either the .json file or the PostgreSQL database.
Apply client-side filters based on date range and search text.



# ClientFileUpload Project:
A separate project reads orders from the JSON file and displays them on the Index page using jQuery.

## Technologies Used

1) .NET Core 6 MVC
2) PostgreSQL with Entity Framework Core
3) Newtonsoft.Json for JSON handling
4) DocumentFormat.OpenXml for Excel file parsing
5) jQuery for client-side interactivity

Just Run the application in visual studio and open the application in any browser.
