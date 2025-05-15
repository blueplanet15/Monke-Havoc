using System;
using System.Collections.Generic;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using GorillaLocomotion;
using MonkeHavoc.Classes;
using MonkeHavoc.Modules.Horror;
using MonkeHavoc.Modules.Spawners;
using MonkeHavoc.Patches;
using Photon.Pun;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using static MonkeHavoc.Panel.AllButtons;

namespace MonkeHavoc.Panel
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        // Boring
        private static bool allowed = false; //sd
        public static ConfigFile config;
        public static ConfigEntry<int> flySpeed;
        public static ConfigEntry<float> SpeedBoostSpeed;
        public static ConfigEntry<bool> isSticky;
        public static ConfigEntry<float> FrozoneSeconds;

        private void Awake()
        {
            config = new ConfigFile(Path.Combine(Paths.ConfigPath, "MonkeHavoc.cfg"), true);
            flySpeed = config.Bind("Movement", "FlySpeed", 15, "Fly speed");
            SpeedBoostSpeed = config.Bind("Movement", "SpeedBoostSpeed", 1.3f, "Speed boost");
            isSticky = config.Bind("Movement", "StickyPlats", false, "Make the platforms sticky");
            FrozoneSeconds = config.Bind("Movement", "FrozoneSeconds", 1f, "Frozone seconds");
        }

        void Start()
        {
            HarmonyPatches.ApplyHarmonyPatches();
            GorillaTagger.OnPlayerSpawned(Init);
            
            Hashtable properties = new Hashtable();
            properties.Add("MonkeHavocVersion", PluginInfo.Version);
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        }

        private void Init()
        {
            NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinedRoom;
            NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeftRoom;
        }

        private void OnJoinedRoom()
        {
            if (NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                allowed = true;
                GTPlayer.Instance.SetHoverAllowed(true);
                CreatePanel();
            }
            else
            {
                allowed = false;
                RemoveAll.ExecuteAllOfThem();
                AbuseMyPowerAndDestroyItAll();
                HorrorLightingHandler.NormalLighting();
                BetterDayNightManager.instance.currentSetting = TimeSettings.Normal;
                foreach (MonkeHavocModule[] category in categories)
                {
                    foreach (MonkeHavocModule button in category)
                    {
                        if (button.toggle)
                        {
                            if (button.on)
                            {
                                button.disable?.Invoke();
                                button.butObj.GetComponent<Renderer>().enabled = true;
                                button.on = false;
                                if (toInvoke.Contains(button))
                                {
                                    toInvoke.Remove(button);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OnLeftRoom()
        {
            allowed = false;
            GTPlayer.Instance.SetHoverAllowed(false);
            RemoveAll.ExecuteAllOfThem();
            AbuseMyPowerAndDestroyItAll();
            HorrorLightingHandler.NormalLighting();
            BetterDayNightManager.instance.currentSetting = TimeSettings.Normal;
            if (pointerBall != null)
            {
                Destroy(pointerBall);
            }

            if (lineRenderer != null)
            {
                Destroy(lineRenderer);
            }

            foreach (MonkeHavocModule[] category in categories)
            {
                foreach (MonkeHavocModule button in category)
                {
                    if (button.toggle)
                    {
                        if (button.on)
                        {
                            button.disable?.Invoke();
                            button.butObj.GetComponent<Renderer>().enabled = true;
                            button.on = false;
                            if (toInvoke.Contains(button))
                            {
                                toInvoke.Remove(button);
                            }
                        }
                    }
                }
            }
        }

        // Very cool!!!!!!!!
        public static GameObject panel;
        public static Color purp = new Color32(51, 0, 87, 1);
        public static Color otherpurp = new Color32(60, 26, 112, 1);
        public static int currentPage = 0;
        public static int currentCategory = 0;
        public static string currentCategoryName = "Home";
        private static int buttonsPerPageYey = 5;
        private static List<MonkeHavocModule> toInvoke = new List<MonkeHavocModule>();
        private static TextMeshPro toChangeOnUpdateButtons;

        private static void CreatePanel()
        {
            // Null Check
            if (panel != null)
            {
                Destroy(panel);
            }

            // Panel
            panel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            panel.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            panel.GetComponent<Renderer>().material.color = purp;
            panel.transform.localScale = new Vector3(0.02f, 0.3f, 0.4f);
            panel.transform.SetParent(GTPlayer.Instance.leftControllerTransform);
            panel.transform.localPosition = new Vector3(0.06f, 0f, 0f);
            panel.transform.localRotation = Quaternion.identity;
            Destroy(panel.GetComponent<BoxCollider>());
            panel.SetActive(false);

            // Title
            GameObject titlePhysicalObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            titlePhysicalObj.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            titlePhysicalObj.GetComponent<Renderer>().material.color = otherpurp;
            titlePhysicalObj.transform.SetParent(panel.transform);
            titlePhysicalObj.transform.localScale = new Vector3(1f, 1.25f, 0.2f);
            titlePhysicalObj.transform.localPosition = new Vector3(0f, 0f, 0.7f);
            titlePhysicalObj.transform.localRotation = Quaternion.identity;
            Destroy(titlePhysicalObj.GetComponent<BoxCollider>());
            GameObject titleTextObj = CreateTextLabel("MonkeHavoc", titlePhysicalObj.transform, out var tmp, 1.9f);

            // Category Shower
            GameObject cs = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cs.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            cs.GetComponent<Renderer>().material.color = (purp + otherpurp) * 0.5f;
            cs.transform.SetParent(panel.transform);
            cs.transform.localScale = new Vector3(1f, 0.9f, 0.15f);
            cs.transform.localPosition = new Vector3(0.6f, 0f, 0.375f);
            cs.transform.localRotation = Quaternion.identity;
            Destroy(cs.GetComponent<BoxCollider>());
            GameObject csTextObj =
                CreateTextLabel(currentCategoryName, cs.transform, out TextMeshPro textMeshPro, 1.5f);
            toChangeOnUpdateButtons = textMeshPro;

            // Page Left
            GameObject pl = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pl.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            pl.GetComponent<Renderer>().material.color = otherpurp;
            pl.transform.SetParent(panel.transform);
            pl.transform.localScale = new Vector3(1f, 0.15f, 0.9f);
            pl.transform.localPosition = new Vector3(0f, 0.65f, 0f);
            pl.transform.localRotation = Quaternion.identity;
            pl.GetComponent<BoxCollider>().isTrigger = true;
            pl.AddComponent<ButtonClass>().mystringtorunitallllllllllllllll = "PageLeft";
            GameObject pageLeftTextObj = CreateTextLabel("<", pl.transform, out var tmp2);

            // Page Right
            GameObject pr = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pr.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            pr.GetComponent<Renderer>().material.color = otherpurp;
            pr.transform.SetParent(panel.transform);
            pr.transform.localScale = pl.transform.localScale;
            pr.transform.localPosition = new Vector3(pl.transform.localPosition.x, -pl.transform.localPosition.y,
                pl.transform.localPosition.z);
            pr.transform.localRotation = Quaternion.identity;
            pr.GetComponent<BoxCollider>().isTrigger = true;
            pr.AddComponent<ButtonClass>().mystringtorunitallllllllllllllll = "PageRight";
            GameObject pageRightTextObj = CreateTextLabel(">", pr.transform, out var tmp3);

            // Home Button
            GameObject hb = GameObject.CreatePrimitive(PrimitiveType.Cube);
            hb.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            hb.GetComponent<Renderer>().material.color = otherpurp;
            hb.transform.SetParent(panel.transform);
            hb.transform.localScale = new Vector3(1f, 1f, 0.15f);
            hb.transform.localPosition = new Vector3(0f, 0f, -0.65f);
            hb.transform.localRotation = Quaternion.identity;
            hb.GetComponent<BoxCollider>().isTrigger = true;
            hb.AddComponent<ButtonClass>().mystringtorunitallllllllllllllll = "Home";
            GameObject homeTextObj = CreateTextLabel("Go Home", hb.transform, out var tmp4, 1.2f);

            // Buttons get automatically created for EFFICIENCY
            for (int categoryIndex = 0; categoryIndex < categories.Length; categoryIndex++)
            {
                MonkeHavocModule[] category = categories[categoryIndex];
                for (int buttonIndex = 0; buttonIndex < category.Length; buttonIndex++)
                {
                    float offset = 0.15f * (buttonIndex % buttonsPerPageYey);
                    CreateButton(offset, category[buttonIndex].textOnButton, buttonIndex, categoryIndex);
                }
            }

            UpdateButtons();
        }

        private static void CreateButton(float offset, string text, int buttonIndex, int categoryIndex)
        {
            GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);
            button.name = text;
            button.transform.SetParent(panel.transform);
            button.transform.localRotation = Quaternion.identity;
            button.transform.localScale = new Vector3(1f, 0.85f, 0.12f);
            button.transform.localPosition = new Vector3(0.4f, 0f, 0.2f - offset);
            button.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            button.GetComponent<Renderer>().material.color = otherpurp;
            button.GetComponent<BoxCollider>().isTrigger = true;
            button.AddComponent<ButtonClass>().mystringtorunitallllllllllllllll = text;
            GameObject buttonTextObj = CreateTextLabel(text, button.transform, out var tmp5, 1f);


            categories[categoryIndex][buttonIndex].butObj = button;
            categories[categoryIndex][buttonIndex].texObj = buttonTextObj;
        }

        static GameObject CreateTextLabel(string text, Transform parent, out TextMeshPro tmp, float size = 2f)
        {
            GameObject go = new GameObject("TMP_" + text);
            go.transform.SetParent(panel.transform);
            go.transform.localPosition = new Vector3(parent.localPosition.x + 0.501f, parent.localPosition.y,
                parent.localPosition.z);
            go.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
            go.transform.localScale = Vector3.one;
            Destroy(go.GetComponent<Collider>());

            tmp = go.AddComponent<TextMeshPro>();
            tmp.text = text;
            tmp.fontSize = size;
            tmp.color = Color.white;
            tmp.alignment = TextAlignmentOptions.Center;
            tmp.enableAutoSizing = false;

            return go;
        }

        private static void AbuseMyPowerAndDestroyItAll()
        {
            if (panel != null)
            {
                Destroy(panel);
            }
        }

        public static void RunAwayWithMeWoHoooooooooooo(string sigmagismga)
        {
            int totalButtons = categories[currentCategory].Length;
            int maxPages = Mathf.CeilToInt((float)totalButtons / buttonsPerPageYey);
            if (sigmagismga == "PageLeft")
            {
                currentPage--;
                if (currentPage < 0) currentPage = maxPages - 1;
                UpdateButtons();
            }
            else if (sigmagismga == "PageRight")
            {
                currentPage++;
                if (currentPage >= maxPages) currentPage = 0;
                UpdateButtons();
            }
            else if (sigmagismga == "Home")
            {
                currentPage = 0;
                currentCategory = 0;
                currentCategoryName = "Home";
                UpdateButtons();
            }
            else
            {
                foreach (MonkeHavocModule[] category in categories)
                {
                    foreach (MonkeHavocModule button in category)
                    {
                        if (button.textOnButton == sigmagismga)
                        {
                            if (button.toggle)
                            {
                                button.on = !button.on;
                                if (button.on)
                                {
                                    button.enable?.Invoke();
                                    button.butObj.GetComponent<Renderer>().enabled = false;
                                    if (button.foreverOrOnce != null)
                                    {
                                        toInvoke.Add(button);
                                    }
                                }
                                else
                                {
                                    button.disable?.Invoke();
                                    button.butObj.GetComponent<Renderer>().enabled = true;
                                    if (button.foreverOrOnce != null)
                                    {
                                        toInvoke.Remove(button);
                                    }
                                }
                            }
                            else
                            {
                                button.foreverOrOnce?.Invoke();
                            }
                        }
                        if (button.textOnButton == "Fly Speed")
                        {
                            button.texObj.GetComponent<TextMeshPro>().text = $"Fly Speed: {flySpeed.Value}";
                        }
                        if (button.textOnButton == "SpeedBoost")
                        {
                            button.texObj.GetComponent<TextMeshPro>().text = $"Speed: {SpeedBoostSpeed.Value}";
                        }
                        if (button.textOnButton == "FrozoneTime")
                        {
                            button.texObj.GetComponent<TextMeshPro>().text = $"Frozone Time: {FrozoneSeconds.Value}";
                        }
                    }
                }
            }
        }

        public static void UpdateButtons()
        {
            int currentCategoryIndex = 0;

            foreach (MonkeHavocModule[] category in categories)
            {
                int currentButtonIndex = 0;

                foreach (MonkeHavocModule button in category)
                {
                    if (currentCategoryIndex == currentCategory)
                    {
                        int startIndex = currentPage * buttonsPerPageYey;
                        int endIndex = startIndex + buttonsPerPageYey;

                        bool isVisible = currentButtonIndex >= startIndex && currentButtonIndex < endIndex;

                        button.butObj.SetActive(isVisible);
                        button.texObj.SetActive(isVisible);

                        currentButtonIndex++;
                    }
                    else
                    {
                        button.butObj.SetActive(false);
                        button.texObj.SetActive(false);
                    }
                }

                currentCategoryIndex++;
            }

            toChangeOnUpdateButtons.text = currentCategoryName;
        }

        private static void IGetCaughtIThink()
        {
            if (pointerBall != null)
            {
                Destroy(pointerBall);
            }

            pointerBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointerBall.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            pointerBall.GetComponent<Renderer>().material.color = Plugin.purp;
            pointerBall.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            Destroy(pointerBall.GetComponent<Collider>());
            pointerBall.SetActive(false);

            lineRenderer = pointerBall.AddComponent<LineRenderer>();
            lineRenderer.material.shader = Shader.Find("GorillaTag/UberShader");
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.positionCount = 2;
            lineRenderer.enabled = false;
        }

        private static GameObject pointerBall;
        private static LineRenderer lineRenderer;
        private static bool iPress;

        private static void UpdateRaycast()
        {
            try
            {
                if (XRSettings.isDeviceActive)
                {
                    if (Physics.Raycast(GTPlayer.Instance.rightControllerTransform.position,
                            -GTPlayer.Instance.rightControllerTransform.up, out RaycastHit hitt, Mathf.Infinity,
                            GTPlayer.Instance.locomotionEnabledLayers))
                    {
                        if (!pointerBall.activeSelf)
                        {
                            pointerBall.SetActive(true);
                        }

                        if (!lineRenderer.enabled)
                        {
                            lineRenderer.enabled = true;
                        }

                        pointerBall.transform.position = hitt.point;
                        lineRenderer.SetPositions(new Vector3[]
                            { GTPlayer.Instance.rightControllerTransform.position, pointerBall.transform.position });
                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f)
                        {
                            pointerBall.GetComponent<Renderer>().material.color = otherpurp;
                            lineRenderer.material.color = otherpurp;
                        }
                        else
                        {
                            pointerBall.GetComponent<Renderer>().material.color = purp;
                            lineRenderer.material.color = purp;
                        }

                        if (ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f && !iPress)
                        {
                            if (hitt.collider.gameObject.GetComponent<ButtonClass>() != null)
                            {
                                hitt.collider.gameObject.GetComponent<ButtonClass>().OnPress();
                            }
                        }

                        iPress = ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
                    }
                }
                else // This is for debugging and will be commented out in final release.
                {
                    Camera iWantTHISCamera = GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).gameObject
                        .activeInHierarchy
                        ? GorillaTagger.Instance.thirdPersonCamera.transform.GetChild(0).GetComponent<Camera>()
                        : GorillaTagger.Instance.mainCamera.GetComponent<Camera>();
                    if (Physics.Raycast(iWantTHISCamera.ScreenPointToRay(Mouse.current.position.value),
                            out RaycastHit hitt))
                    {
                        if (!pointerBall.activeSelf)
                        {
                            pointerBall.SetActive(true);
                        }

                        if (!lineRenderer.enabled)
                        {
                            lineRenderer.enabled = true;
                        }

                        pointerBall.transform.position = hitt.point;
                        lineRenderer.SetPositions(new Vector3[]
                            { GTPlayer.Instance.bodyCollider.transform.position, pointerBall.transform.position });
                        if (Mouse.current.leftButton.isPressed)
                        {
                            pointerBall.GetComponent<Renderer>().material.color = otherpurp;
                            lineRenderer.material.color = otherpurp;
                        }
                        else
                        {
                            pointerBall.GetComponent<Renderer>().material.color = purp;
                            lineRenderer.material.color = purp;
                        }

                        if (Mouse.current.leftButton.isPressed && !iPress)
                        {
                            if (hitt.collider.gameObject.GetComponent<ButtonClass>() != null)
                            {
                                hitt.collider.gameObject.GetComponent<ButtonClass>().OnPress();
                            }
                        }

                        iPress = Mouse.current.leftButton.isPressed;
                    }
                }
            }
            catch
            {
                IGetCaughtIThink();
            }
        }

        void FixedUpdate()
        {
            if (allowed)
            {
                try
                {
                    if (XRSettings.isDeviceActive)
                    {
                        if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                        {
                            panel.SetActive(true);
                            UpdateRaycast();
                            pointerBall.SetActive(true);
                        }
                        else
                        {
                            panel.SetActive(false);
                            pointerBall.SetActive(false);
                        }
                    }
                    else // Same with this, it is for debugging on PC and will be commented out for release.
                    {
                        panel.SetActive(true);
                        panel.transform.parent = null;
                        panel.transform.position = Vector3.one;
                        panel.transform.rotation = Quaternion.Euler(-90f, -90f, 0f);
                        UpdateRaycast();
                        pointerBall.SetActive(true);
                    }

                    if (toInvoke != null)
                    {
                        foreach (MonkeHavocModule button in toInvoke)
                        {
                            button.foreverOrOnce?.Invoke();
                        }
                    }
                }
                catch
                {
                    CreatePanel();
                }
            }
        }
    }
}