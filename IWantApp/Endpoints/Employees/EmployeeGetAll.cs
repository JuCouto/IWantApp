using Dapper;
using IWantApp.Domain.Products;
using IWantApp.Endpoints.Employees;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "EmployeePolicy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query)
    {
        var result = await query.Execute(page, rows);
        return Results.Ok(result);
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
