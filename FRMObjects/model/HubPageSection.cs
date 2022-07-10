namespace FRMObjects.model
{
    public partial class HubPageSection : BaseModelObject
    {
        public HubPageSection()
        {
            Title = "New Section";
            Headline = "New Headline";
            Summary = "New Summary";
            Image = "None";
            Body = "New Body";
            Url = "#";
        }

        public long Id { get; set; }
        public long HubPageId { get; set; }
        public string Title { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
