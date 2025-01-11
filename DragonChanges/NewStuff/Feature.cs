using DragonChanges.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.NewStuff
{
    internal class Feature
    {
        public static void Configure()
        {
            // feats
            PowerfulThrow.Configure();
            PowerfulWings.Configure();
            // mounts
            GriffonMount.Configure();
            UndeadMount.Configure();
            UnicornMount.Configure();
        }
    }
}
