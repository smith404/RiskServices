﻿namespace FRMObjects.model
{
    public partial class SearchItem : BaseModelObject
    {
        public long Id { get; set; }
        public string Domain { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Keywords { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string TargetType { get; set; } = null!;
        public string Target { get; set; } = null!;
    }
}