using Plugin = MonkeHavoc.Panel.Plugin;

namespace MonkeHavoc.Modules
{
    public class AllTheCoolThings
    {
        public static void Settings()
        {
            Plugin.currentCategory = 1;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Settings";
            Plugin.UpdateButtons();
        }

        public static void Movement()
        {
            Plugin.currentCategory = 2;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Movement";
            Plugin.UpdateButtons();
        }

        public static void Guns()
        {
            Plugin.currentCategory = 3;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Guns";
            Plugin.UpdateButtons();
        }

        public static void Multiplayer()
        {
            Plugin.currentCategory = 4;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Multiplayer";
            Plugin.UpdateButtons();
        }

        public static void Visual()
        {
            Plugin.currentCategory = 5;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Visual";
            Plugin.UpdateButtons();
        }

        public static void Spawners()
        {
            Plugin.currentCategory = 6;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Spawners";
            Plugin.UpdateButtons();
        }

        public static void Horror()
        {
            Plugin.currentCategory = 7;
            Plugin.currentPage = 0;
            Plugin.currentCategoryName = "Horror";
            Plugin.UpdateButtons();
        }
    }
}