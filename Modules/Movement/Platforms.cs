using GorillaLocomotion.Climbing;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class Platforms
    {
        private static GameObject lPlat;
        private static GameObject rPlat;
        private static GameObject lSticky;
        private static GameObject rSticky;
        private static bool isLeftGrabbing;
        private static bool isRightGrabbing;
        
        public static GameObject CreatePlat()
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject.transform.localScale = new Vector3(0.02f, 0.4f, 0.4f);
            gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            gameObject.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            gameObject.GetComponent<Renderer>().material.color = Plugin.purp;
            gameObject.SetActive(false);
            return gameObject;
        }

        public static GameObject CreateSticky(GameObject gameObject)
        {
            GameObject StickyObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            StickyObj.AddComponent<GorillaClimbable>();
            StickyObj.layer = LayerMask.NameToLayer("GorillaInteractable");
            Object.Destroy(StickyObj.GetComponent<Renderer>());
            StickyObj.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
            StickyObj.transform.SetParent(gameObject.transform, false);
            StickyObj.SetActive(Plugin.isSticky.Value);
            return StickyObj;
        }

        public static void CreatePlats()
        {
            if (lPlat == null)
            {
                lPlat = CreatePlat();
                lSticky = CreateSticky(lPlat);
            }

            if (rPlat == null)
            {
                rPlat = CreatePlat();
                rSticky = CreateSticky(rPlat);
            }
        }

        public static void DIEDIEDIE()
        {
            if (lPlat != null)
            {
                GameObject.Destroy(lPlat);
            }

            if (rPlat != null)
            {
                GameObject.Destroy(rPlat);
            }
        }

        public static void PlatformsModuleUpdate()
        {
            try
            {
                lSticky.SetActive(false);
                rSticky.SetActive(false);
                
                if (ControllerInputPoller.instance.leftGrab && !isLeftGrabbing)
                {
                    lPlat.transform.position = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.position;
                    lPlat.transform.rotation = GorillaLocomotion.GTPlayer.Instance.leftControllerTransform.rotation;
                    lPlat.SetActive(true);
                }
                else if (!ControllerInputPoller.instance.leftGrab)
                {
                    lPlat.SetActive(false);
                }

                if (ControllerInputPoller.instance.rightGrab && !isRightGrabbing)
                {
                    rPlat.transform.position = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.position;
                    rPlat.transform.rotation = GorillaLocomotion.GTPlayer.Instance.rightControllerTransform.rotation;
                    rPlat.SetActive(true);
                }
                else if (!ControllerInputPoller.instance.rightGrab)
                {
                    rPlat.SetActive(false);
                }

                isLeftGrabbing = ControllerInputPoller.instance.leftGrab;
                isRightGrabbing = ControllerInputPoller.instance.rightGrab;
            }
            catch
            {
                CreatePlats();
            }
        }
    }
}