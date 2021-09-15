using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class Cart
    {
        private List<Detail> items = new List<Detail>();
        public void Add(Detail d)
        {
            Detail detail = items.Where(i => i.Id == d.Id).FirstOrDefault();
            if (detail == null)
            {
                items.Add(d);
            }
            else
            {
                detail.Quantity += Convert.ToInt32(d.Quantity);
            }
        }
        public void Remove(int id)
        {
            Detail remDet = items.Find(item => Convert.ToInt32(item.Id) == id);
            items.Remove(remDet);
        }
        public List<Detail> Details
        {
            get { return items; }
        }
    }
}