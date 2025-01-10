using DragonChanges.New_Classes.Redditor;
using DragonChanges.New_Classes.Swordmaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.New_Classes
{
    internal class AllClasses
    {
        public static void Configure()
        {
            RedditorClass.Configure();
            SwordmasterClass.Configure();
        }
    }
}
