# MeetingsManagement
The Meeting and Task Management API is a RESTful web service that allows users to manage meetings and tasks. The system supports creating, retrieving, updating, and deleting meetings and tasks. It also allows for the generation of a summary report of meetings and functions within a specified date range.

The application is built using C# and ASP.NET Core, and uses in-memory data storage (dictionaries) for simplicity. The project follows SOLID principles to ensure a clean and maintainable codebase.

# Implemented Features:
Meeting Management:

Create Meeting
Retrieve Meeting by ID
List All Meetings (with pagination)
Update Meeting
Delete Meeting
Generate Meeting Report within a Date Range
Task Management:

Create Task
Retrieve Tasks by Meeting ID (with pagination)
Update Task
Delete Task
Report Generation:

Summary of Meetings and their associated tasks within a specified date range

Unit test cases.


# Setup & run locally
Open the Project:

Open your Web API project in Visual Studio.
Build the Project:

Press Ctrl + Shift + B or go to Build > Build Solution.
Test Locally in Visual Studio:

Press F5 to run the project.
Visual Studio will launch the API using IIS Express or Kestrel (depending on your project setup).
The API will be hosted locally at https://localhost:7075.


# Test the Web API
Please use the swagger URL for API documentation and try the endpoints: https://localhost:7075/swagger/index.html

<img width="1175" alt="image" src="https://github.com/user-attachments/assets/b3caa4bd-ea75-4e68-a87a-607e5253d758" />

