using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Net_MVC.Models
{
    public class SampleData //Инициализатор
    {
        public static void Initialize(ScriptContext context)
        {
            if (!context.Scripts.Any())
            {
                context.Scripts.AddRange(
                    new Script
                    {
                        Name = "Приветствие",
                        Count = 2                     
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
