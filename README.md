# Tour Planner
https://github.com/jakobfriedl/tour-planner

SWEN2 SS2022 Semesterproject - by Jakob Friedl (if20b089) & Philipp Haider (if20b097)

## Solution Structure and Architecture

```c
├── src 
│   ├── TourPlanner
│   │   ├── Config 
│   │   │   ├── settings.json // custom configuration 
│   │   ├── Utility // helper classes
│   │   ├── ViewModels
│   │   │   ├── Abstract // abstract base classes
│   │   │   ├── Commands 
│   │   │   │   ├── [*]Command.cs
│   │   │   ├── [*]ViewModel.cs
│   │   ├── Views // UserControls and dialog windows
│   ├── TourPlanner.BusinessLayer 
│   │   ├── Abstract // interfaces
│   │   ├── Exceptions // custom exceptions 
│   │   ├── [*]Manager.cs 
│   ├── TourPlanner.DataAccessLayer
│   │   ├── Common // interfaces
│   │   ├── Configuration // custom config management
│   │   ├── DAO // interfaces for DAO
│   ├── TourPlanner.DataAccessLayer.REST 
│   │   ├── HttpRequest.cs // calls to MapQuest API
│   ├── TourPlanner.DataAccessLayer.SQL
│   │   ├── Database.cs
│   │   ├── [*]DAO.cs
│   ├── TourPlanner.Models
├── tests
│   ├── TourPlanner.Tests // Unit Tests for the Presentation Layer
│   ├── TourPlannerBL.Tests // Unit Tests for the Business Layer
```

This C#-WPF application was developed with regard to the MVVM (Model-View-ViewModel) Design Pattern. All logic of View components is defined in the corresponding ViewModel. The communication between ViewModels is ensured via dependency injection in the App.xaml.cs file in the presentation layer. Events are handelt with Commands and DataBindings. The project is divided into several layers, where each layer is separated by the definition of the single responsibility principle.  

### Design Patterns
- MVVM
- Command Pattern
- Factory Pattern
- Observer Pattern 

## Features 
- Create Tours (Metrics automatically queried from API or calculated)
- Image of route queried from API and saved on file system
- Delete/Modify Tours
- Create Logs for a specific Tour 
- Delete/Modify Logs
- Full Text Search for Tours based on Name, Description, Start, Destination and Comment in TourLogs
- Generate TourReport for single tour with all information + logs
- Generate SummarizeReport for complete tour-list with statistical overview
- Import and Export Tours with Logs in Json-Format
- Custom configuration file allowing the user to change image-path, database-access, etc. 
- Validated User-Input 
- Unique Feature: TODO 

## Development Process and Design

### Frontend
The frontend of the application consists of multiple UserControls that design the look of a specific component (e.g the search-bar or log-list). The window can be resized and is fully responsive. If the tour or log list gets too long, the app automatically provides a vertical scrollbar. When creating/editing a tour or log, the user is prompted with an modal dialog. In order to avoid duplicate code, Create and Edit use the same UserControl. Edit only fills the fields with the already existing data from the selected tour or log. 

![UI Wireframe](./docs/UI.png)

### Models 

The models are placed in a separate layer so that all other layers have access to them and to avoid code duplication. 

### Backend 

When a button is pressed, its command executes a function on the ViewModel which sends or requests data from the corresponding Manager in the BusinessLayer. This Log or TourManager then creates a request to SQL- and/or REST-DAL and sends the results back to the ViewModel where the view is then adapted. 

![DB Diagram](./docs/DB.png)

### Unit Tests

TODO 

## Lessons learned 
- Building complex WPF-Apps 
- API integration to C# projects
- Custom configuration files
- Multi-layered architecture and structure
- MVVM-Pattern
- PDF-Generation libraries
- Efficient DAL structure
- Dependency injection 
- Resolving Merge-conflicts 

## Screenshots

![CreateTour](./resources/img-1.png)

![TourRoute](./resources/img-2.png)

![SummarizeReport](./resources/img-3.png)

## Time Tracking

TODO 

## External Libraries

[Extended WPF Toolkit](https://github.com/xceedsoftware/wpftoolkit) for DateTimePicker, WatermarkTextBox and other advanced WPF elements

[QuestPDF](https://github.com/QuestPDF/QuestPDF) for PDF-Report generation 
