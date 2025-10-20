namespace Application.Models.Responses;

public class GenericResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
}
