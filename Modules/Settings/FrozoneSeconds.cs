using MonkeHavoc.Panel;

namespace MonkeHavoc.Modules.Settings
{
    public class FrozoneSeconds
    {
        public static void MoreSeconds()
        {
            if (Plugin.FrozoneSeconds.Value > 6f)
            {
                Plugin.FrozoneSeconds.Value = 0.5f;
            }
            else
            {
                Plugin.FrozoneSeconds.Value += 0.1f;
            }
            Plugin.config.Save();
        }
    }
}