using ResetWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResetWeb.DAO
{
    interface IVipDao
    {
        Dictionary<int,Vip> getVipList();
        Vip getVip(int id);
        Vip createVip(Vip vip);
        bool removeVip(int id);
        bool updateVip(Vip vip);
    }
}
