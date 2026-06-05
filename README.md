# RestAPIDemo

RestAPIDemo es una API REST desarrollada en ASP.NET Core (C#) diseñada para recibir, consultar y registrar eventos (logs) provenientes de Zoho hacia una base de datos SQL Server.

## Controladores y Endpoints

La API se divide en tres controladores principales para segmentar el guardado de los registros:

*   **`ZohoController` (`/Zoho`)**: 
    *   `POST`: Recibe un modelo simple y lo registra en la tabla `tlbBitacora`.
*   **`LogZohoProduccionController` (`/LogZohoProduccion`)**: 
    *   `GET`: Obtiene los últimos 5 registros de la tabla de producción (`LogProduccion`), ordenados de manera descendente.
    *   `POST`: Recibe una lista de logs y registra de forma masiva los eventos en la tabla `LogProduccion`.
*   **`LogZohoPruebasController` (`/LogZohoPruebas`)**: 
    *   `GET`: Obtiene los últimos 5 registros de la tabla de pruebas (`LogPruebas`), ordenados de manera descendente.
    *   `POST`: Recibe una lista de logs y registra de forma masiva los eventos en la tabla `LogPruebas`.

## Configuración de Base de Datos

La conexión a la base de datos SQL Server se genera dinámicamente utilizando credenciales seguras. Para que la aplicación funcione, es necesario que el archivo `appsettings.json` incluya los siguientes parámetros de configuración:

```json
{
  "ServerBD": "NombreOIpDelServidor",
  "BDUsuario": "TuUsuario",
  "BDContrasena": "TuContrasena",
  "BDConexion": "NombreDeLaBaseDeDatos"
}
```

## Modelos de Datos

El sistema utiliza principalmente dos modelos para recibir las peticiones:
*   `SabanaModel`: Utilizado para la bitácora general (Nombre, FechaRegistro).
*   `SabanaLog`: Un modelo extenso con información detallada de eventos, roles, etapas, secuencias y pagos de tiempo. La API se encarga de parsear las fechas (formato `dd-MM-yy HH:mm:ss`) para integrarlas correctamente en la base de datos.

## Tecnologías
*   .NET Core / ASP.NET Core Web API
*   `System.Data.SqlClient` (ADO.NET puro)
*   SQL Server
