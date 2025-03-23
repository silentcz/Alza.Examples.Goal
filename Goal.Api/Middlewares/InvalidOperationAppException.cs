namespace Goal.API.Middlewares;

public class InvalidOperationAppException(string message) : Exception(message);
