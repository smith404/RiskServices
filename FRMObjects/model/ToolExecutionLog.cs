namespace FRMObjects.model
{
    public partial class ToolExecutionLog : BaseModelObject
    {
        public ToolExecutionLog()
        {
            Guid = "";
        }

        public long Id { get; set; }
        public string Guid { get; set; }
        public long ToolDefinitionId { get; set; }
        public string? Requestor { get; set; }
        public string RunConfiguration { get; set; } = null!;
        public bool Persist { get; set; }
        public bool ExitOnFail { get; set; }
        public string Status { get; set; } = null!;
        public byte StepNumber { get; set; }
        public DateTime RequestedTimestamp { get; set; }
        public DateTime? RunStartTimestamp { get; set; }
        public DateTime? RunEndTimestamp { get; set; }
    }
}
