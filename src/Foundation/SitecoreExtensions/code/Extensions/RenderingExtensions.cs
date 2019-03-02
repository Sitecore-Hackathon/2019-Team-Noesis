using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Mvc.Presentation;

namespace Noesis.Foundation.SitecoreExtensions.Extensions
{
    public static class RenderingExtensions
    {
        public static string RenderingCssClass(this Rendering rendering, string extraClass = null)
        {
            var parameters = new Dictionary<string, string>
            {
                {"GridParameters", "Class"}, 
                {"Styles", "Value"}
            };

            var classes = string.Empty;

            foreach (var parameter in parameters)
            {
                if (rendering.Parameters.Contains(parameter.Key))
                {
                    var parameterValue = rendering.Parameters[parameter.Key];

                    var parameterValueItems = parameterValue.Split('|');

                    classes = parameterValueItems.Aggregate(classes, (current, item) => current + " " + Context.Database.GetItem(item)[parameter.Value]).Trim();
                }
            }

            return !string.IsNullOrEmpty(classes) || !string.IsNullOrEmpty(extraClass)
                ? $"class='{classes} {extraClass}'"
                : string.Empty;
        }
    }
}