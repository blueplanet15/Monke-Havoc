using System.Collections;
using System.Collections.Generic;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class Frozone
    {
        private static List<GameObject> plats = new List<GameObject>();
        private static float lastTimeL = 0f;
        private static float lastTimeR = 0f;

        public static void CreatePlats(Transform HT)
        {
            GameObject FrozonePlat = GameObject.CreatePrimitive(PrimitiveType.Cube);
            FrozonePlat.transform.localScale = new Vector3(0.02f, 0.4f, 0.4f);

            FrozonePlat.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            FrozonePlat.GetComponent<Renderer>().material.color = Plugin.purp;

            FrozonePlat.transform.position = HT.position - new Vector3(0f, 0.06f, 0f);
            FrozonePlat.transform.rotation = HT.rotation;
            FrozonePlat.AddComponent<DeletePlats>();

            GameObject cooliCool = new GameObject("cool");

            cooliCool.transform.parent = FrozonePlat.transform;
            cooliCool.transform.localPosition = Vector3.zero;
            cooliCool.transform.localRotation = Quaternion.identity;
            cooliCool.transform.localScale = Vector3.one * 1.1f;

            cooliCool.AddComponent<BoxCollider>().isTrigger = true;
            cooliCool.AddComponent<CheckInteracted>();

            plats.Add(FrozonePlat);
        }

        public static void UpdateDaMod()
        {
            if (ControllerInputPoller.instance.rightGrab && Time.time > lastTimeR)
            {
                lastTimeR = Time.time + 0.03f;
                CreatePlats(GorillaTagger.Instance.rightHandTransform);
            }

            if (ControllerInputPoller.instance.leftGrab && Time.time > lastTimeL)
            {
                lastTimeL = Time.time + 0.03f;
                CreatePlats(GorillaTagger.Instance.leftHandTransform);
            }

            if (ControllerInputPoller.instance.leftGrab || ControllerInputPoller.instance.rightGrab)
            {
                GTPlayer_GetSlidePercentage.isPatched1 = true;
                GTPlayer_GetSlidePercentage.isPatched2 = false;
            }
            else
            {
                GTPlayer_GetSlidePercentage.isPatched1 = false;
                GTPlayer_GetSlidePercentage.isPatched2 = false;
            }
        }

        public static void DisableDaMod()
        {
            foreach (GameObject FrozonePlat in plats)
            {
                if (FrozonePlat != null)
                {
                    GameObject.Destroy(FrozonePlat);
                }
            }

            plats.Clear();
            GTPlayer_GetSlidePercentage.isPatched1 = false;
            GTPlayer_GetSlidePercentage.isPatched2 = false;
        }
    }


    public class DeletePlats : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }

    public class CheckInteracted : MonoBehaviour
    {
        private static int amountInteractions = 0;
        private int interactions;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber")
            {
                amountInteractions++;
                interactions = amountInteractions;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber")
            {
                GTPlayer_GetSlidePercentage.isPatched1 = true;
                GTPlayer_GetSlidePercentage.isPatched2 = false;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber" && interactions == amountInteractions)
            {
                GTPlayer_GetSlidePercentage.isPatched1 = false;
                GTPlayer_GetSlidePercentage.isPatched2 = false;
            }
        }
    }
}