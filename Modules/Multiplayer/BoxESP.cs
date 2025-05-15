using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonkeHavoc.Modules.Multiplayer
{
    public class BoxESP
    {
        private static List<GameObject> boxes = new List<GameObject>();
        private static List<VRRig> rigs = new List<VRRig>();
        
        public static void Forever()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isLocal)
                {
                    if (!rigs.Contains(rig))
                    {
                        rigs.Add(rig);
                    }
                }
            }

            foreach (VRRig rig in rigs)
            {
                if (!boxes.Any(box => box.name == rig.OwningNetPlayer.NickName))
                {
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = rig.transform.position;
                    box.transform.localScale = new Vector3(1f, 1f, 0.0001f);
                    box.name = rig.OwningNetPlayer.NickName;
                    box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    box.GetComponent<Renderer>().material.color = new Color(rig.playerColor.r, rig.playerColor.g, rig.playerColor.b,0.35f);
                    boxes.Add(box);
                }
            }
            
            foreach (GameObject box in boxes)
            {
                if (rigs.Any(rig => rig.OwningNetPlayer.NickName == box.name))
                {
                    box.transform.position = rigs.First(rig => rig.OwningNetPlayer.NickName == box.name).transform.position;
                    box.transform.LookAt(GorillaTagger.Instance.mainCamera.transform);
                }
            }
        }
        
        public static void WhenIDisable()
        {
            foreach (GameObject box in boxes)
            {
                GameObject.Destroy(box);
            }
            boxes.Clear();
            rigs.Clear();
        }
    }
}