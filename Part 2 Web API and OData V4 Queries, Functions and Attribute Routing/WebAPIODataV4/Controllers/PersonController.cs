using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using WebAPIODataV4.Models;

namespace WebAPIODataV4.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [ODataRoute]
        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            return Ok(_db.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            return Ok(_db.Person.SingleOrDefault(t => t.BusinessEntityID == key));
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}