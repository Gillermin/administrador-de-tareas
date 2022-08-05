# Administrador de tareas
Este está desarrollado en 2 partes, el proyecto WebAPI que es nuestro API, para conectarnos a una base de datos PostgreSQL para manejar el backend. El segundo desarrollo es el WebUI que contiene el frontend.

El proyecto consiste en un administrador básico de tareas, este cuenta con:
1. Descripción: Campo que describe de que trata la tarea. 
2. Colaborador: Una lista de colaboradores preexistentes (Nombre), que se cargan de la base de datos.
3. Estado: Indica el estado actual de la tarea (PENDIENTE, EN PROCESO, FINALIZADA).
4. Prioridad: Indica el nivel de “importancia” que se le debe tener al atender las tareas asignadas (ALTA, MEDIA, BAJA).
5. Fechas de Inicio y Fin: Indica la fecha de inicio/fin de la tarea.
6. Notas: Permite guardar notas asociadas a la tarea.

Básicamente consiste en un CRUD de tareas, consta con un filtro para el listado de las tareas, y ciertas validaciones en editar y borrar tareas como:
- No se permitirán editar las tareas que estén FINALIZADAS.
- No se permitirán eliminar las tareas que estén EN PROCESO.

## Pre-requisitos
- Ambos proyectos estan desarrollados en C#, por lo que requeriremos tener instalado _Visual Studio 2022_ (la utilizada para este desarrollo).
- También requeriremos tener instalado el _PGAdmin_ para trabajar nuestra base de datos en PostgreSQL.

## Instalación y despliegue
1. Primeramente debemos ir a nuestro _PGAdmin_, (previamente instalado) y crear una base de datos con el nombre "TaskDB", una vez creada, daremos click-derecho y en la opcion Query Tool, luego ejecutaremos en contenido de nuestro _query.sql_ que se encuentra en la raíz de este repositorio. Con esta instrucción tendremos creado la base de datos a la que accederá la API.
2. Seguido abriremos el proyecto WebAPI en _Visual Studio 2022_ (previamente instalado) y en el archivo _appsettings.json_ que se encuentra en la raíz de este proyecto, encontraremos el espacio donde deberemos incluir nuestro string de conexión.
   - En caso de utilizar un puerto o una clave distinta en el _PGAdmin_, debemos hacer el cambio en esta línea de código.
```json
"ConnectionStrings": {
    "PostgreSQLConnection": "Server=127.0.0.1;Port=5432;Database=TaskDB;User Id=postgres;Password=admin;"
  }
```
3. Seguidamente debemos ejecutar el proyecto.
   - Este nos abrirá en el browser que tengamos por defecto la herramienta Swagger para probar nuestro API. Para este, tenemos que usar la siguientes estructuras JSON en los métodos correspondientes.
   - Por defecto nos abre siguiente URL https://localhost:44359 en caso de que cambie, ya indicaremos donde cambiar este valor para consumir nuestro API desde el proyecto frontend. Seguido irá la ruta de nuestro API, para este caso [/api/Task](https://localhost:44359/api/Task).
   - Para los métodos [GET, POST, PUT, DELETE] usaremos la siguiente URL: https://localhost:44359/api/Task.

[GET] Obtiene todas las Tareas. 

[POST] Inserta una tarea, dada por el siguiente JSON:
```json
{
  "idCollaborator": 1,
  "description": "Descripción de la tarea",
  "status": "PENDIENTE",
  "priority": "BAJA",
  "fromDate": "2022-08-04",
  "toDate": "2022-08-04",
  "notes": "Nota de la tarea"
}
```

[PUT] Edita una tarea, dada por el siguiente JSON:
```json
{
  "idCollaborator": 2,
  "description": "Descripción de la tarea",
  "status": "EN PROCESO",
  "priority": "ALTA",
  "fromDate": "2022-08-04",
  "toDate": "2022-08-04",
  "notes": "Nota de la tarea"
}
```
[DELETE] Elimina una tarea.
- /api/Task/{id}

[GET] Obtiene los datos de una tarea en concreto.
- /api/Task/{id}

[GET] Obtiene la lista de los colaboradores.
- /api/Task/GetCollaborators

[POST] Obtiene la lista de las tareas filtradas, dadas por el siguiente JSON:
- /api/Task/Filter
```json
{
  "idCollaborator": 3,
  "status": "PENDIENTE",
  "priority": "MEDIA",
  "fromDate": "2022-08-04",
  "toDate": "2022-08-04"
}
```
   
3. Seguido abriremos el proyecto WebUI en _Visual Studio 2022_ (previamente instalado) y en el archivo _appsettings.json_ que se encuentra en la raíz de este proyecto, encontraremos el espacio donde deberemos incluir el resultado del URL base donde se esta ejecutando nuestra API. 
   - Para el caso de ejemplo mencionado anteriormente usaremos https://localhost:44359, debemos hacer el cambio en esta línea de código si no abre en la misma URL.
```json
"ApiSettings": {
    "baseUrl": "https://localhost:44359"
  }
```
4. Teniendo la configuración correcta solo queda ejecutar nuestro proyecto WebUI ver el funcionamiento por nosotros mismos.

