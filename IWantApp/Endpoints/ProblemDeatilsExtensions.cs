using Flunt.Notifications;

namespace IWantApp.Endpoints;

public static class ProblemDeatilsExtensions
{
    public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications)
    {
        // Preparando para receber mais de um erro.
        return notifications
            .GroupBy(g => g.Key)
            .ToDictionary(g => g.Key, g => g.Select(x => x.Message).ToArray());
       
    }
}
