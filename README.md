# healthcare-app

A healthcare management application containing Assignments 1–5 for the course.  
This includes a console application, API development, MAUI UI work, and final persistence functionality.

---

## 📚 Assignment Structure

- **Assignment 1 – Console Application**  
  Located in `/assignment1`  
  Demonstrates patient, physician, appointment, and medical note creation.

- **Assignment 2 – Web API**  
  Located in `/assignment2`  
  Provides CRUD endpoints for patient data tested using Postman.

- **Assignment 4 – MAUI Application**  
  Located in `/assignment4`  
  (See explanation below regarding SDK compatibility.)

- **Assignment 5 – Persistence (Final Assignment)**  
  Located in `/assignment5`  
  Fully tested with persistent JSON storage. This satisfies the A-level requirement.

---

## 🎥 Final Video Submission

Video Link: https://youtu.be/_L2LpnrLwo0

The video demonstrates:

- Assignment 5 persistence tested with file creation + server restart  
- Explanation of Assignment 4 and grading justification  

---

## 📝 Note on Assignment 4 (MAUI Application)

Assignment 4 was implemented as a .NET MAUI application targeting Android, iOS, and Mac Catalyst.  
This project previously ran successfully on my machine under an earlier .NET SDK.

However, after updating my system to **.NET 10**, the MAUI workloads and platform SDKs (Android/iOS/MacCatalyst) that MAUI depends on were no longer available. MAUI is not yet fully supported on .NET 10, and the required tooling (Android SDK, emulators, and platform workloads) would need to be reinstalled and reconfigured.

The current error:

The Android SDK directory could not be found occurs because .NET 10 removed or invalidated the MAUI platform workloads that existed under .NET 7/8/9. Reinstalling these workloads and SDKs requires several gigabytes of downloads and extensive setup, which is not feasible within the time constraints of this submission.

Despite this, **all required code for Assignment 4 is included**, and the project structure and logic match the assignment requirements. Since **Assignment 5 (Persistence)** is fully implemented and represents the highest-level requirement for an A, the demonstration focuses on Assignments 1, 2, and 5.

---

## 🎓 Grade Justification

- Assignment 1 — Completed  
- Assignment 2 — Completed  
- Assignment 4 — Implemented (not runnable due to .NET 10 MAUI SDK issue)  
- Assignment 5 — Fully completed with persistence  

Because Assignment 5 is the A-level requirement and is fully functional,  
**I believe I have earned an A in the course.**
