using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using WebAPIODataV4.Models;

namespace WebAPIODataV4.Controllers
{
    [ODataRoutePrefix("ContactType")]
    public class ContactTypeController : ODataController
    {
        readonly DomainModel _db = new DomainModel();

        [ODataRoute]
        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get()
        {
            return Ok(_db.ContactType.AsQueryable());
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20)]
        public IHttpActionResult Get([FromODataUri] int key)
        {
            var contactType = _db.ContactType.Find(key);
            if (contactType == null)
            {
                return NotFound();
            }
            return Ok(contactType);
        }

        [ODataRoute()]
        [HttpPost]
        [EnableQuery]
        public IHttpActionResult Post(ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.ContactType.AddOrUpdate(contactType);
            _db.SaveChanges();
            return Created(contactType);
        }

        [ODataRoute("({key})")]
        [HttpPut]
        [EnableQuery]
        public IHttpActionResult Put([FromODataUri] int key, ContactType contactType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != contactType.ContactTypeID)
            {
                return BadRequest();
            }

            _db.ContactType.AddOrUpdate(contactType);
            _db.SaveChanges();

            return Updated(contactType);
        }

        [ODataRoute]
        [HttpDelete]
        [EnableQuery]
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            var entityInDb = _db.ContactType.SingleOrDefault(t => t.ContactTypeID == key);
            if (entityInDb == null)
            {
                return NotFound();
            }
            _db.ContactType.Remove(entityInDb);
            _db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [ODataRoute]
        [HttpPatch]
        [EnableQuery]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<ContactType> delta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contactType = _db.ContactType.SingleOrDefault(t => t.ContactTypeID == key);
            if (contactType == null)
            {
                return NotFound();
            }

            delta.Patch(contactType);
            _db.SaveChanges();
            return Updated(contactType);
        }

        [ODataRoute("Default.ChangePersonStatus")]
        [HttpPost]
        [EnableQuery]
        public IHttpActionResult ChangePersonStatus([FromODataUri] int key, ODataActionParameters parameters)
        {
            if (ModelState.IsValid)
            {
                var level = parameters["Level"];
                // SAVE THIS TO THE DATABASE OR WHATEVER....
            }
            return Ok(true);
        }

        [ODataRoute("Default.ChangePersonStatus")]
        [HttpPost]
        [EnableQuery]
        public IHttpActionResult ChangePersonStatus(ODataActionParameters parameters)
        {
            if (ModelState.IsValid)
            {
                var level = parameters["Level"];
                // SAVE THIS TO THE DATABASE OR WHATEVER....
            }
            return Ok(true);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
