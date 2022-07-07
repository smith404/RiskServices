using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRMObjects.model
{
    [Table("SearchableItem", Schema = "dbo")]
    public class SearchableItem
    {
        public SearchableItem()
        {
            this.Title = "";
            this.KeyWords = "";
            this.Summary = "";
            this.TargetType = "";
            this.Target = "";
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [Required]
        [JsonProperty(PropertyName = "key_words")]
        public string KeyWords { get; set; }

        [Required]
        [StringLength(90)]
        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [Required]
        [StringLength(90)]
        [JsonProperty(PropertyName = "target_type")]
        public string TargetType { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }
    }

    [Table("Notifications", Schema = "dbo")]
    public class Notification
    {
        public Notification()
        {
            this.Owner = "";
            this.Sender = "";
            this.Message = "";
            this.URL = "";
            this.Type = "";
            this.Processed = false;
            this.CreatedOn = DateTime.Now;
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "owner")]
        public string Owner { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "sender")]
        public string Sender { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [Required]
        [StringLength(255)]
        [JsonProperty(PropertyName = "url")]
        public string URL { get; set; }

        [Required]
        [StringLength(10)]
        [JsonProperty(PropertyName = "typw")]
        public string Type { get; set; }

        [Required]
        [JsonProperty(PropertyName = "processed")]
        public bool Processed { get; set; }

        [Required]
        [JsonProperty(PropertyName = "created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "processed_on")]
        public DateTime? ProcessedOn { get; set; }
    }
}
