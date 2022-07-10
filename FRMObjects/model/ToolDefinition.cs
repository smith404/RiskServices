namespace FRMObjects.model
{
    public partial class ToolDefinition : BaseModelObject
    {
        public ToolDefinition()
        {
            ToolName = "New Tool";
            Description = "New Tool";
            Url = "#";
            Status = "A";
        }

        public long Id { get; set; }
        public string ToolName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Url { get; set; }
        public byte Version { get; set; }
        public string Status { get; set; } = null!;
    }
}
