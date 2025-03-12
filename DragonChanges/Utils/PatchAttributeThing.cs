using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonChanges.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class DragonConfigure: Attribute
    {
    }

    internal class Thingy
    {
        public static void DoPatches()
        {
            var methods = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.IsStatic && m.GetCustomAttribute<DragonConfigure>() is not null);
            foreach (var method in methods)
                method.Invoke(null, []);
        }
    }
}
