# PruebaProgramacion

## Instalación general
Para la creacion de la base de datos inicial se deberá ejecutar el comando: `Add-Migration InitDB` y luego `Update-Database InitDB`.
Con la creación del usuario se deberá especificar el id de organizacion, el slug de esa organización es la que debe se debe usar para setear la base correspondiente.

## Instalación de cada Organización
Pasos para el agregado de una nueva organización:

1. En el archivo `appsettings.json`, en la seccion `ConnectionString`, una entrada con el nombre del slug y su correspondiente string de conexión.
2.  En la carpeta Models se deberá agregar un contexto derivado de la clase `ProductContext`. Recordar agregar el constructor que reciba el `DbContextOptions` como parámetro.
3.  En el archivo `AutoFacModule.cs` a partir de la linea 15 se debe agregar el contexto nuevo y como key el nombre del slug asociado a esa base.
4.  En el archivo `Program.cs` a partir de la linea 28 se debe agregar el contexto nuevo junto con el nombre de donde esta configurado el string de conexion dentro de `appsetings.json`.
5.  Para generar las tablas para el slug correspondiente ejecutar el comando `Add-Migration -Context Product2Context` y luego `Update-Database -Context Product2Context` donde `Product2Context` es el nombre de la clase contexto correspondiente al slug a crear.
   
