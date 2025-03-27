
# Sistema de Incidencias

Hola!. Soy Geuberth y te daré una breve introducción al como se desarrolló, implemento y se luchó por el correcto funcionamiento de un sistema de incidencias sencillo pero primeramente explicaré como instalar las dependencias necesarias.

    1. Descomprimir *NeoTecnologias.rar*
    2. Abrir NeoTecnologias.sln con el uso de Visual Studio 2022
    3. Instalar los paquetes necesarios para su funcionamiento listados en *Installation*
    4. Tener SQL Server Express con autenticación por windows para la conexión a la base de datos
    5. Crear Base de datos llamada "Incidents" para la correcta conexión a la BD




## Installation

En la Consola del Administrador de Paquetes (Tools > NuGet Package Manager > Package Manager Console)

```bash
Update-Package -reinstall

```
Seguido de la correcta instalación de los paquetes procedemos a un ultimo paso el cual será en la Consola del Administrador de Paquetes (Tools > NuGet Package Manager > Package Manager Console)

```bash
Update-Database

```
Solo si es Necesario
```bash
Add-Migration NuevaMigracion
Update-Database
```
Una vez te aparezca las tablas en tu base de datos debes crear manual mente 2 tipos de usuario ya que ambos tipos de usuario son obligatorios, los puedes crear de esta forma:

```bash
INSERT INTO Users (Name,IsTechnician)
VALUES ('Petro', 0); // 0 para usuarios normales

INSERT INTO Users (Name,IsTechnician)
VALUES ('sara', 1); // 1 para usuarios Tecnicos
```


## Ejecución
    1. En Visual Studio, seleccionar "IIS Express".

    2. Presionar Ctrl + F5 para iniciar la aplicación.

    3. Acceder en el navegador a: https://localhost:44320/
## Conclusión

Este sistema de indicendias fue desarrollado con la arquitectura MVC de .NET Framework 4.8 junto a la tecnica de Code First. El sistema permite a los usuarios registrar, actualizar y eliminar incidencias, agregar comentarios con AJAX, a su vez se respetaron en lo mayor posible la convencion de nomenclatura para variables,constantes,archivos, modelos, clases etc... a su vez la estructura del proyecto y el desarrollo intuitivo para desenvolverse en la aplicación. No implementé procedimientos almacenados ya que me encuentro muy nuevo y aun aprendiendo para su correcto funcionamiento, se intentó pero no logré hacerlo funcionar a tiempo, a su vez la autenticación no la pude realizar por motivos de precariedad de tiempo.

## Tecnologias usadas
*   ASP.NET MVC 5

*   Entity Framework 6

*   SQL Server (Express o LocalDB)

*   PagedList.Mvc (para paginación)

*   jQuery y AJAX (para comentarios sin recarga)

*   Bootstrap 5 (para la interfaz de usuario)

## Funcionalidades
*   Gestión de incidencias (Crear, Editar, Eliminar, Listar con paginación y filtros).
*   Agregar comentarios con AJAX (sin recargar la página).
*   Paginación y Filtros (por estado, prioridad y búsqueda de texto).
*   Validaciones en el lado del cliente y servidor.

## Patrones y Prácticas Implementadas
* Repository Pattern: Para separar la lógica de acceso a datos del controlador.
* MVC (Modelo-Vista-Controlador): Arquitectura que separa la lógica de negocio de la presentación.
* Validaciones en Cliente y Servidor: Se usan DataAnnotations en los modelos y validaciones en el lado del cliente con jQuery Validation.
