
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
       

        public VipdataController()
        {

        }



        // Get all vips list
        public IEnumerable<Vip> Get()
        {
            IVipDao db = FileDao.getInstance();

            return db.getVipList().Values;

        }

        // Get vip by his/her id number
        public Vip Get(int id)
        {
            IVipDao db = FileDao.getInstance();

            return db.getVip(id);
        }

        // Edit vip
        public HttpResponseMessage Put([FromBody] Vip entry)
        {
            IVipDao db = FileDao.getInstance();

            db.updtaeVip(entry);

            return new HttpResponseMessage(HttpStatusCode.OK);  
        }

        // Create new vip
        public Vip Post(Vip entry)
        {
            IVipDao db = FileDao.getInstance();

            return db.createNewVip(entry);



            //        return new HttpResponseMessage(HttpStatusCode.BadRequest);


            //    return new HttpResponseMessage(HttpStatusCode.NotFound);

        }

        // Remove vip
        public HttpResponseMessage Delete([FromBody] int vipId)
        {
            IVipDao db = FileDao.getInstance();
            
            db.removeVip(vipId);

            return new HttpResponseMessage(HttpStatusCode.OK);

        }
    }
}
