namespace Lionheart.Application.Interfaces.Services;

public interface IValidationService<TModel> where TModel : class
{
    Dictionary<string, IEnumerable<string>> Validate(TModel model);
    bool IsValid(TModel model);
}
