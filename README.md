# CarritoCompras

Este proyecto es una aplicación que realiza la tarea Carrito de Compras desarrollada con ASP.NET Core.

## Funcionalidades

- **Creación categorias:** Permite crear categorias en base productos.
- **Creación productos:** Permite crear productos los cuales pertenecen a una categoria.
- **Actualizacion productos:** Permite ajustar la infomacion de los productos.
- **Eliminar productos:** Permite eliminar productos (manera logica).
- **Agregar productos al carrito:** Permite a los usuarios agregar productos al carrito de compras.
- **Actualizar la cantidad de productos:** Los usuarios pueden ajustar la cantidad de productos en el carrito.
- **Eliminar productos del carrito:** Ofrece la opción de eliminar productos del carrito.
- **Vaciar carrito:** Ofrece la opción de eliminar todos los productos de un carrito.
- **Proceso de Compra:** Implementa un proceso de compra que incluye la verificación de stock, la creación de pedidos y el registro de detalles de pedidos.
- **Autenticación de Usuarios:** Los usuarios pueden iniciar sesión para acceder a funciones como el proceso de compra.

## Requisitos Previos

- [ASP.NET Core](https://dotnet.microsoft.com/download)
- [SQL Server]
- [Visual Studio](https://visualstudio.microsoft.com/) o cualquier otro editor de código

## Tecnologias Utilizadas

- **ASP.NET Core:** Framework para construir aplicaciones web modernas.
- **C#:** Lenguaje de programación utilizado en el backend.
- **Entity Framework:** ORM para trabajar con bases de datos en .NET.
- **HTML, CSS, JavaScript:** Tecnologías web estándar utilizadas en el frontend.
- **Bootstrap:** Framework de diseño para facilitar la creación de interfaces atractivas.
- **SQL Server:** Sistema de gestión de bases de datos relacional.
- **Git:** Sistema de control de versiones para el seguimiento del código fuente.
- **Migrations:** Administracion de cambios en la estructura de la base de datos utilizando migraciones.

## Configuración del Proyecto

1. Clona el repositorio: `git clone https://github.com/Mushu4/CarritoCompras.git`
2. Abre el proyecto en Visual Studio o tu editor de código preferido.
3. Restaura los paquetes NuGet y realiza cualquier migración necesaria.

## Configuración de la Base de Datos

1. Asegúrate de tener una conexión válida a una base de datos SQL Server.
2. Actualiza la cadena de conexión en `appsettings.json` para reflejar tu configuración.
3. Ejecutar Script suministrados o tambien  se puede crear la base de datos a partir de las migraciones con el comando `dotnet ef database update` (esto lo que hara es utilizar Entity Framework para crear la Base de Datos.
