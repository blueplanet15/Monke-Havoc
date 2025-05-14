using GorillaLocomotion;
using MonkeHavoc.Libraries;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Guns
{
    public class HoverboardGun
    {
        private static Gun gun = new Gun();
        private static float lastTime = 0f;

        public static void ForeverTogether()
        {
            gun.UpdateGun();
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                {
                    if (lastTime < Time.time && GTPlayer.Instance.isHoverAllowed)
                    {
                        lastTime = Time.time + 0.2f;
                        Quaternion rot = GTPlayer.Instance.rightControllerTransform.rotation;
                        Vector3 vel = Vector3.zero;
                        Vector3 avel = Vector3.zero;
                        Color color = Plugin.purp;

                        FreeHoverboardManager.instance.SendDropBoardRPC(gun.hit.point, rot, vel, avel, color);
                    }
                }
            }
        }
        
        public static void EnableWOOOOOO()
        {
            gun.OnEnable();
        }

        public static void DisableMeSad()
        {
            gun.OnDisable();
        }
    }
}