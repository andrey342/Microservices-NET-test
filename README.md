# Asisto-TAD-backend-TEST
    
Backend desarrollado en **.NET 9** siguiendo una arquitectura de **microservicios**.
Se utilizan patrones y tecnologías como:

- **DDD** (Domain‑Driven Design)  
- **CQRS** (Command Query Responsibility Segregation)  
- **Unit of Work**  
- **Mediator** para la orquestación de comandos y consultas  
- **FluentValidation** para la validación de entrada  
- **Kafka** como **bus de eventos** para la comunicación asíncrona  
- **SQL Server** como base de datos relacional  

El despliegue completo se realiza con **Docker Compose**, lo que permite levantar todo el stack con un solo comando.

---

## Estructura de la solución

```
Asisto-TAD-backend-TEST/
├── src/
│   ├── ApiGateway/          # Puerta de enlace YARP y Swagger
│   ├── Shared/        # Configuraciones comunes para Kafka
│   └── UserManagement/         # Microservicio de Usuarios
├── docker-compose.yml       # Orquestación de contenedores
└── README.md
```

| Proyecto               | Descripción                                                                              |
|------------------------|------------------------------------------------------------------------------------------|
| **ApiGateway**         | Proxy inverso basado en **YARP** que enruta peticiones al resto de microservicios.       |
| **Shared**             | Biblioteca que centraliza la configuración de productores y consumidores de **Kafka**.   |
| **UserManagement**     | Microservicio responsable de la gestión de usuarios.                                     |
| **docker-compose.yml** | Define contenedores de API Gateway, microservicios, Kafka, Zookeeper y SQL Server.       |

---

## Puesta en marcha

### Requisitos previos

- **Docker Desktop** o motor Docker + Docker Compose v2  
- **.NET SDK 9** (solo si se va a compilar el código localmente)

### Pasos rápidos

```bash
git clone https://github.com/andrey342/Microservices-NET-test.git
```

Al desplegar, se exponen los siguientes endpoints Swagger:

| Servicio              | URL por defecto                                   |
|-----------------------|---------------------------------------------------|
| API Gateway           | <http://localhost:32710/swagger/index.html>       |
| Microservicio Usuario | <http://localhost:32701/swagger/index.html>       |

> **Nota:** si cambias los puertos o variables de entorno en `docker-compose.yml`, actualiza también las URL anteriores.