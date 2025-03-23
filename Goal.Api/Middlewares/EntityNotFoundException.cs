namespace Goal.API.Middlewares;

public class EntityNotFoundException(string entityName, object id)
    : Exception($"Entity '{entityName}' with id '{id}' not found.");
