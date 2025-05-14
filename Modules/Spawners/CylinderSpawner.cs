using System.Collections.Generic;
using MonkeHavoc.Libraries;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Spawners
{
    public class CylinderSpawner
    {
        private static Gun gun = new Gun();
        private static List<GameObject> cylinders = new List<GameObject>();
        private static bool isPressed;

        public static void ThisWillRunFOREVER()
        {
            gun.UpdateGun();
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f && !isPressed)
                {
                    CreateCylinder(gun.hit.point);
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
            RemoveCylinders();
        }

        public static void RemoveCylinders()
        {
            foreach (GameObject cylinder in cylinders)
            {
                GameObject.Destroy(cylinder);
            }

            cylinders.Clear();
        }

        private static void CreateCylinder(Vector3 position)
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            if (cylinder.GetComponent<BoxCollider>() == null) cylinder.AddComponent<BoxCollider>();
            cylinder.transform.position = position + Vector3.up * 0.5f;
            cylinder.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var renderer = cylinder.GetComponent<Renderer>();
            renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            renderer.material.color = Plugin.purp;

            cylinder.AddComponent<Rigidbody>();
            cylinder.layer = 30;
            cylinders.Add(cylinder);

            GameObject dummyCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            GameObject.Destroy(dummyCylinder.GetComponent<Renderer>());
            dummyCylinder.transform.SetParent(cylinder.transform, false);
            dummyCylinder.transform.localPosition = Vector3.zero;
            dummyCylinder.transform.localScale = Vector3.one;
        }
    }
}