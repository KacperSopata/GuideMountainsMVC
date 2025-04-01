# GuideMountainsMVC

GuideMountainsMVC is a full-stack ASP.NET Core MVC application that helps users plan mountain trips by allowing them to browse ski passes, accommodations, and equipment rentals — and create complete reservations for selected places.

---

## 🚀 Features

- **Three-step exploration flow** for ski passes, accommodations, and rentals:
  - Select **Country** → **Mountain Place** → **Details**
- **SkiPass reservation system** with support for types, durations, and quantities
- **Accommodation booking** including number of guests and nights
- **Equipment rental** with categories, quantity, and rental days
- **Unified reservation summary** with all booked items and calculated price
- **Authentication and authorization** with ASP.NET Identity & Google login
- **Admin panel** to add, edit, and delete entries (e.g. SkiPass, Accommodation, EquipmentRental)
- **Image upload** and display for each place and item
- **Unit testing** with xUnit and Moq

---

## 🧱 Technologies Used

- **.NET 8.0**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **MS SQL Server**
- **Clean Architecture**
- **AutoMapper**
- **FluentValidation**
- **ASP.NET Identity**
- **Google Authentication**
- **Razor Views**
- **xUnit / Moq**
- **Bootstrap (basic)**

---

## 🗂️ Project Structure

```
GuideMountainsMVC
├── Domain           # Core domain models and interfaces
├── Application      # Business logic and services
├── Infrastructure   # EF Core, database context, repositories
├── Web (MVC)        # Views, controllers, UI layer
├── wwwroot/img      # Uploaded images (Base64 or file)
├── Tests            # Unit tests with xUnit & Moq
└── GuideMountainsMVC.sln
```

---

## 🛠️ How to Run the Project

1. **Clone the repo**
```bash
git clone https://github.com/YourUsername/GuideMountainsMVC.git
```

2. **Open the solution in Visual Studio** (`GuideMountainsMVC.sln`)

3. **Set `Web` project as startup**

4. **Update the connection string** in `appsettings.json`
```json
"ConnectionStrings": {
  "DefaultConnection": "your-sql-connection-string"
}
```

5. **Apply migrations & seed data (optional)**
```bash
dotnet ef database update
```

6. **Run the project** (`Ctrl + F5` in Visual Studio)

---

## 📸 Screenshots
> *(You can upload and link your screenshots here)*

---

## 👤 Author

**Kacper Sopata**  
.NET Developer (Junior)  
🔗 [GitHub](https://github.com/KacperSopata) • [LinkedIn](https://linkedin.com/in/KacperSopata)

---

## 📄 License

This project is for educational and portfolio use. You can fork and build upon it freely.
