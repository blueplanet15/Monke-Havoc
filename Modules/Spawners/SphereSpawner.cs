using System.Collections.Generic;
using MonkeHavoc.Libraries;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Spawners
{
    public class SphereSpawner
    {
        private static Gun gun = new Gun();
        private static List<GameObject> spheres = new List<GameObject>();
        private static bool isPressed;

        public static void ThisWillRunFOREVER()
        {
            gun.UpdateGun();
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f && !isPressed)
                {
                    CreateSphere(gun.hit.point);
                }
                isPressed = ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
            }
        }

        public static void Enable()
        {
            gun.OnEnable();
        }

        public static void Disable()
        {
            gun.OnDisable();
        }

        public static void RemoveSpheres()
        {
            foreach (GameObject sphere in spheres)
            {
                GameObject.Destroy(sphere);
            }

            spheres.Clear();
        }

        private static void CreateSphere(Vector3 position)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.AddComponent<SphereCollider>();
            sphere.transform.position = position + Vector3.up * 0.5f;
            sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var renderer = sphere.GetComponent<Renderer>();
            renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            renderer.material.color = Plugin.purp;

            sphere.AddComponent<Rigidbody>();
            sphere.layer = 30;
            spheres.Add(sphere);

            GameObject dummySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject.Destroy(dummySphere.GetComponent<Renderer>());
            dummySphere.transform.SetParent(sphere.transform, false);
            dummySphere.transform.localPosition = Vector3.zero;
            dummySphere.transform.localScale = Vector3.one;
        }
    }
}