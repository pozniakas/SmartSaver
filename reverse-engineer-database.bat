REM dotnet tool install -g dotnet-ef
dotnet tool install --global dotnet-ef

dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f

pause