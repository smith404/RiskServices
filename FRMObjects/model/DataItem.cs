namespace FRMObjects.model
{
    public partial class DataItem : BaseModelObject
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string Display { get; set; } = null!;
    }
}
