using GorillaLocomotion;
using MonkeHavoc.Panel;

namespace MonkeHavoc.Modules.Movement
{
    public class Speedboost
    {
        public static void LetsDoThisFAST()
        {
            GTPlayer.Instance.maxJumpSpeed = 6.5f * Plugin.SpeedBoostSpeed.Value;
            GTPlayer.Instance.jumpMultiplier = 1.1f * Plugin.SpeedBoostSpeed.Value;
        }
    }
}