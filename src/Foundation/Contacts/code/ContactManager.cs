using Sitecore.Analytics;
using Sitecore.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Collection.Model;
using System;
using System.Linq;

namespace Noesis.Foundation.Contacts
{
    public class ContactManager
    {
        public static void AddGoal(Guid goalId)
        {
            using (var client = Sitecore.XConnect.Client.Configuration.SitecoreXConnectClientConfiguration.GetClient())
            {
                var newContact = new Contact();
                client.AddContact(newContact);

                var channelId = Tracker.Current.Interaction.ChannelId;
                var userAgent = Tracker.Current.Interaction.UserAgent;
                var interaction = new Interaction(newContact, InteractionInitiator.Brand, channelId, userAgent);

                var excludesUrlParts = new[] { "/api/" };
                var pages = Tracker.Current.Interaction.Pages.Reverse();
                var currentPage = pages.FirstOrDefault(page => page?.Url?.Path != null && !excludesUrlParts.Any(s => page.Url.Path.Contains(s)));

                if (currentPage != null)
                {
                    var pageViewEvent = new PageViewEvent(DateTime.UtcNow, currentPage.Item.Id, currentPage.Item.Version, currentPage.Item.Language)
                    {
                        Url = currentPage.Url?.Path
                    };

                    interaction.Events.Add(pageViewEvent);

                    var goalFromTracker = Tracker.MarketingDefinitions.Goals[goalId];

                    var goalFromXConnect = new Goal(goalId, DateTime.UtcNow)
                    {
                        EngagementValue = goalFromTracker.EngagementValuePoints,
                        ParentEventId = pageViewEvent.Id
                    };

                    interaction.Events.Add(goalFromXConnect);

                    client.AddInteraction(interaction);

                    client.Submit();
                }
            }
        }
    }
}