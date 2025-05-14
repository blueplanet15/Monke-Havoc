namespace MonkeHavoc.Modules.Horror
{
    public class HorrorLightingHandler
    {
        public static void HorrorLighting()
        {
            GameLightingManager.instance.SetCustomDynamicLightingEnabled(true);
            var playerTempLight = GorillaTagger.Instance.headCollider.transform.Find("PlayerTempLight");
            playerTempLight.gameObject.SetActive(true);
        }
        
        public static void NormalLighting()
        {
            GameLightingManager.instance.SetCustomDynamicLightingEnabled(false);
            var playerTempLight = GorillaTagger.Instance.headCollider.transform.Find("PlayerTempLight");
            playerTempLight.gameObject.SetActive(false);
        }
    }
}