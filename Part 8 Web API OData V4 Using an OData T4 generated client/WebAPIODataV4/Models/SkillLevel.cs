using System.ComponentModel.DataAnnotations;
using System.Web.OData.Builder;

namespace WebAPIODataV4.Models
{
    public class SkillLevel
    {
        [Key]
        public int Level { get; set; }

        public string Description { get; set; }

        [Contained]
        public virtual PlayerStats PlayerStats { get; set; }
    }
}