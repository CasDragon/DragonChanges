using System.Linq;
using Kingmaker.Blueprints;

namespace DragonChanges.Utils
{
    internal class LibraryStuff
    {
        public static void RemoveComponent(BlueprintScriptableObject blueprint, BlueprintComponent component)
        {
            blueprint.Components = blueprint.Components.Except([component]).ToArray();
        }
    }
}
