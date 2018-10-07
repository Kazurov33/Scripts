using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_MVC.Models
{
    public class Point // Текст инструкции с названием и описательной частью
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Context { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
