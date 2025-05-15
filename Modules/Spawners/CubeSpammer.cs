using System.Collections.Generic;
using MonkeHavoc.Libraries;
using MonkeHavoc.Panel;
using UnityEngine;

namespace MonkeHavoc.Modules.Spawners
{
    public class CubeSpammer
    {
        private static Gun gun = new Gun();
        private static List<GameObject> cubes = new List<GameObject>();
        private static float lastTime = 0f;

        public static void ThisWillRunFOREVER()
        {
            gun.UpdateGun();
            if (ControllerInputPoller.instance.rightGrab)
            {
                if (Time.time > lastTime && ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                {
                    lastTime = Time.time + 0.05f;
                    CreateCube(gun.hit.point);
                }
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

        public static void RemoveCubes()
        {
            foreach (GameObject cube in cubes)
            {
                GameObject.Destroy(cube);
            }
            cubes.Clear();
        }
        
        private static void CreateCube(Vector3 position)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (cube.GetComponent<BoxCollider>() == null) cube.AddComponent<BoxCollider>();
            cube.transform.position = position + Vector3.up * 0.5f;
            cube.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            var renderer = cube.GetComponent<Renderer>();
            renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            renderer.material.color = Plugin.purp;

            cube.AddComponent<Rigidbody>();
            cube.layer = 30;
            cubes.Add(cube);

            GameObject dummyCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            GameObject.Destroy(dummyCube.GetComponent<Renderer>());
            dummyCube.transform.SetParent(cube.transform, false);
            dummyCube.transform.localPosition = Vector3.zero;
            dummyCube.transform.localScale = Vector3.one;
        }
    }
}