namespace FRMObjects.model
{
    public partial class ToolStepConfig : BaseModelObject
    {
        public long ToolDefinitionId { get; set; }
        public long ToolStepId { get; set; }
        public byte StepOrder { get; set; }
        public bool Active { get; set; }
    }
}
