namespace FRMObjects.model
{
    public partial class Notification : BaseModelObject
    {
        public long Id { get; set; }
        public string Type { get; set; } = null!;
        public string Icon { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string Owner { get; set; } = null!;
        public string Sender { get; set; } = null!;
        public bool Processed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ProcessedOn { get; set; }
    }
}
