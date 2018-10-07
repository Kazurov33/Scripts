using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.Net_MVC.Models
{
    // Класс для создания выпадающего списка с выбором сценария внутри скрипта.
    // Функция пока в разработке

    public class SceneFilter 
    {
        public IEnumerable<Group> Groups { get; set; }
        public SelectList Scenes{ get; set; }
    }
}
