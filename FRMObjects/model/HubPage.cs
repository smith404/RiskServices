namespace FRMObjects.model
{
    public partial class HubPage : BaseModelObject
    {
        public HubPage()
        {
            Title = "New Page";
        }

        public long Id { get; set; } = 0;
        public string Guid { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public string Status { get; set; } = null!;

    }
}
