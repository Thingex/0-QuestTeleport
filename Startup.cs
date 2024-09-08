using HarmonyLib;
using System.Reflection;
namespace QuestTeleport
{
    public class Startup : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            Harmony harmony = new Harmony(base.GetType().Name);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}