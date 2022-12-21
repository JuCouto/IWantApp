using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Endpoints.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Npgsql;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace IWantApp.Endpoints.Categories;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    // Método com DAPPER 
    public static IResult Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        return Results.Ok(query.Execute(page.Value, rows.Value));
    }

    // Método sem o DAPPER.
    //public static IResult Action(int page, int rows, UserManager<IdentityUser> userManager) // PAGINAÇÂO
    //{
    //    var users = userManager.Users.Skip((page - 1) * rows).Take(rows).ToList();
    //    var employees = new List<EmployeeResponse>();
    //    foreach (var user in users)
    //    {
    //        var claims = userManager.GetClaimsAsync(user).Result;
    //        var claimName = claims.FirstOrDefault(c => c.Type == "Name");
    //        var userName = claimName != null ? claimName.Value : string.Empty;
    //        employees.Add(new EmployeeResponse(user.Email, userName));
    //    }
    //    return Results.Ok(employees);
    //}
}
