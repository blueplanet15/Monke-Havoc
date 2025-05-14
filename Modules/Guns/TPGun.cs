using MonkeHavoc.Libraries;
using MonkeHavoc.Patches;

namespace MonkeHavoc.Modules.Guns
{
    public class TPGun
    {
        private static Gun gun = new Gun();
        private static bool isPressed;

        public static void ForeverIWasDoingGUNS()
        {
            gun.UpdateGun();
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f && !isPressed)
                {
                    TeleportPatch.TeleportPlayer(gun.hit.point);
                }

                isPressed = ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
            }
        }

        public static void IHAVENABLEDMYFULLPOWER()
        {
            gun.OnEnable();
        }

        public static void DisabledAhhhhh()
        {
            gun.OnDisable();
        }
    }
}