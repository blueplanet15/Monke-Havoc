using System.Collections.Generic;
using GorillaLocomotion;
using UnityEngine;

namespace MonkeHavoc.Modules.Multiplayer
{
    public class Tracers
    {
        private static List<LineRenderer> lines = new List<LineRenderer>();

        public static void ForeverTogether()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isLocal)
                {
                    LineRenderer iAmALINE = null;

                    if (rig.GetComponent<LineRenderer>() != null)
                    {
                        iAmALINE = rig.GetComponent<LineRenderer>();
                    }
                    else
                    {
                        iAmALINE = rig.AddComponent<LineRenderer>();
                        iAmALINE.startWidth = 0.025f;
                        iAmALINE.endWidth = 0.025f;
                        iAmALINE.material = new Material(Shader.Find("GUI/Text Shader"));
                        iAmALINE.positionCount = 2;
                        lines.Add(iAmALINE);
                    }

                    iAmALINE.material.color = rig.playerColor;
                    iAmALINE.SetPositions(new Vector3[2]
                        { GTPlayer.Instance.bodyCollider.transform.position, rig.transform.position });
                }
            }
        }
        
        public static void WhenIDisable()
        {
            foreach (LineRenderer line in lines)
            {
                GameObject.Destroy(line);
            }
            lines.Clear();
        }
    }
}