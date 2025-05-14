namespace MonkeHavoc.Modules.Visual
{
    public class TimeChangersAndEverything
    {
        public static void Day()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }

        public static void Evening()
        {
            BetterDayNightManager.instance.SetTimeOfDay(7);
        }

        public static void Morning()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }

        public static void Night()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }

        public static void Rain()
        {
            for (int i = 0; i <= BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
            }
        }

        public static void NoRain()
        {
            for (int i = 0; i <= BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
            }
        }
    }
}