# **🏌️ Portmarnock Golf Club 🏌️**

Welcome to the Portmarnock Golf Club App! This application provides golf club members with a way to manage their memberships and bookings.
---

### **💻 Project Overview**
The PortmarnockGolfClub is a ASP.Net application built with Razor.
- Create, Read, Update and Delete Members
- Create, Read, Update and Delete Bookings
- Filter and sort members
- Filter bookings by member

---

### **✨ Features**
- **Member Management:** View and manage member details, including gender, email, and handicap.
- **Booking System:** Book tee times, check availability, and edit existing bookings. A member can't make more than one booking a day
- **Filter and Sorting:** Easily filter members by gender or handicap and sort by membership number or name.
- **Responsive Design:** Optimized for desktop and mobile devices.

---

### **📋 Tech Stack**
The project is built using the following technologies:
- **Framework:** ASP.NET Core Blazor
- **Language:** C#
- **Database:** Entity Framework Core with SQLite
- **UI Design:** Razor Components with Bootstrap

---

## 📋 Prerequisites

![.NET](https://img.shields.io/badge/.NET-8.0-blue?logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-3.39%2B-lightblue?logo=sqlite&logoColor=white)
![Razor](https://img.shields.io/badge/Razor-Informational-purple)
![Web Browser](https://img.shields.io/badge/Web_Browser-Supported-blue)

### Requirements:
1. **.NET 8.0 SDK**: Ensure you have the latest .NET SDK installed.
2. **SQLite 3.39+**: Ensure SQLite is installed on your system
3. **Razor Syntax**: Required for building UI components in Blazor (`.razor` files).
4. **Web Browser**: A modern web browser (e.g., Chrome, Edge, Safari) to run the Blazor app.

---

### **📦 Installation**
1. **Clone this repository:**
   ```bash
   git clone https://github.com/cwmcfeely/PortmarnockGolfClub.git
   cd PortmarnockGolfClub
  
2. **Install Prerequisites:**
Ensure you have the following installed:
- **.NET 8.0 SDK:** Download and install from here.
- **SQLite 3.39+:** SQLite is downloadable extension on VSC
  
4. **Build and Run the App**
Run the following commands to build and run the app locally:

  ```bash
  dotnet build
  dotnet run
  ```
The app will be accessible at http://localhost:<port> in your browser.

### **📋 Usage Instructions**
Managing Members:
1. Navigate to the "Members" page.
2. View member details such as name, email, gender, and handicap.
3. Use filters (gender or handicap) or sorting options (membership number or name) to refine results.
4. Click "Edit" to modify member details or "Delete" to remove a member.

Booking Tee Times:
1. Navigate to the "Bookings" page.
2. Select a date and check available time slots.
3. Book a tee time for yourself or other members.
4. Edit or delete existing bookings as needed.

### **🎨 UI Enhancements**
The app features a clean design with:
- A sidebar for navigation (Home, Members, Bookings).
- Dropdown filters for gender and handicap on the Members page.
- Responsive table layouts for member details.
