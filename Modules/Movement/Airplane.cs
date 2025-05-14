using System;
using UnityEngine;

namespace MonkeHavoc.Modules.Movement
{
    public class Airplane
    {
        private static GameObject lBox;
        private static GameObject rBox;
        public static bool canFlyR = false;
        public static bool canFlyL = false;

        public static void EnableBadable()
        {
            lBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
            rBox = GameObject.CreatePrimitive(PrimitiveType.Cube);

            GameObject.Destroy(lBox.GetComponent<Renderer>());
            GameObject.Destroy(rBox.GetComponent<Renderer>());

            lBox.transform.parent = GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform;
            lBox.transform.localPosition = new Vector3(-0.5f, 0.2f, 0f);
            lBox.transform.localScale = new Vector3(0.6f, 0.8f, 0.6f);
            lBox.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            rBox.transform.parent = GorillaLocomotion.GTPlayer.Instance.bodyCollider.transform;
            rBox.transform.localPosition = new Vector3(0.5f, 0.2f, 0f);
            rBox.transform.localScale = new Vector3(0.6f, 0.8f, 0.6f);
            rBox.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);

            lBox.AddComponent<AirplaneColL>();
            rBox.AddComponent<AirplaneColR>();
            lBox.GetComponent<BoxCollider>().isTrigger = true;
            rBox.GetComponent<BoxCollider>().isTrigger = true;

            rBox.layer = 18;
            lBox.layer = 18;
        }

        public static void DisableBadable()
        {
            try
            {
                GameObject.Destroy(lBox);
                GameObject.Destroy(rBox);
            }
            catch (NullReferenceException)
            {
                Debug.LogError("wqawawa no worky");
            }
        }

        public static void UpdateTheMod()
        {
            if (canFlyL && canFlyR)
            {
                var rb = GorillaLocomotion.GTPlayer.Instance.bodyCollider.attachedRigidbody;
                GorillaLocomotion.GTPlayer.Instance.transform.position +=
                    (GorillaLocomotion.GTPlayer.Instance.headCollider.transform.forward * UnityEngine.Time.deltaTime) *
                    15;
                rb.velocity = Vector3.zero;
            }
        }
    }


    public class AirplaneColL : MonoBehaviour
    {
        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber" &&
                other.gameObject.transform.parent.name == "LeftHand Controller")
            {
                Airplane.canFlyL = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber" &&
                other.gameObject.transform.parent.name == "LeftHand Controller")
            {
                Airplane.canFlyL = false;
            }
        }
    }

    public class AirplaneColR : MonoBehaviour
    {
        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber" &&
                other.gameObject.transform.parent.name == "RightHand Controller")
            {
                Airplane.canFlyR = true;
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.name == "GorillaHandClimber" &&
                other.gameObject.transform.parent.name == "RightHand Controller")
            {
                Airplane.canFlyR = false;
            }
        }
    }
}