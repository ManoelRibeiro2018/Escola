namespace Escola.Domain.Dtos
{
    public class ResponseGeneric
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public static ResponseGeneric Failure(Exception ex) => new()
        {
            Message = ex.Message,
            Success = false,            
        };

        public static ResponseGeneric Successful() => new()
        {
            Message = "Ação realiza com sucesso",
            Success = true,
        };
    }
}
