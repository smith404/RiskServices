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
}
