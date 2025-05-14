using GorillaLocomotion;

namespace MonkeHavoc.Modules
{
    public class testmod
    {
        public static void forever()
        {
            GTPlayer.Instance.maxJumpSpeed = 1.1f * 1.5f;
            GTPlayer.Instance.jumpMultiplier = 6.5f * 1.5f;
        }
    }
}