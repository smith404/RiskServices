namespace FRMObjects.model
{
    public partial class ToolExecutionLog : BaseModelObject
    {
        public long Id { get; set; }
        public long ToolDefinitionId { get; set; }
        public string? Requestor { get; set; }
        public string RunConfiguration { get; set; } = null!;
        public Guid? Guid { get; set; }
        public bool Persist { get; set; }
        public bool ExitOnFail { get; set; }
        public string Status { get; set; } = null!;
        public byte StepNumber { get; set; }
        public DateTime RequestedTimestamp { get; set; }
        public DateTime? RunStartTimestamp { get; set; }
        public DateTime? RunEndTimestamp { get; set; }

        public virtual ToolDefinition ToolDefinition { get; set; } = null!;
    }
}
