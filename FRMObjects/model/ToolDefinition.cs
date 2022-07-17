namespace FRMObjects.model
{
    public partial class ToolDefinition : BaseModelObject
    {
        public ToolDefinition()
        {
            ToolName = "New Tool";
            Status = "D";
        }

        public long Id { get; set; } = 0;
        public string ToolName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Url { get; set; }
        public byte Version { get; set; }
        public string Status { get; set; } = null!;
    }
}
