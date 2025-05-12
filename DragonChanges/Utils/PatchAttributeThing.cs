using System;
using System.Linq;
using System.Reflection;

namespace DragonChanges.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class DragonConfigure : Attribute
    {
        public static int priority = 0;
        public DragonConfigure(int patchPriority = 0) 
        {
            priority = patchPriority;
        }
        public int PatchPriority
        {
            get { return priority; }
            set { priority = value; }
        }
    }

    internal class Thingy
    {
        public static void DoPatches()
        {
            var methods = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.IsStatic && m.GetCustomAttribute<DragonConfigure>() is not null && m.GetCustomAttribute<DragonConfigure>().PatchPriority == 0);
            foreach (var method in methods)
                method.Invoke(null, []);
            var methods1 = Assembly.GetExecutingAssembly().GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                .Where(m => m.IsStatic && m.GetCustomAttribute<DragonConfigure>() is not null && m.GetCustomAttribute<DragonConfigure>().PatchPriority == 1);
            foreach (var method in methods1)
                method.Invoke(null, []);
        }
    }
}
