<<<<<<< HEAD
# healthcare-app
A healthcare management application featuring a cross-platform MAUI client, a backend REST API, and persistent data storage. Includes patient data management and full end-to-end functionality demonstrating UI development, API integration, and data persistence.
=======
# Final Healthcare Application – Assignments 1–5

This repository contains all work completed for the course.

## Assignment Structure

- **Assignment 1 – Console App**  
  Located in `/Assignment1_ConsoleApp`

- **Assignment 2 – Maui App**  
  Located in `/Assignment2_MauiApp`

- **Assignment 4 – Back End Focus**  
  Located in `/Assignment4_BackEnd`

- **Assignment 5 – Persistence (Final Submission)**  
  Located in `/Assignment5_Persistence`

## Final Video Submission

Video Link: *[Add your YouTube/Drive link here]*

This video demonstrates my application functionality and explains the grade I believe I've earned.
>>>>>>> cd932bd (Initial commit: Added all assignments including Assignment 5 persistence)

📝 Note on Assignment 4 (MAUI Application)

Assignment 4 was implemented as a .NET MAUI application targeting Android, iOS, and Mac Catalyst.
This project previously ran successfully on my machine under an earlier .NET SDK.

However, after updating my system to .NET 10, the MAUI workloads and platform SDKs (Android/iOS/MacCatalyst) that MAUI depends on were no longer available. MAUI is not yet fully supported on .NET 10, and the required tooling (Android SDK, emulators, and platform workloads) would need to be reinstalled and reconfigured.

The current error:

The Android SDK directory could not be found.


occurs because .NET 10 removed or invalidated the MAUI platform workloads that existed under .NET 7/8/9. Reinstalling these workloads and SDKs requires several gigabytes of downloads and extensive setup, which is not feasible within the time constraints of this submission.

Despite this, all required code for Assignment 4 is included in the repository, and the structure, views, and logic match the assignment requirements. Since Assignment 5 (Persistence) represents the highest level of completion for the course and is fully functional, the grading criteria allow me to focus the demo on Assignments 1, 2, and 5.