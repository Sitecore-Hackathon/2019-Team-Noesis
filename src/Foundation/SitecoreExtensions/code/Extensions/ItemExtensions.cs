using Noesis.Foundation.SitecoreExtensions.Models;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;
using Sitecore.Data.Managers;

namespace Noesis.Foundation.SitecoreExtensions.Extensions
{
    public static class ItemExtensions
    {
        public static Image GetImage(this Item item, ID id)
        {
            var image = (ImageField)item?.Fields[id];

            if (image?.MediaItem != null)
            {
                return new Image
                {
                    Path = MediaManager.GetMediaUrl(image.MediaItem),
                    AltText = image.Alt
                };
            }

            return new Image();
        }

        public static string GetInternalLinkMedia(this Item item, ID id)
        {
            var mediaPath = item?.Fields[id]?.Value;

            MediaItem mediaItem = Sitecore.Context.Database.GetItem(mediaPath);

            return mediaItem != null
                ? MediaManager.GetMediaUrl(mediaItem)
                : null;
        }

        public static string GetText(this Item item, ID id)
        {
            return item?[id];
        }

        public static bool GetCheckBoxValue(this Item item, ID id)
        {
            var check = (CheckboxField)item?.Fields[id];

            return check.Checked;
        }

        public static ID GetSelectedIdFromDropdown(this Item item, ID id)
        {
            var field = (LookupField)item?.Fields[id];

            return field?.TargetItem?.ID ?? ID.Null;
        }

        public static bool IsDerived(this Item item, ID templateId)
        {
            if (item == null)
            {
                return false;
            }

            return !templateId.IsNull && item.IsDerived(item.Database.Templates[templateId]);
        }

        private static bool IsDerived(this Item item, Item templateItem)
        {
            if (item == null)
            {
                return false;
            }

            if (templateItem == null)
            {
                return false;
            }

            var itemTemplate = TemplateManager.GetTemplate(item);
            return itemTemplate != null &&
                   (itemTemplate.ID == templateItem.ID || itemTemplate.DescendsFrom(templateItem.ID));
        }
    }
}
