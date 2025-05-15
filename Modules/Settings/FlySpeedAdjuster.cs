using MonkeHavoc.Panel;

namespace MonkeHavoc.Modules.Settings
{
    public class FlySpeedAdjuster
    {
        public static void MakeItMoreSpeedy()
        {
            if (Plugin.flySpeed.Value > 30)
            {
                Plugin.flySpeed.Value = 10;
            }
            else
            {
                Plugin.flySpeed.Value += 5;
            }
            Plugin.config.Save();
        }
    }
}