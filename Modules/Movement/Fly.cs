using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class Fly
    {
        private static Rigidbody rb = GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody;
        
        public static void UpdateTheMod()
        {
            if (ControllerInputPoller.instance.leftControllerPrimaryButton)
            {
                GorillaLocomotion.GTPlayer.Instance.transform.position += (GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * UnityEngine.Time.deltaTime) * 15;
                rb.velocity = Vector3.zero;
            }
        }
    }
}