PruDouble V Partners
Bienvenidos a este repositorio. Aquí encontrarás la Prueba Back End la cual consiste en registar usuarios con entidades de tipo persona ademas solicita usuario y password para su respectivo logueo y porde ingresar al sistema donde puede visualizar los usuarios registrados

Tecnologías Utilizadas
Base de Datos: SQL Server
*C# .NET CORE 6 con Microsoft Visual Studio 2022
Instrucciones de Configuración
Crear Base de Datos:

Crear una base de datos local .
Configuración del Proyecto:

Ajustar la instancia de la base de datos en el archivo appsettings.json. En la sección "DefaultConnection", especificar la instancia del servidor y el nombre de la base de datos.
Migraciones:

Navegar al proyecto Persistence.Database.
Ejecutar los siguientes comandos para aplicar las migraciones: add-migration mas el  de la migracion y por ultimo update-database
Ejecutar la API:

Iniciar la API.
Se cargará Swagger automáticamente para realizar pruebas de la API.
Uso de la API
Una vez ejecutada la API, puedes utilizar Swagger para interactuar con los endpoints y realizar operaciones como crear usuarios, mostrar usuarios.
