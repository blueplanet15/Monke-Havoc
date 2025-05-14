using System.Diagnostics;
using UnityEngine;

namespace MonkeHavoc.Modules.Horror
{
    public class ScaryBack
    {
        public static void DoTheThing()
        {
            if (PlayerPrefs.HasKey("seenGhostReactor"))
            {
                PlayerPrefs.DeleteKey("seenGhostReactor");
            }
            Application.Quit();
        }
    }
}