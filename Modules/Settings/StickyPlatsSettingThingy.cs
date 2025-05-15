using MonkeHavoc.Panel;

namespace MonkeHavoc.Modules.Settings
{
    public class StickyPlatsSettingThingy
    {
        public static void OnDisableAndOnEnableBecauseHanSoloSaidIsGood()
        {
            Plugin.isSticky.Value = !Plugin.isSticky.Value;
            Plugin.config.Save();
        }
    }
}