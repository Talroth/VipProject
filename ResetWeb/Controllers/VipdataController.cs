
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResetWeb.Models;
using ResetWeb.DAO;

namespace ResetWeb.Controllers
{

    public class VipdataController : ApiController
    {
        private IVipDao db = FileDao.getInstance();

        public VipdataController()
        {
            
        }



        // Get all vips list
        public IEnumerable<Vip> Get()
        {
            return db.getVipList().Values;
        }

        // Get vip by his/her id number
        public Vip Get(int id)
        {
            return db.getVip(id);
        }

        // Edit vip
        public HttpResponseMessage Put([FromBody] Vip entry)
        {
            if (db.updateVip(entry))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }

        // Create new vip
        public Vip Post(Vip entry)
        {
            Vip createdVip = db.createVip(entry);

            return createdVip;
        }

        // Remove vip
        public HttpResponseMessage Delete([FromBody] int vipId)
        {
            if (db.removeVip(vipId))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }
    }
}
