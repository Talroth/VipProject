﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.SessionState;
using ResetWeb.Models;

namespace ResetWeb.Controllers
{

    public class VipdataController : ApiController
    {
       
        public HttpSessionState session = HttpContext.Current.Session;

        public VipdataController()
        {
            //
            // Mimic DB
            //

            if (session != null && session["myData"] == null)
            {
                Dictionary<int, Vip> vList = new Dictionary<int, Vip>();
                vList.Add(0, new Vip(0, "Yeuda", 40, "IL"));
                vList.Add(1, new Vip(1, "Ninat", 35, "IL"));
                vList.Add(2, new Vip(2, "Tzion", 35, "IL"));
                session["myData"] = vList;
            }


        }



        // Get all vips list
        public IEnumerable<Vip> Get()
        {
            return ((Dictionary<int, Vip>)session["myData"]).Values;
        }

        // Get vip by his/her id number
        public Vip Get(int id)
        {
            return ((Dictionary<int, Vip>)session["myData"])[id]; 
        }

        // Edit exists vip
        public HttpResponseMessage Put([FromBody] Vip entry)
        {
            Console.WriteLine(entry.age);
            if (entry != null)
            {
                try
                {
                    Dictionary<int, Vip> vList = (Dictionary<int, Vip>)session["myData"];
                    if (vList[entry.id] != null)
                    {
                        vList[entry.id] = entry;
                        session["myData"] = vList;
                        return new HttpResponseMessage(HttpStatusCode.OK);
                    }
                }
                catch (KeyNotFoundException e)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }                            
            }

            return new HttpResponseMessage(HttpStatusCode.Forbidden);     

        }

        // Create new vip
        public HttpResponseMessage Post(Vip entry)
        {
            try
            {
                if (entry != null)
                {
                    Dictionary<int, Vip> vList = ((Dictionary<int, Vip>)session["myData"]);
                    entry.id = vList.Last().Value.id + 1;
                    vList.Add(entry.id, entry);
                    session["myData"] = vList;
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
                
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        // Remove exists vip
        public HttpResponseMessage Delete(Vip entry)
        {
            try
            {
                if (entry != null)
                {
                    Dictionary<int, Vip> vList = ((Dictionary<int, Vip>)session["myData"]);
                    vList.Remove(entry.id);
                    session["myData"] = vList;
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }



    }


}