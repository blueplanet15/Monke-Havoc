using MonkeHavoc.Panel;

namespace MonkeHavoc.Modules.Settings
{
    public class MoreSpeedBoost
    {
        public static void MakeItMoreSpeedy()
        {
            if (Plugin.SpeedBoostSpeed.Value > 1.8f)
            {
                Plugin.SpeedBoostSpeed.Value = 1.1f;
            }
            else
            {
                Plugin.SpeedBoostSpeed.Value += 0.1f;
            }
            Plugin.config.Save();
        }
    }
}