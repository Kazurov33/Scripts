using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_MVC.Models
{
    public class Scene //Сценарий скрипта
    {
        public int SceneId { get; set; }
        public string Name { get; set; }

        public int ScriptId { get; set; }
        public Script Script { get; set; }

    }
}
