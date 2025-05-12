using BepInEx;
using MonkeHavoc.Patches;
using UnityEngine;

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
			}
			else
			{
				allowed = false;
			}
		}

		void OnLeftRoom()
		{
			allowed = false;
		}
		
		// Very cool!!!!!!!!
		public static GameObject panel;
		public static GameObject ball;

		private static void CreatePanel()
		{
			
		}
		
		void FixedUpdate()
		{
			if (allowed)
			{
				
			}
		}
	}
}
