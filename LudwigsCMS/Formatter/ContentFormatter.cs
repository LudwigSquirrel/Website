using System.Text;
using System.Text.RegularExpressions;

namespace LudwigsCMS.Formatter;

public static class ContentFormatter
{
    private static Regex Regex = new Regex(@"{{2}(.*)}{2}"); // matches {{Property}} where Property is the captured group

    public static string FormatWith(this string format, object obj)
    {
        StringBuilder builder = new StringBuilder(format);

        var matches = Regex.Matches(format);
        
        foreach (Match match in matches)
        {
            string propName = match.Groups[1].Value;

            var propertyInfo = obj.GetType().GetProperty(propName);
            if (propertyInfo != null)
            {
                builder.Replace($"{{{{{propName}}}}}", propertyInfo.GetValue(obj)?.ToString());
            }
        }

        return builder.ToString();
    }
}