
# Hosting de Servidores de Videojuegos

API REST desarrollada con ASP.NET Core 10 para la gestión de un sistema de hosting de servidores de videojuegos.

## Tecnologías utilizadas

- C# / ASP.NET Core 10
- Entity Framework Core 10
- PostgreSQL
- Docker
- Docker Compose
- Scalar (documentación y pruebas de API)

## Requisitos

- Docker y Docker Compose
- Git

## Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/zDANIEL-BOT/Host_Server_Videojuego.git
cd Host_Server_Videojuego
```

### 2. Configurar variables de entorno

Crear el archivo `.env` a partir de la plantilla:

```bash
cp .env.example .env
```

Editar `.env` y configurar las credenciales de PostgreSQL.

### 3. Iniciar los servicios

```bash
docker compose --env-file .env -f constructor.yml up --build -d
```

### 4. Acceder a la API

- API: http://localhost:5000
- Scalar: http://localhost:5000/scalar/v1
- OpenAPI: http://localhost:5000/openapi/v1.json

## Endpoints de Usuarios

| Método | Endpoint | Descripción |
|---|---|---|
| POST | /api/usuarios | Crear usuario |
| GET | /api/usuarios | Listar usuarios |
| GET | /api/usuarios/{id} | Consultar usuario |
| PUT | /api/usuarios/{id} | Actualizar usuario |
| DELETE | /api/usuarios/{id} | Eliminar usuario |

## Base de datos

La aplicación utiliza PostgreSQL y Entity Framework Core.

Las migraciones se encuentran en la carpeta `Migrations`.

Para aplicar el script SQL generado, con los servicios levantados:

```bash
docker compose --env-file .env -f constructor.yml exec -T postgres sh -c 'psql -U "$POSTGRES_USER" -d "$POSTGRES_DB"' < Migrations/script.sql
```

**Verificación:** las implementaciones fueron compiladas y probadas mediante solicitudes HTTP utilizando Scalar, verificando las respuestas de los endpoints y la persistencia de datos en PostgreSQL.
