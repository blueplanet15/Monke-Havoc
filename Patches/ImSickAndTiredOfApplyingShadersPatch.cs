using HarmonyLib;
using UnityEngine;

namespace MonkeHavoc.Patches
{
    [HarmonyPatch(typeof(GameObject))]
    [HarmonyPatch("CreatePrimitive", MethodType.Normal)]
    internal class ImSickAndTiredOfApplyingShadersPatch
    {
        private static void Postfix(GameObject __instance)
        {
            __instance.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
        }
    }
}