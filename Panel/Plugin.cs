using System;
using BepInEx;
using GorillaLocomotion;
using MonkeHavoc.Patches;
using UnityEngine;
using TMPro;

namespace MonkeHavoc.Panel
{
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		// Boring
		private static bool allowed = false;

		void Start()
		{
			HarmonyPatches.ApplyHarmonyPatches();
			GorillaTagger.OnPlayerSpawned(Init);
		}

		void Init()
		{
			NetworkSystem.Instance.OnJoinedRoomEvent += OnJoinedRoom;
			NetworkSystem.Instance.OnReturnedToSinglePlayer += OnLeftRoom;
		}

		void OnJoinedRoom()
		{
			if (NetworkSystem.Instance.GameModeString.Contains("MODDED"))
			{
				allowed = true;
				CreatePanel();
				CreateMyBawlerLikeHolyShitHeIsBAWLING();
			}
			else
			{
				allowed = false;
				AbuseMyPowerAndDestroyItAll();
			}
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
		}

		void OnLeftRoom()
		{
			allowed = false;
			AbuseMyPowerAndDestroyItAll();
		}
		
		// Very cool!!!!!!!!
		public static GameObject panel;
		public static GameObject ball;
		public static Color purp = new Color32(51, 0, 87, 1);
		public static Color otherpurp = new Color32(60, 26, 112, 1);

		private static void CreatePanel()
		{
			// Null Check
			if (panel != null)
			{
				Destroy(panel);
			}
			
			// Panel
			panel = GameObject.CreatePrimitive(PrimitiveType.Cube);
			panel.GetComponent<Renderer>().material.color = purp;
			panel.transform.localScale = new Vector3(0.02f, 0.3f, 0.4f);
			panel.transform.SetParent(GTPlayer.Instance.rightControllerTransform);
			panel.transform.localPosition = new Vector3(0.06f, 0f, 0f);
			panel.transform.localRotation = Quaternion.identity;
			Destroy(panel.GetComponent<BoxCollider>());
			panel.SetActive(false);
			
			// Title
			GameObject titlePhysicalObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			titlePhysicalObj.GetComponent<Renderer>().material.color = otherpurp;
			titlePhysicalObj.transform.SetParent(panel.transform);
			titlePhysicalObj.transform.localScale = new Vector3(1f, 1.25f, 0.2f);
			titlePhysicalObj.transform.localPosition = new Vector3(0f, 0f, 0.7f);
			titlePhysicalObj.transform.localRotation = Quaternion.identity;
			Destroy(titlePhysicalObj.GetComponent<BoxCollider>());
			
			GameObject titleTextObj = new GameObject("TitleText");
			titleTextObj.transform.SetParent(titlePhysicalObj.transform);
			titleTextObj.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
			titleTextObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);

			TextMeshPro textMeshPro = titleTextObj.AddComponent<TextMeshPro>();
			textMeshPro.text = "Monke Havoc";
			textMeshPro.fontSize = 0.45f;
			textMeshPro.alignment = TextAlignmentOptions.Center;
			textMeshPro.color = Color.white;
			
			// Category Shower
			GameObject cs = GameObject.CreatePrimitive(PrimitiveType.Cube);
			cs.GetComponent<Renderer>().material.color = otherpurp;
			cs.transform.SetParent(panel.transform);
			cs.transform.localScale = new Vector3(1f, 1f, 0.1f);
			cs.transform.localPosition = new Vector3(0f, 0.35f, 0f);
			cs.transform.localRotation = Quaternion.identity;
			Destroy(cs.GetComponent<BoxCollider>());
			
