# 🥬 DKD Fresh Express — Price List Management System

> ASP.NET Core MVC (.NET 8) · SQL Server · Entity Framework Core

---

## 📋 About

A web-based price list management system for **DKD Fresh Express**, a wholesale and retail supplier of vegetables, fruits, seafood, and pork cuts based in Davao City, Philippines.

---

## 🚀 Quick Start

### Prerequisites
- Visual Studio 2022 (v17.8 or later)
- .NET 8 SDK
- SQL Server 2019+ / SQL Server Express / LocalDB

### Steps
1. Clone the repository
   ```
   git clone https://github.com/kdu-333/DKDFreshExpress.git
   ```
2. Open `DirectWholesaleSupply.sln` in Visual Studio 2022
3. Edit `appsettings.json` — update the connection string for your SQL Server
4. Run `Database_Setup.sql` in SSMS to create the database and seed all products
5. Press **F5** — the app connects and runs

---

## 🔌 Connection String

**LocalDB (default):**
```
Server=(localdb)\mssqllocaldb;Database=DirectWholesaleSupplyDB;Trusted_Connection=True;
```

**SQL Server / Hosting:**
```
Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;Encrypt=False;TrustServerCertificate=True;
```

---

## 🔑 Login Credentials

| Role  | Username | Password |
|-------|----------|----------|
| Admin | admin    | ******** |
| Staff | staff    | ******** |

> ⚠️ Default credentials are set during database setup. Change them after first login.

---

## ✅ Features

- 📊 Dashboard with stat cards and recent price history
- 📋 Price list grouped by category with Philippine Peso (₱) prices
- 🥩 Pork categories display both **Wholesale** and **Retail** prices
- 📈 Price movement indicators — **▲ green** (up) and **▼ red** (down)
- ✏️ Click any price to edit it directly (inline edit, no page reload)
- ⚡ Quick Adjust buttons — **+1 / -1 / +5 / -5** (AJAX)
- 💰 Bulk price editor — update all prices on one screen
- 🖨️ Print-ready price list with business header and contact info
- 👥 Role-based access control — **Admin** and **Staff** roles
- 📝 Full audit log with Philippine Standard Time (UTC+8) timestamps
- 📱 Mobile responsive with hamburger sidebar

---

## 🗄️ Database Setup

Run `Database_Setup.sql` in SQL Server Management Studio (SSMS).

The script will:
- Create all tables
- Seed 6 categories
- Seed 80 products with default prices
- Create default user accounts

---

## 📁 Project Structure

```
DKDFreshExpress/
├── Controllers/
│   ├── AccountController.cs       ← Login / Logout
│   ├── DashboardController.cs     ← Dashboard
│   ├── ProductsController.cs      ← CRUD + Price Update + Quick Adjust
│   ├── CategoriesController.cs    ← Category management (Admin only)
│   ├── PriceListController.cs     ← Price list + Print
│   └── AuditLogsController.cs     ← Audit log viewer (Admin only)
├── Data/
│   └── AppDbContext.cs            ← EF Core DbContext + seed data
├── Migrations/                    ← EF Core migration files
├── Models/                        ← All domain models
├── ViewModels/                    ← Dashboard, PriceList, etc.
├── Views/                         ← All Razor views (.cshtml)
├── wwwroot/
│   ├── css/site.css               ← Green theme
│   └── js/site.js                 ← AJAX + mobile sidebar
├── Database_Setup.sql             ← Standalone SQL setup script
├── appsettings.json               ← Connection string config
└── Program.cs                     ← App entry point
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core MVC (.NET 8), C# |
| ORM | Entity Framework Core 8 |
| Database | Microsoft SQL Server |
| Frontend | Razor, HTML5, CSS3, Vanilla JS |
| Auth | Cookie Authentication + Role-based Authorization |

---

## 📞 Contact

**DKD Fresh Express**
📍 Davao City, Philippines
