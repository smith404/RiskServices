﻿namespace FRMObjects.model
{
    public partial class HubPage : BaseModelObject
    {
        public HubPage()
        {
            Title = "New Page";
            Guid = "";
            Headline = "New Headline";
            Summary = "New Summary";
            Image = "None";
            Body = "New Body";
            Url = "#";
            Owner = "";
            Status = "A";
        }

        public long Id { get; set; }
        public string Guid { get; set; }
        public string Title { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Url { get; set; }
        public string Owner { get; set; } = null!;
        public string Status { get; set; } = null!;

    }
}
