namespace TimelineExample.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TimelineExample.Services.Activity;
    using TimelineExample.Services.Caching;

    using Windows.ApplicationModel.UserActivities;

    public class DataService : IDataService
    {
        private readonly IDataCacheProvider cacheProvider;

        private UserActivitySession currentActivity;

        public DataService(IDataCacheProvider cacheProvider)
        {
            this.cacheProvider = cacheProvider;
        }

        public MyTextItem CreateItem()
        {
            MyTextItem item = new MyTextItem
                           {
                               Id = Guid.NewGuid(),
                               Created = DateTime.UtcNow,
                               LastModified = DateTime.UtcNow
                           };

            this.cacheProvider?.AddOrUpdate(item.Id.ToString(), item);

            return item;
        }

        public IEnumerable<MyTextItem> GetItems(Func<MyTextItem, DateTime> keySelector)
        {
            return this.cacheProvider?.GetAll<MyTextItem>().OrderByDescending(keySelector);
        }

        public MyTextItem GetItem(Guid id)
        {
            return this.cacheProvider?.Get<MyTextItem>(id.ToString());
        }

        public async Task SaveItemAsync(Guid id, string content)
        {
            MyTextItem item = this.GetItem(id);
            if (item == null)
            {
                return;
            }

            item.Content = content;
            item.LastModified = DateTime.UtcNow;

            this.cacheProvider.AddOrUpdate(item.Id.ToString(), item);

            await this.GenerateTimelineActivityAsync(item);
        }

        public void DeleteItem(Guid id)
        {
            this.cacheProvider.Remove(id.ToString());
        }

        public void DeleteItem(MyTextItem item)
        {
            if (item != null)
            {
                this.DeleteItem(item.Id);
            }
        }

        private async Task GenerateTimelineActivityAsync(MyTextItem item)
        {
            UserActivityChannel channel = UserActivityChannel.GetDefault();
            UserActivity userActivity = await channel.GetOrCreateUserActivityAsync(item.Id.ToString());

            userActivity.VisualElements.DisplayText = item.Content;
            userActivity.VisualElements.Description = $"Last saved: {item.LastModified}";
            userActivity.ActivationUri = new Uri($"mytext://editor?id={item.Id}");

            await userActivity.SaveAsync();

            this.currentActivity?.Dispose();
            this.currentActivity = userActivity.CreateSession();
        }
    }
}