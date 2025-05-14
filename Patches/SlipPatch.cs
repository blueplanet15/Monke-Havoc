using HarmonyLib;
using GorillaLocomotion;

namespace MonkeHavoc
{
    [HarmonyPatch(typeof(GTPlayer), "GetSlidePercentage")]
    class GTPlayer_GetSlidePercentage
    {
        public static bool isPatched1 = false;
        public static bool isPatched2 = false;
        static bool Prefix(ref float __result)
        {
            if (isPatched1 && !isPatched2)
            {
                __result = 1f;
                return false;
            }
            if (!isPatched1 && isPatched2)
            {
                __result = 0f;
                return false;
            }
            return true;
        }
    }
}