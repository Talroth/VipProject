using NanoApi.JsonFile;

namespace ResetWeb.Models
{
    public class Vip 
    {
        
        private int id;
        private string name;
        private int age;
        private string country;

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

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 0)
                    value = 0;
                age = value;
            }
        }

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [PrimaryKey]
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}