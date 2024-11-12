using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace CargoApp;

public static class EnumExtensions
{
    public static string GetDisplayNameContext(this Enum enumValue, HttpContext context)
    {
        var culture = context.Features.Get<IRequestCultureFeature>()?.RequestCulture?.Culture;
        return GetDisplayName(enumValue, culture);
    }

    public static string GetDisplayName(this Enum enumValue, CultureInfo? culture)
    {
        var displayAttribute = enumValue.GetType()
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>();

        if (culture == null) return displayAttribute?.GetName() ?? enumValue.ToString();

        // Retrieve the localized name with the specified culture
        return displayAttribute?.GetName()?.Localize(culture) ?? enumValue.ToString();
    }

    private static string? Localize(this string resourceName, CultureInfo culture)
    {
        ResourceManager resourceManager = new(typeof(AnnotationsSharedResource));
        return resourceManager.GetString(resourceName, culture);
    }
}