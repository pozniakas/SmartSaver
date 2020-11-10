
dotnet tool update --global dotnet-ef --version 3.1.9
REM dotnet tool update --global dotnet-ef --version 5.0.0-preview.8.20407.4
REM --namespace DbEntities

dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --project ../../WebAPI -o ../DbEntities/Models -f

pause