# Examen1_API

## Descripción

API RESTful desarrollada en .NET utilizando Entity Framework Core y SQLite como base de datos. La API permite gestionarla creación, lectura, actualización y eliminación (CRUD) de cursos y estudiantes. Además, soporta la carga de imágenes asociadas a los cursos.

---

## Requisitos Previos

- **.NET SDK**: Versión 8.0 o superior.
- **SQLite**: Incluido en el proyecto.
- **Visual Studio Code** o cualquier editor compatible con .NET

---

## Configuración del Proyecto

1. **Clonar el Repositorio**:
   ```bash
   git clone <URL_DEL_REPOSITORIO>
   cd Examen1_API
   ```

2. **Configurar la Base de Datos**:
   - La conexión a la base de datos está definida en `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=cursos.db"
     }
     ```
   - Si necesitas regenerar la base de datos, ejecuta:
     ```bash
     dotnet ef database update
     ```

3. **Ejecutar la Aplicación**:
- Estando en el directorio del proyecto
   ```bash
   dotnet watch run
   ```
   - La API estará disponible en:
     - **HTTP**: `http://localhost:5241`
     - **HTTPS**: `https://localhost:7185`

4. **Explorar la Documentación**:
   - Accede a Swagger UI en: `http://localhost:5241/swagger`

---

## Endpoints Principales

### Cursos

- **GET** `/api/Course`: Lista todos los cursos.
- **GET** `/api/Course/{id}`: Obtiene un curso por ID.
- **POST** `/api/Course`: Crea un nuevo curso (requiere un archivo de imagen).
- **PUT** `/api/Course/{id}`: Actualiza un curso existente.
- **DELETE** `/api/Course/{id}`: Elimina un curso.
- **GET** `/api/Course/with-students`: Lista cursos con sus estudiantes.
- **GET** `/api/Course/{id}/with-students`: Obtiene un curso por ID.

### Estudiantes

- **GET** `/api/Student`: Lista todos los estudiantes.
- **POST** `/api/Student`: Crea un nuevo estudiante.
- **PUT** `/api/Student/{id}`: Actualiza un estudiante existente.
- **DELETE** `/api/Student/{id}`: Elimina un estudiante.

---

## Validaciones

- **Cursos**:
  - El nombre del curso debe ser único (ignora acentos y mayúsculas).
  - El nombre del profesor es obligatorio.

- **Estudiantes**:
  - El correo electrónico debe ser único y válido.
  - El teléfono debe ser único.
  - El estudiante debe estar asociado a un curso válido.

---

## Migraciones de Base de Datos

1. **Crear una Nueva Migración**:
   ```bash
   dotnet ef migrations add <NombreDeLaMigracion>
   ```

2. **Aplicar Migraciones**:
   ```bash
   dotnet ef database update
   ```

3. **Nota**: Las migraciones están excluidas del repositorio (`.gitignore`). Si necesitas incluirlas, elimina `Migrations/` del archivo `.gitignore`.

---

## Estructura del Proyecto

```
examenAPI/
├── Controllers/         # Controladores de la API
├── Data/                # Configuración de la base de datos (AppDbContext)
├── Dtos/                # Data Transfer Objects (DTOs)
├── Migrations/          # Migraciones de Entity Framework Core
├── Models/              # Modelos de datos (Course, Student)
├── Validators/          # Validadores de FluentValidation
├── UploadedImages/      # Carpeta para imágenes subidas
├── Program.cs           # Configuración principal de la aplicación
├── appsettings.json     # Configuración de la aplicación
```

---
