using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Buildings
{
    public class Palace : Building
    {
        public Palace(Guid Id, string Name, int Level, string Info, string ImagePath) : base(Id, Name, Level, Info, ImagePath)
        {
        }
    }
}