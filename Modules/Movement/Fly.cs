using BepInEx.Configuration;
using MonkeHavoc.Panel;
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
                GorillaLocomotion.GTPlayer.Instance.transform.position += (GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * Time.deltaTime) * Plugin.flySpeed.Value;
                rb.velocity = Vector3.zero;
            }
        }
    }
}