namespace FRMObjects.model
{
    public partial class ToolDefinition : BaseModelObject
    {
        public ToolDefinition()
        {
            ToolExecutionLogs = new HashSet<ToolExecutionLog>();
            ToolStepConfigs = new HashSet<ToolStepConfig>();
        }

        public long Id { get; set; }
        public string ToolName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Url { get; set; }
        public byte Version { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<ToolExecutionLog> ToolExecutionLogs { get; set; }
        public virtual ICollection<ToolStepConfig> ToolStepConfigs { get; set; }
    }
}
