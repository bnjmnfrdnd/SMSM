using Interface.Controllers;
using Interface.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Syndication;

public class NotificationService : INotificationHandler
{
    public Rss20FeedFormatter GetNotification()
    {
        SyndicationFeed syndicationFeed = new SyndicationFeed("New request", "Test notification", new Uri(""));
        syndicationFeed.Authors.Add(new SyndicationPerson("benjaminferdinand@gmail.com"));
        syndicationFeed.Categories.Add(new SyndicationCategory("Syndication category"));
        syndicationFeed.Description = new TextSyndicationContent("Syndication description");

        SyndicationItem syndicationItem1 = new SyndicationItem(
            "Title 1",
            "Content goes here",
            new Uri("http://localhost/Content/One"), "syndicationItem1ID", DateTime.Now
        );

        List<SyndicationItem> syndicationItems = new List<SyndicationItem>();

        syndicationItems.Add(syndicationItem1);

        syndicationFeed.Items = syndicationItems;

        return new Rss20FeedFormatter(syndicationFeed);
    }
}
