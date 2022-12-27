using Dapper;
using IWantApp.Endpoints.Employees;
using Npgsql;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWithClaimName
{
	private readonly IConfiguration configuration;

	public QueryAllUsersWithClaimName(IConfiguration configuration)
	{
		this.configuration = configuration;
	}

    // Método com DAPPER (não está fazendo a pesquisa)
    public async Task<IEnumerable<EmployeeResponse>> Execute(int page, int? rows)
    {
       
        var db = new NpgsqlConnection(configuration["ConnectionString:IWantDb"]);
        string query = "select \"Email\", \"ClaimValue\" as \"Name\" from \"AspNetUsers\" anu INNER " +
            "JOIN \"AspNetUserClaims\" anuc " +
            "ON anu.\"Id\" = anuc.\"UserId\" and \"ClaimType\" = \'Name\' ";

        return await db.QueryAsync<EmployeeResponse>(query, new { page, rows }
            );

        //return Results.Ok(employees);
    }
}
