# Direct Wholesale Supply — Price List Management System

ASP.NET Core MVC (.NET 8) | SQL Server | Entity Framework Core

Davao City, Philippines | 09109160568 | 09383174368

---

## Quick Start

### Prerequisites
- Visual Studio 2022 (17.8+)
- .NET 8 SDK
- SQL Server 2019+ / Express / LocalDB

### Steps

1. Extract the archive and open `DirectWholesaleSupply.sln`
2. Edit `appsettings.json` — update the connection string for your SQL Server
3. Press F5 — the app auto-migrates and seeds the database on first run
4. Login at `/Account/Login`

### Default Connection String (LocalDB)
```
Server=(localdb)\mssqllocaldb;Database=DirectWholesaleSupplyDB;Trusted_Connection=True;
```

### Full SQL Server
```
Server=YOUR_SERVER;Database=DirectWholesaleSupplyDB;User Id=sa;Password=YOUR_PASS;TrustServerCertificate=True;
```

---

## Login Credentials

| Role  | Username | Password  |
|-------|----------|-----------|
| Admin | admin    | sikreto  |
| Staff | staff    | parabibo  |

---

## Features

- Dashboard with stat cards and recent price history
- Price List grouped by category with PHP peso prices
- Pork categories display both Wholesale and Retail price
- Price movement arrows: green up, red down
- Quick Adjust buttons: +1 / -1 / +5 / -5 (AJAX, no reload)
- Bulk price editor — edit all prices on one screen
- Print-ready price list with business header
- Role-based access (Admin / Staff)
- Full audit log with Philippine Standard Time timestamps
- Mobile responsive with hamburger sidebar

---

## Database Option (Manual SQL)

Run `Database_Setup.sql` in SQL Server Management Studio
as an alternative to EF migrations.

---

## Project Structure

```
DirectWholesaleSupply/
  Controllers/   — Account, Dashboard, Products, Categories,
                   PriceList, AuditLogs
  Data/          — AppDbContext with EF seed data
  Migrations/    — Initial migration (80 products seeded)
  Models/        — All domain models
  ViewModels/    — Dashboard, PriceList, etc.
  Views/         — All Razor views
  wwwroot/       — CSS (green theme) + JS
  Program.cs     — App entry point with auto-migrate
  appsettings.json
```

---

Direct Wholesale Supply — Davao City, Philippines
09109160568 | 09383174368
