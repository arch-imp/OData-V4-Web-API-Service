using System.ComponentModel.DataAnnotations;
using System.Web.OData.Builder;

namespace WebAPIODataV4.Models
{
    public class Player
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [Contained]
        public virtual PlayerStats PlayerStats { get; set; }
    }
}