using IWantApp.Domain.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace IWantApp.Endpoints.Categories;

public class CategoryPost
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(CategoryRequest categoryRequest, ApplicationDbContext context)
    {
        // Validação normal, mas ficaria grandevalidar tudo.
    //    if (string.IsNullOrEmpty(categoryRequest.Name))
    //        return Results.BadRequest("Name is required");

        var category = new Category(categoryRequest.Name, "teste", "teste");

        // As validações vêm do contrato
        if (!category.IsValid)
        {
            
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());
                       
        }

        context.Categories.Add(category);
        context.SaveChanges();

        return Results.Created($"/categories/{category.Id}", category.Id); // Após realizar o post, irá mostrar apenas o id que foi criado .
    }
}
