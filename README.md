## Stack Tecnológico

- **Backend:** C# / .NET 8
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core 8
- **Base de Datos:** SQL Server Express 2022
- **Autenticación:** JWT (JSON Web Tokens)
- **Control de Versiones:** Git + GitHub

## Arquitectura del Proyecto

BarberiaReservas/
├── Domain/ # Entidades de negocio
├── Application/ # Lógica de aplicación (SOLID aquí)
├── Infrastructure/ # EF Core, repositorios
└── API/ # Controllers, Program.cs

## Instalación y Configuración

### Prerrequisitos

- .NET SDK 8.0
- SQL Server Express 2022
- Git

### Pasos

1. **Clonar el repositorio**

```bash
git clone https://github.com/tu-usuario/barberia-reservas-api.git
cd barberia-reservas-api
```

2. **Restaurar paquetes**

```bash
dotnet restore
```

3. **Configurar Connection String**

Editar `BarberiaReservas.API/appsettings.json`:

```json
"DefaultConnection": "Server=TU_SERVIDOR;Database=BarberiaDB;..."
```

4. **Aplicar migraciones**

```bash
dotnet ef database update --project BarberiaReservas.Infrastructure --startup-project BarberiaReservas.API
```

5. **Ejecutar**

```bash
cd BarberiaReservas.API
dotnet run
```

6. **Abrir Swagger**
