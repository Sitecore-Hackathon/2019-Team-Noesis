using System;
using Noesis.Foundation.SitecoreExtensions.Extensions;
using Noesis.Foundation.SitecoreExtensions.Models;
using Sitecore.Data.Items;

namespace Noesis.Feature.PageStructure.Models
{
    public class PageSectionViewModel
    {
        public string BackgroundColor { get; internal set; }

        public bool HasBackgroundColor => !string.IsNullOrEmpty(BackgroundColor);

        public Image BackgroundImage { get; internal set; }

        public bool HasImage => !string.IsNullOrEmpty(BackgroundImage?.Path);

        public string BackgroundVideoPath { get; internal set; }

        public bool HasBackgroundVideo => !string.IsNullOrEmpty(BackgroundVideoPath);

        public bool Autoplay { get; internal set; }

        public Guid GoalId { get; internal set; }

        public string Anchor { get; internal set; }

        public string Id { get; internal set; }

        public string RenderingCssClass { get; internal set; }

        public PageSectionViewModel(Item item)
        {
            if (item != null)
            {
                Id = !string.IsNullOrWhiteSpace(Anchor) ? Anchor : $"id-{Guid.NewGuid():N}";

                InitFields(item);
                InitRenderingParameters();
            }
        }

        private void InitFields(Item item)
        {
            BackgroundColor = item.GetText(Templates.PageSection.Fields.BackgroundColor);
            BackgroundImage = item.GetImage(Templates.PageSection.Fields.BackgroundImage);
            BackgroundVideoPath = item.GetInternalLinkMedia(Templates.PageSection.Fields.InternalVideo);
            Autoplay = item.GetCheckBoxValue(Templates.PageSection.Fields.Autoplay);
            GoalId = item.GetSelectedIdFromDropdown(Templates.PageSection.Fields.GoalId).Guid;
        }

        private void InitRenderingParameters()
        {
            var renderingContext = Sitecore.Mvc.Presentation.RenderingContext.CurrentOrNull;

            if (renderingContext != null)
            {
                RenderingCssClass = renderingContext.Rendering.RenderingCssClass("page-section");
                Anchor = renderingContext.Rendering.Parameters["Anchor"] ?? string.Empty;
            }
        }
    }
}