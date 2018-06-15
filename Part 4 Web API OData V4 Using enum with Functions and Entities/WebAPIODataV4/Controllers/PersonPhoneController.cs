using System.Linq;
using System.Web.Http;
using System.Web.OData;
using WebAPIODataV4.Models;

namespace WebAPIODataV4.Controllers
{
    public class PersonPhoneController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            return Ok(_db.PersonPhone.AsQueryable());
        }

        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.PersonPhone.Find(key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}