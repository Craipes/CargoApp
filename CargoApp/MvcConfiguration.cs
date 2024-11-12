using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CargoApp;

public class MvcConfiguration : IConfigureOptions<MvcOptions>
{
    private readonly IStringLocalizer<AnnotationsSharedResource> _stringLocalizer;

    public MvcConfiguration(IStringLocalizer<AnnotationsSharedResource> stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
    }

    public void Configure(MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(name => _stringLocalizer["Number Error", name]);
    }
}
