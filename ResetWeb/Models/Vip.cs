using NanoApi.JsonFile;


namespace ResetWeb.Models
{
    public class Vip 
    {
        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set;}
        public int age { get; set; }
        public string country { get; set; }
     
        public Vip()
        {

        }

        public Vip(int id, string name, int age, string country)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.country = country;
        }

    }
}