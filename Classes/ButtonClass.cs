using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Classes
{
    internal class hhhh : GorillaPressableButton {}
    
    internal class ButtonClass : MonoBehaviour
    {
        public string mystringtorunitallllllllllllllll;
        private float lastimetimetimetime;

        private void OnTriggerEnter(Collider other)
        {
            if (lastimetimetimetime > Time.time) return;
            if (other.gameObject == Plugin.ball)
            {
                Plugin.RunAwayWithMeWoHoooooooooooo(mystringtorunitallllllllllllllll);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(62, false, 0.5f);
                lastimetimetimetime = Time.time + 0.5f;
            }
        }
    }
}