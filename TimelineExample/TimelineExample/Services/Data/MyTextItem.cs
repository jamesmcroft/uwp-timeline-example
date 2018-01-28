namespace TimelineExample.Services.Data
{
    using System;

    public class MyTextItem
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}