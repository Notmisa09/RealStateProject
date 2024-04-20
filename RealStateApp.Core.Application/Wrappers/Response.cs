namespace RealStateApp.Core.Application.Wrappers;

public class Response<T>
{
    
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; set; }
    
    public Response()
    {
    }
    
    //Falla o respuesta inesperada
    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }
    
    //Respuesta esperada
    public Response(T data, string? message = null)
    {
        Succeeded = true;
        Message = message ?? "";
        Data = data;
    }
    
    
}