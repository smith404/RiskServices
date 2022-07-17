namespace FRMObjects.model
{
    public partial class ToolStep : BaseModelObject
    {
        public ToolStep()
        {
            Id = 0;
            Description = "New Step";
        }

        public long Id { get; set; } = 0;
        public string Description { get; set; } = null!;
        public string StepType { get; set; } = null!;
        public string Definition { get; set; } = null!;
        public string? TestObject { get; set; }
        public bool FailOnExit { get; set; }
        public bool InMemory { get; set; }
        public bool HasInput { get; set; }
        public bool HasOutput { get; set; }
        public string DatasetName { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Format { get; set; } = null!;
        public bool Active { get; set; }
    }
}
