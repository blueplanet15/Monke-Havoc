using UnityEngine;

namespace MonkeHavoc.Modules.Multiplayer
{
    public class BoneESP
    {
        public static void OnActivateOrOnForever()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isLocal)
                {
                    rig.skeleton.enabled = true;
                    rig.skeleton.renderer.material.shader = Shader.Find("GUI/Text Shader");
                    rig.skeleton.renderer.material.color = rig.playerColor;
                }
            }
        }

        public static void OnDisable()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (!rig.isLocal)
                {
                    rig.skeleton.enabled = false;
                }
            }
        }
    }
}