			GameObject categoryShowerTextObj = new GameObject("CategoryShowerText");
			categoryShowerTextObj.transform.SetParent(cs.transform);
			categoryShowerTextObj.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
			categoryShowerTextObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);
			
			TextMeshPro textMeshPro4 = categoryShowerTextObj.AddComponent<TextMeshPro>();
			textMeshPro4.text = "CATEGORY";
			textMeshPro4.fontSize = 0.45f;
			textMeshPro4.alignment = TextAlignmentOptions.Center;
			textMeshPro4.color = Color.white;
			
			// Page Left
			GameObject pl = GameObject.CreatePrimitive(PrimitiveType.Cube);
			pl.GetComponent<Renderer>().material.color = otherpurp;
			pl.transform.SetParent(panel.transform);
			pl.transform.localScale = new Vector3(1f, 0.1f, 1f);
			pl.transform.localPosition = new Vector3(0f, 0.65f, 0f);
			pl.transform.localRotation = Quaternion.identity;
			
			GameObject pageLeftTextObj = new GameObject("PageLeftText");
			pageLeftTextObj.transform.SetParent(pl.transform);
			pageLeftTextObj.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
			pageLeftTextObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);

			TextMeshPro textMeshPro1 = pageLeftTextObj.AddComponent<TextMeshPro>();
			textMeshPro1.text = "<";
			textMeshPro1.fontSize = 0.45f;
			textMeshPro1.alignment = TextAlignmentOptions.Center;
			textMeshPro1.color = Color.white;
			
			// Page Right
			GameObject pr = GameObject.CreatePrimitive(PrimitiveType.Cube);
			pr.GetComponent<Renderer>().material.color = otherpurp;
			pr.transform.SetParent(panel.transform);
			pr.transform.localScale = pl.transform.localScale;
			pr.transform.localPosition = new Vector3(pl.transform.localPosition.x, -pl.transform.localPosition.y, pl.transform.localPosition.z);
			pr.transform.localRotation = Quaternion.identity;
			pr.GetComponent<BoxCollider>().isTrigger = true;
			
			GameObject pageRightTextObj = new GameObject("PageRightText");
			pageRightTextObj.transform.SetParent(pr.transform);
			pageRightTextObj.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
			pageRightTextObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);

			TextMeshPro textMeshPro2 = pageRightTextObj.AddComponent<TextMeshPro>();
			textMeshPro2.text = ">";
			textMeshPro2.fontSize = 0.45f;
			textMeshPro2.alignment = TextAlignmentOptions.Center;
			textMeshPro2.color = Color.white;
			
			// Home Button
			GameObject hb = GameObject.CreatePrimitive(PrimitiveType.Cube);
			hb.GetComponent<Renderer>().material.color = otherpurp;
			hb.transform.SetParent(panel.transform);
			hb.transform.localScale = new Vector3(1f, 1f, 0.075f);
			hb.transform.localPosition = new Vector3(0f, 0f, -0.65f);
			hb.transform.localRotation = Quaternion.identity;
			hb.GetComponent<BoxCollider>().isTrigger = true;
			
			GameObject homeButtonTextObj = new GameObject("HomeText");
			homeButtonTextObj.transform.SetParent(hb.transform);
			homeButtonTextObj.transform.localRotation = Quaternion.Euler(180f, 90f, 90f);
			homeButtonTextObj.transform.localPosition = new Vector3(0f, 0.5f, 0f);
			
			TextMeshPro textMeshPro3 = homeButtonTextObj.AddComponent<TextMeshPro>();
			textMeshPro3.text = "Go Home";
			textMeshPro3.fontSize = 0.45f;
			textMeshPro3.alignment = TextAlignmentOptions.Center;
			textMeshPro3.color = Color.white;
		}

		private static void CreateMyBawlerLikeHolyShitHeIsBAWLING()
		{
			// Null Check
			if (ball != null)
			{
				Destroy(ball);
			}
			
			// Ball
			ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			ball.GetComponent<Renderer>().material.color = purp;
			ball.transform.SetParent(GTPlayer.Instance.leftControllerTransform);
			ball.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			ball.transform.localPosition = new Vector3(0f, -0.1f, 0f);
			ball.transform.localRotation = Quaternion.identity;
			ball.GetComponent<Rigidbody>().isKinematic = true;
			ball.SetActive(false);
		}

		private static void AbuseMyPowerAndDestroyItAll()
		{
			if (panel != null)
			{
				Destroy(panel);
				panel = null;
			}
			
			if (ball != null)
			{
				Destroy(ball);
				ball = null;
			}
		}
		
		void FixedUpdate()
		{
			if (allowed)
			{
				if (ControllerInputPoller.instance.rightControllerSecondaryButton)
				{
					panel.SetActive(true);
					ball.SetActive(true);
				}
				else
				{
					panel.SetActive(false);
					ball.SetActive(false);
				}
			}
		} // henlo
	}
}
