using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public class Palace : Building
    {
        public Palace(string Name, int Level, string Info, string ImagePath) : base(Name, Level, Info, ImagePath)
        {
        }
    }
}