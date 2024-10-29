using FluentValidation;

namespace Backend.Features.User.Configurations.Store;

public class ConfigurationValidator : AbstractValidator<ConfigurationModel>
{
    public ConfigurationValidator()
    {
        RuleFor(x => x.Property).NotEmpty();
        RuleFor(x => x.Value).NotEmpty();
    }
}

public class Validator : AbstractValidator<ConfigurationStoreReq>
{
    public Validator()
    {
        RuleFor(x => x.Configurations).NotEmpty();
        RuleForEach(x => x.Configurations).SetValidator(new ConfigurationValidator());
    }
}
