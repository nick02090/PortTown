using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Resources
{
    public class Resource
    {
        public ResourceType Type { get; set; }
        public int Value { get; set; }

        public Resource(ResourceType Type, int Value)
        {
            this.Type = Type;
            this.Value = Value;
        }

        public Resource(ResourceType Type)
        {
            this.Type = Type;
            this.Value = 0;
        }
    }
}