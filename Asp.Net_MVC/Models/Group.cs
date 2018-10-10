using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_MVC.Models
{
    public class Group // Группа текстов
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public int SceneId { get; set; }
        public Scene Scene { get; set; }

        public virtual List<Point> Points { get; set; }
    }
}
