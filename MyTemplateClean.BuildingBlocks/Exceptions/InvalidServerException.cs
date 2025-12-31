namespace MyTemplateClean.BuildingBlocks.Exceptions;

public class InvalidServerException : Exception
{
    public InvalidServerException(string message) : base(message)
    {
    }
    public InvalidServerException(string message, string Details) : base(message)
    {
        this.Details = Details;
    }
    public string? Details { get; set; }
}