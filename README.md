🏠 Baytak - Real Estate Platform API

Baytak is a modern Real Estate Backend API built with **ASP.NET Core Web API** using **Clean Architecture** principles.

The platform allows users to browse properties, communicate with agents in real-time, manage bookings, upload images, receive notifications, and more.

---

🚀 Features

🔐 Authentication & Authorization

* User Registration
* JWT Authentication
* Email Confirmation with OTP
* Resend OTP
* Forgot Password with OTP
* Reset Password
* Protected Endpoints



 🏘️ Properties

* Create Property
* Update Property
* Delete Property
* Get Property Details
* Get All Properties
* Mark Property As Sold
* Upload Main Image
* Multiple Property Images



🔎 Search

* Search by City
* Search by Price
* Search by Rooms
* Search by Property Type



❤️ Favorites

* Add Property to Favorites
* Remove from Favorites
* Get User Favorites



💬 Real-Time Chat

* Start Conversation
* Send Messages
* Get Conversation Messages
* Real-Time Messaging using SignalR
* Seen Messages



📅 Booking System

* Create Booking Request
* Booking Time Slots
* Approve Booking
* Reject Booking
* Cancel Booking
* Agent Booking Dashboard
* Prevent Double Booking
* Max Booking Duration Validation



🔔 Notifications

* Message Notifications
* Booking Notifications
* Read Notifications
* Notification History



🧱 Architecture

The project follows **Clean Architecture**:

```plaintext
Baytak.API
Baytak.Application
Baytak.Domain
Baytak.Infrastructure
```

Layers

API Layer

Handles:

* Controllers
* Swagger
* Authentication Middleware
* SignalR Hubs

 Application Layer

Handles:

* Business Logic
* DTOs
* Interfaces
* Services

 Domain Layer

Handles:

* Entities
* Enums
* Core Business Rules

 Infrastructure Layer

Handles:

* EF Core
* Repositories
* Email Service
* File Uploads
* SignalR Notifications



 🛠️ Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* JWT Authentication
* SignalR
* Swagger
* Clean Architecture
* LINQ
* Dependency Injection



 📡 SignalR

The application supports real-time messaging using SignalR.

 Features:

* Join Conversation Groups
* Receive Messages Instantly
* Real-Time Chat Experience



 📷 Image Upload

Images are stored inside:

```plaintext
wwwroot/images
```

And accessed through:

```plaintext
/images/{imageName}
```



🔑 Authentication Flow

```plaintext
Register
   ↓
Receive OTP via Email
   ↓
Confirm Email
   ↓
Login
   ↓
Receive JWT Token
```



📅 Booking Flow

```plaintext
User creates booking
   ↓
Agent reviews booking
   ↓
Approve / Reject
   ↓
Other conflicting bookings rejected automatically
```



 📂 API Modules

 Auth

* Register
* Login
* Confirm Email
* Forgot Password
* Reset Password
* Resend OTP

Property

* CRUD Operations
* Mark as Sold

 Booking

* Create Booking
* Approve / Reject / Cancel
* Agent Bookings

 Chat

* Conversations
* Messages
* Seen Messages
* SignalR Hub

 Notification

* Get Notifications
* Mark as Read

 Favorites

* Add / Remove Favorites

 Search

* Property Search & Filtering



🔒 Security

* JWT Protected APIs
* Authorization Policies
* OTP Email Verification
* Booking Ownership Validation
* Agent Ownership Validation



📬 Example API Endpoints

 Login

```http
POST /api/Auth/login
```

 Create Property

```http
POST /api/Property
```

 Send Message

```http
POST /api/Messages
```

Create Booking

```http
POST /api/Booking
```



 ⚙️ Running The Project

 1. Clone Repository

```bash
git clone <repo-url>
```

---

 2. Update Connection String

Inside:

```plaintext
appsettings.json
```



 3. Apply Migrations

```bash
Update-Database
```



 4. Run Project

```bash
dotnet run
```



📘 Swagger

Swagger UI is available at:

```plaintext
/swagger
```



🌍 Deployment

The project can be deployed on:

* MonsterASP.NET
* IIS
* Azure
* Docker



📈 Future Improvements

* Real-Time Notifications
* Role-Based Authorization
* Property Reviews
* Payment Integration
* AI Property Recommendations
* Admin Dashboard



👨‍💻 Author

Developed by:

**Mohamed Taya**

Backend Developer | ASP.NET Core Developer



⭐ Project Highlights

✅ Clean Architecture

✅ Real-Time Chat with SignalR

✅ Booking System

✅ JWT Authentication

✅ OTP Email Verification

✅ Notifications System

✅ Image Upload & Static Files

✅ Production-Ready Structure
