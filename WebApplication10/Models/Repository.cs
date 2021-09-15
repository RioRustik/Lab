using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication10.Models
{
    public class Repository
    {
        private ItemContext itemContext = new ItemContext();
        public IEnumerable<Item> Items
        {
            get { return itemContext.Items; }
        }
        

    }
}