
using System.Collections.Generic;
using System.Linq;
using ResetWeb.Models;
using NanoApi;
using System.IO;
using System;

namespace ResetWeb.DAO
{
    // In order to keep the code simple i am using Json file repo and keep it localy
    public class FileDao : IVipDao
    {
        // json file stored in the temp folder just to keep it abstract 
        private static JsonFile<Vip> db = NanoApi.JsonFile<Vip>.GetInstance(Path.GetTempPath(), "vip.json");
        
        private static FileDao fileDao = new FileDao(); 

        private FileDao()
        {
            
        }

        public static FileDao getInstance()
        {
            return fileDao;

        }

        public Vip createVip(Vip vip)
        {

            db.Insert(new Vip() {  Name = vip.Name, Age = vip.Age, Country = vip.Country });

            // this is not good way but Jsonfile does not support  returning the auto id assigning, in "real" application with DB this should be implemented diffrently
            return db.Select(p => p.Name == vip.Name && p.Age == vip.Age && p.Country == vip.Country).First<Vip>();
            
        }

        public Vip getVip(int id)
        {
            List<Vip> selectedVip = db.Select(vip => vip.Id == id);
         
            return selectedVip.Count == 0 ? null : selectedVip.First<Vip>();
        }

        public Dictionary<int, Vip> getVipList()
        {
            List<Vip> listVip = db.Select();
            return listVip.ToDictionary(tt => tt.Id, tt => tt);
        }

        public bool removeVip(int id)
        {
            return (db.Delete(p => p.Id == id) > 0);
        }

        public bool updateVip(Vip vip)
        {
            return (db.Update(p => p.Id == vip.Id, p => { p.Name = vip.Name; p.Age = vip.Age; p.Country = vip.Country; }) > 0);
        }
    }
}