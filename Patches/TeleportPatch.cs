using System;
using System.Threading.Tasks;
using UnityEngine;
using GorillaLocomotion;
using HarmonyLib;

namespace MonkeHavoc.Patches
{
    // All of this code is just copied from Grate because I asked Monky and he told me okay since he fixed it.
    [HarmonyPatch(typeof(GTPlayer))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    internal class TeleportPatch
    {
        private static bool _isTeleporting = false,
            _rotate = false;

        private static Vector3 _teleportPosition;
        private static float _teleportRotation;
        private static bool _killVelocity;
        private static LayerMask baseMask;

        public static bool Prefix(GTPlayer __instance)
        {
            try
            {
                if (_isTeleporting)
                {
                    baseMask = GTPlayer.Instance.locomotionEnabledLayers;
                    GTPlayer.Instance.locomotionEnabledLayers = 1 << 29;
                    GTPlayer.Instance.bodyCollider.isTrigger = true;
                    GTPlayer.Instance.headCollider.isTrigger = true;
                    var playerRigidBody = __instance.GetComponent<Rigidbody>();
                    if (playerRigidBody != null)
                    {
                        Vector3 correctedPosition = _teleportPosition - __instance.bodyCollider.transform.position +
                                                    __instance.transform.position;

                        if (_killVelocity)
                            playerRigidBody.velocity = Vector3.zero;

                        __instance.transform.position = correctedPosition;
                        if (_rotate)
                            __instance.transform.rotation = Quaternion.Euler(0, _teleportRotation, 0);


                        Traverse.Create(__instance).Field("lastLeftHandPosition")
                            .SetValue(__instance.leftHandFollower.transform.position);
                        Traverse.Create(__instance).Field("lastRightHandPosition")
                            .SetValue(__instance.rightHandFollower.transform.position);

                        Traverse.Create(__instance).Field("lastPosition").SetValue(correctedPosition);
                        Traverse.Create(__instance).Field("lastOpenHeadPosition")
                            .SetValue(__instance.headCollider.transform.position);

                        GorillaTagger.Instance.offlineVRRig.transform.position = correctedPosition;
                    }

                    GTPlayer.Instance.headCollider.isTrigger = false;
                    GTPlayer.Instance.bodyCollider.isTrigger = false;
                    Task.Delay(250).ContinueWith(delegate { GTPlayer.Instance.locomotionEnabledLayers = baseMask; });
                    _isTeleporting = false;
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return true;
        }

        internal static void TeleportPlayer(Vector3 destinationPosition, float destinationRotation,
            bool killVelocity = true)
        {
            if (_isTeleporting)
                return;
            _killVelocity = killVelocity;
            _teleportPosition = destinationPosition;
            _teleportRotation = destinationRotation;
            _isTeleporting = true;
            _rotate = true;
        }

        internal static void TeleportPlayer(Vector3 destinationPosition, bool killVelocity = true)
        {
            if (_isTeleporting)
                return;

            _killVelocity = killVelocity;
            _teleportPosition = destinationPosition;
            _isTeleporting = true;
            _rotate = false;
        }
    }
}