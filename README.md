# Asisto-TAD-backend-TEST

# Crear y lanzar migración:
1. dotnet ef migrations add NOMBRE --project UserManagemenet.Infrastructure --startup-project UserManagemenet.API
2. dotnet ef database update --project UserManagemenet.Infrastructure --startup-project UserManagemenet.API

# Deshacer migración ejecutada en BBDD
1. dotnet ef database update NOMBRE_MIGRACION_ANTERIOR --project UserManagemenet.Infrastructure --startup-project UserManagemenet.API
2. dotnet ef migrations list --project UserManagemenet.Infrastructure --startup-project UserManagemenet.API 
3. dotnet ef migrations remove --project UserManagemenet.Infrastructure --startup-project UserManagemenet.API