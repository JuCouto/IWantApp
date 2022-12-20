
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager)
    {

        var user = new IdentityUser { UserName = employeeRequest.Email, Email = employeeRequest.Email };
        var result = userManager.CreateAsync(user, employeeRequest.Password).Result;

        if (!result.Succeeded)
            return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

        var userClaims = new List<Claim>
        {
            new Claim("EmplyeeCode", employeeRequest.EmployeeCode),
            new Claim("Name", employeeRequest.Name)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        // var claimResult = userManager.AddClaimAsync(user, new Claim("Name", employeeRequest.Name)).Result; 1 claim por linha

        if (!claimResult.Succeeded)
            return Results.BadRequest(result.Errors.First());

        return Results.Created($"/categories/{user.Id}", user.Id); // Após realizar o post, irá mostrar apenas o id que foi criado .
    }
}
