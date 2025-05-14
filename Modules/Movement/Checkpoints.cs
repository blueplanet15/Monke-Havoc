using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class Checkpoints
    {
        private static GameObject Checkpoint;
        private static bool isPressed;
        
        public static void OnActivateOrOnForever()
        {
            if (ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Checkpoint.SetActive(true);
                Checkpoint.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                Checkpoint.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
            }

            if (ControllerInputPoller.instance.leftControllerPrimaryButton && !isPressed &&
                !ControllerInputPoller.instance.rightControllerPrimaryButton)
            {
                Patches.TeleportPatch.TeleportPlayer(Checkpoint.transform.position);
            }
            isPressed = ControllerInputPoller.instance.leftControllerPrimaryButton;
        }
        
        public static void OnEnable()
        {
            Checkpoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Checkpoint.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            GameObject.Destroy(Checkpoint.GetComponent<BoxCollider>());
            Checkpoint.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            Checkpoint.GetComponent<Renderer>().material.color = Plugin.purp;
            Checkpoint.SetActive(false);
        }
        
        public static void OnDisable()
        {
            GameObject.Destroy(Checkpoint);
        }
    }
}