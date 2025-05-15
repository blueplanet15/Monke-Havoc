using System.Collections.Generic;
using UnityEngine;

namespace MonkeHavoc.Modules.Multiplayer
{
    public class BoneESP
    {
        public static void Forever()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isLocal)
                {
                    rig.skeleton.enabled = true;
                    rig.skeleton.renderer.enabled = true;
                    rig.skeleton.renderer.material.shader = Shader.Find("GUI/Text Shader");
                    rig.skeleton.renderer.material.color = rig.playerColor;
                }
            }
        }

        public static void WhenIDisable()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                rig.skeleton.enabled = false;
                rig.skeleton.renderer.enabled = false;
                rig.skeleton.renderer.material.shader = Shader.Find("GorillaTag/UberShader");
            }
        }
    }
}