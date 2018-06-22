using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.OData.Builder;

namespace WebAPIODataV4.Models
{
    public class SkillLevels
    {
        [Key]
        public int Id { get; set; }

        [Contained]
        public List<SkillLevel> Levels { get; set; }
    }
}