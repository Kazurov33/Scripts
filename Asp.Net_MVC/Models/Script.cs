using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_MVC.Models
{
    public class Script //Cкрипт
    {
        public int ScriptId { get; set; } 
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public int Count { get; set; }
    }

}
