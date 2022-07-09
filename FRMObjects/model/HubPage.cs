namespace FRMObjects.model
{
    public partial class HubPage : BaseModelObject
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Headline { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Body { get; set; } = null!;
        public string? Url { get; set; }
    }
}
