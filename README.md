
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

## Uso de inteligencia artificial

Se utilizó ChatGPT como herramienta de apoyo para preparar el archivo `despliegue.yml`, siguiendo el requisito de ejecutar el backend mediante una imagen previamente publicada en Docker Hub, sin necesidad de compilar el código fuente.

### Consulta realizada

Se consultó a la IA cómo configurar un archivo Docker Compose que permitiera:

- Ejecutar el backend utilizando la imagen `zdanielxbot/hosting-servidores-videojuegos:1.0`.
- Iniciar un contenedor de PostgreSQL.
- Configurar las variables de entorno para la conexión entre la API y la base de datos.
- Configurar una red compartida entre ambos servicios.
- Utilizar un volumen persistente para los datos de PostgreSQL.
- Esperar a que PostgreSQL estuviera saludable antes de iniciar la API.

### Verificación de la propuesta

La configuración sugerida fue revisada y probada mediante los siguientes procedimientos:

1. Se validó la sintaxis y configuración del archivo con `docker compose config`.
2. Se descargó la imagen publicada desde Docker Hub utilizando `docker compose pull api`.
3. Se inició PostgreSQL mediante el archivo `despliegue.yml`.
4. Se aplicó el script SQL de migraciones para crear las tablas necesarias en la base de datos nueva.
5. Se inició el backend utilizando la imagen descargada, sin ejecutar `docker build` ni compilar el código fuente.
6. Se verificó el estado de los contenedores y sus registros mediante `docker compose ps` y `docker compose logs api`.
7. Se accedió a Scalar y se probaron las operaciones CRUD de usuarios.

Como resultado, se comprobó que la API podía ejecutarse utilizando la imagen publicada en Docker Hub y conectarse correctamente con PostgreSQL, sin depender de la compilación del código fuente local.

### Herramienta utilizada

- ChatGPT: asistencia en la elaboración, revisión y explicación de la configuración Docker Compose.
