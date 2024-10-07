namespace MicroServicioCM.Entidades.Response
{
    public class ResponseInformation
    {
        public object? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
    }
}
