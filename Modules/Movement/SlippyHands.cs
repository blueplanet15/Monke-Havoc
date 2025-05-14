namespace MonkeHavoc.Modules.Movement
{
    public class SlippyHands
    {
        public static void OnEnable()
        {
            GTPlayer_GetSlidePercentage.isPatched1 = true;
            GTPlayer_GetSlidePercentage.isPatched2 = false;
        }

        public static void OnDisable()
        {
            GTPlayer_GetSlidePercentage.isPatched1 = false;
            GTPlayer_GetSlidePercentage.isPatched2 = false;
        }
    }
}