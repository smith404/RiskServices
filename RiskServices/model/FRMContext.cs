﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiskServices.model
{
    public class FRMContext : DbContext
    {
        public DbSet<ToolDefinition> ToolDefinitions { get; set; }

        public DbSet<ToolStep> ToolSteps { get; set; }

        public DbSet<ToolExecutionLog> ToolExecutionLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionBuilder.GetSQLConnectionString(), builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
            base.OnConfiguring(optionsBuilder);            
        }
    }

    [Table("ToolDefinition", Schema = "dbo")]
    public class ToolDefinition
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "tool_name")]
        public string ToolName { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty(PropertyName = "version")]
        public byte Version { get; set; }

        [Required]
        [JsonProperty(PropertyName = "status")]
        public char Status { get; set; }
    }

    [Table("ToolExecutionLog", Schema = "dbo")]
    public class ToolExecutionLog
    {
        public ToolExecutionLog()
        {
            this.GUID = Guid.NewGuid().ToString();
            this.Persist = true;
            this.ExitOnFail = true;
            this.Status = 'N';
            this.StepNumber = 0;
            this.RequestedTimestamp = DateTime.Now;
            this.RunStartTimestamp = new DateTime(1970, 8, 22);
            this.RunEndTimestamp = new DateTime(1970, 8, 22);
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "tool_definition_id")]
        public long ToolDefinitionId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "run_configuration")]
        public string RunConfiguration { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        [JsonProperty(PropertyName = "requestor")]
        public string Requestor { get; set; }

        [Required]
        [StringLength(36)]
        [JsonProperty(PropertyName = "guid")]
        public string GUID { get; set; }

        [Required]
        [JsonProperty(PropertyName = "persist")]
        public bool Persist { get; set; }

        [Required]
        [JsonProperty(PropertyName = "exit_on_fail")]
        public bool ExitOnFail { get; set; }

        [Required]
        [JsonProperty(PropertyName = "status")]
        public char Status { get; set; }

        [Required]
        [JsonProperty(PropertyName = "step_number")]
        public byte StepNumber { get; set; }

        [Required]
        [JsonProperty(PropertyName = "requested_timestamp")]
        public DateTime RequestedTimestamp { get; set; }

        [JsonProperty(PropertyName = "run_start_timestamp")]
        public DateTime RunStartTimestamp { get; set; }

        [JsonProperty(PropertyName = "run_end_timestamp")]
        public DateTime RunEndTimestamp { get; set; }
    }

    [Table("ToolStep", Schema = "dbo")]
    public class ToolStep
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [Required]
        [StringLength(3)]
        [JsonProperty(PropertyName = "step_type")]
        public string StepType { get; set; }

        [Required]
        [JsonProperty(PropertyName = "definition")]
        public string Definition { get; set; }

        [Required]
        [JsonProperty(PropertyName = "in_memory")]
        public bool InMemory { get; set; }

        [Required]
        [JsonProperty(PropertyName = "has_input")]
        public bool HasInput { get; set; }

        [Required]
        [JsonProperty(PropertyName = "has_output")]
        public bool HasOutput { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "dataset_name")]
        public string DatasetName { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [Required]
        [StringLength(10)]
        [JsonProperty(PropertyName = "format")]
        public string Format { get; set; }

        [Required]
        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }
    }
}