using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Endpoints.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Npgsql;
using System.Xml.Linq;

namespace IWantApp.Endpoints.Categories;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // Método com DAPPER
    //public static IResult Action(int page, int? rows, IConfiguration configuration)
    //{

    //    var db = new NpgsqlConnection(configuration["ConnectionString:IWantDb"]);
    //    string query = @"select 'Email', 'ClaimValue' as Name from AspNetUsers anu INNER JOIN AspNetUserClaims anuc ON anu.Id = anuc.UserId and ClaimType = 'Name' order by name OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

    //    var employees = db.Query<EmployeeResponse>(query, new {page, rows}
    //        );

    //    return Results.Ok(employees);
    //}

    // Método sem o DAPPER.
    public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager) // PAGINAÇÂO
    {
        var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
        var employees = new List<EmployeeResponse>();
        foreach (var user in users)
        {
            var claims = userManager.GetClaimsAsync(user).Result;
            var claimName = claims.FirstOrDefault(c => c.Type == "Name");
            var userName = claimName != null ? claimName.Value : string.Empty;
            employees.Add(new EmployeeResponse(user.Email, userName));
        }
        return Results.Ok(employees);
    }
}
