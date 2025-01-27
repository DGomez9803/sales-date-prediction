# sales-date-prediction

 ## Tecnologías

- **.NET SDK**: 8.0
- **Base de Datos**: SQL Server (utilizado con SQL Server Management Studio - SSMS)
- **Angular CLI**: 19.1.4
- **Node**: v20.13.1
- **npm**: 10.5.2

## Modificaciones en .NET

Para poder ejecutar la API correctamente, se deben realizar las siguientes modificaciones:
### 1. Cadena de Conexión
La cadena de conexión está configurada en el archivo `appsettings.json`. Asegúrate de actualizarla con los valores correspondientes a tu entorno de base de datos.

![image](https://github.com/user-attachments/assets/f92dd73c-74b3-4cbd-b2c4-631ee5c912ab)

### 2. Configuración de CORS
En el archivo Program.cs, se configura CORS para permitir peticiones desde el entorno local. Esto es necesario para que el frontend Angular pueda comunicarse con la API.

Ejemplo de configuración de CORS en Program.cs:

![image](https://github.com/user-attachments/assets/5b7a00e4-9c27-4ee0-9642-4deb79af5cfc)
