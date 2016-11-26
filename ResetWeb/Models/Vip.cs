using System;
using System.Collections.Generic;
// TODO: get rid of dependency?
using System.Linq;
using System.Web;

namespace ResetWeb.Models
{
    public class Vip : IEquatable<Vip>
    {
      // TODO: it's more conventional to make private fields (lowercase)
      // and use capital case for the getter/setter (e.g. Id, Name...)
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

        // TODO: do you still need it?
        public bool Equals(Vip other)
        {
            if (other == null) return false;
            return (this.id.Equals(other.id));
        }
    }
}
