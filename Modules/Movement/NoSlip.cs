namespace MonkeHavoc.Modules.Movement
{
    public class NoSlip
    {
        public static void OnEnable()
        {
            GTPlayer_GetSlidePercentage.isPatched1 = false;
            GTPlayer_GetSlidePercentage.isPatched2 = true;
        }

        public static void OnDisable()
        {
            GTPlayer_GetSlidePercentage.isPatched1 = false;
            GTPlayer_GetSlidePercentage.isPatched2 = false;
        }
    }
}