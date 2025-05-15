using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class NoClip
    {
        private static bool isEnabled = false;
        
        public static void ForeverLostOnIslandsLivingThatBoatRand()
        {
            if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
            {
                if (!isEnabled)
                {
                    isEnabled = true;
                    foreach (MeshCollider col in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        col.enabled = false;
                    }
                }
            }
            else
            {
                if (isEnabled)
                {
                    isEnabled = false;
                    foreach (MeshCollider col in Resources.FindObjectsOfTypeAll<MeshCollider>())
                    {
                        col.enabled = true;
                    }
                }
            }
        }

        public static void Disable()
        {
            isEnabled = false;
            foreach (MeshCollider col in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                col.enabled = true;
            }
        }
    }
}