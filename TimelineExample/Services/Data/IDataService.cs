namespace TimelineExample.Services.Activity
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TimelineExample.Services.Data;

    public interface IDataService
    {
        MyTextItem CreateItem();

        IEnumerable<MyTextItem> GetItems(Func<MyTextItem, DateTime> keySelector);

        MyTextItem GetItem(Guid id);

        Task SaveItemAsync(Guid id, string content);

        void DeleteItem(Guid id);

        void DeleteItem(MyTextItem item);
    }
}