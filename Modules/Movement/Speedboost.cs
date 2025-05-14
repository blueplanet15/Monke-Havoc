using GorillaLocomotion;

namespace MonkeHavoc.Modules.Movement
{
    public class Speedboost
    {
        public static void LetsDoThisFAST()
        {
            GTPlayer.Instance.maxJumpSpeed = 6.5f * 1.3f;
            GTPlayer.Instance.jumpMultiplier = 1.1f * 1.3f;
        }
    }
}