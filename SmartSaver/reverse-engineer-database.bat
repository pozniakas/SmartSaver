
dotnet tool update --global dotnet-ef --version 3.1.9

dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f

pause