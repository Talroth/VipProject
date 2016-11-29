
using System.Collections.Generic;
using System.Linq;
using ResetWeb.Models;
using NanoApi;


namespace ResetWeb.DAO
{
    // In order to keep the code simple i am using Json file repo and keep it localy
    public class FileDao : IVipDao
    {
        private static JsonFile<Vip> db = NanoApi.JsonFile<Vip>.GetInstance(@"c:\temp", "vip.json");
        private static FileDao fileDao = new FileDao(); 

        private FileDao()
        {

        }

        public static FileDao getInstance()
        {
            return fileDao;

        }

        public Vip createNewVip(Vip vip)
        {

            db.Insert(new Vip() {  name = vip.name, Age = vip.Age, country = vip.country });

            // this is not good way but Jsonfile does not support  returning the auto id assigning, in "real" application with DB this should be implemented diffrently
            return db.Select(p => p.name == vip.name && p.Age == vip.Age && p.country == vip.country).First<Vip>();
            
        }

        public Vip getVip(int id)
        {
            List<Vip> selectedVip = db.Select(vip => vip.id == id);
         
            return selectedVip.Count == 0 ? null : selectedVip.First<Vip>();
        }

        public Dictionary<int, Vip> getVipList()
        {
            List<Vip> listVip = db.Select();
            return listVip.ToDictionary(tt => tt.id, tt => tt);
        }

        public void removeVip(int id)
        {
            db.Delete(p => p.id == id);
        }

        public void updtaeVip(Vip vip)
        {
            db.Update(p => p.id == vip.id, p => p.name = vip.name);
            db.Update(p => p.id == vip.id, p => p.Age = vip.Age);
            db.Update(p => p.name == vip.name, p => p.country = vip.country);
        }
    }
}