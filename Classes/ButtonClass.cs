using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Classes
{
    internal class hhhh : GorillaPressableButton {}
    
    internal class ButtonClass : MonoBehaviour
    {
        public string mystringtorunitallllllllllllllll;
        private static float lastimetimetimetime;

        public void OnPress()
        {
            if (lastimetimetimetime > Time.time) return;
            GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(62, false, 0.5f);
            lastimetimetimetime = Time.time + 0.2f;
            GorillaTagger.Instance.StartVibration(false, 0.7f, 0.3f);
            Plugin.RunAwayWithMeWoHoooooooooooo(mystringtorunitallllllllllllllll);
        }
    }
}