## Stack Tecnológico

- **Backend:** C# / .NET 8
- **Framework:** ASP.NET Core Web API
- **ORM:** Entity Framework Core 8
- **Base de Datos:** SQL Server Express 2022
- **Autenticación:** JWT (JSON Web Tokens)
- **Control de Versiones:** Git + GitHub

## Arquitectura del Proyecto

El proyecto sigue los principios de una Arquitectura Limpia (Clean Architecture), dividida en las siguientes capas:

- **BarberiaReservas.Domain:** Contiene las entidades principales del negocio (`User`, `Service`, `Reservation`, `WorkingHours`, `BlockedDate`). Es el núcleo del sistema y no depende de ninguna otra capa.
- **BarberiaReservas.Application:** Contiene los casos de uso de la aplicación, interfaces de repositorios, DTOs y lógica de coordinación.
- **BarberiaReservas.Infrastructure:** Contiene la implementación del acceso a datos usando Entity Framework Core (`AppDbContext`), repositorios y la interacción con servicios de infraestructura externos (como BCrypt para contraseñas).
- **BarberiaReservas.API:** Capa de presentación y punto de entrada de la aplicación. Expone los controladores REST, configura Swagger, CORS, inyección de dependencias y la configuración de appsettings.
