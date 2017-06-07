#
# ScaffoldDbContext.ps1
#
dotnet ef dbcontext scaffold "Server=(localdb)\MSSQLLocalDB;Database=MetricServer;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models -f