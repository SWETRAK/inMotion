using System.Runtime.Serialization;
using FluentValidation.Results;

namespace IMS.Shared.Utils.Exceptions;

public class InvalidIncomingDataException: Exception
{
    public ValidationResult ValidationErrors { get; set; }

    public InvalidIncomingDataException(ValidationResult validationErrors): base()
    {
        ValidationErrors = validationErrors;
    }

    protected InvalidIncomingDataException(SerializationInfo info, StreamingContext context, ValidationResult validationResult) : base(info, context)
    {
        ValidationErrors = validationResult;
    }

    public InvalidIncomingDataException(string? message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult;
    }

    public InvalidIncomingDataException(string? message, Exception? innerException, ValidationResult validationResult) : base(message, innerException)
    {
        ValidationErrors = validationResult;
    }
}