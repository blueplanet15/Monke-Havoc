using System;
using UnityEngine;

namespace MonkeHavoc.Classes
{
    public class MonkeHavocModule
    {
        public Action enable;
        public Action disable;
        public Action foreverOrOnce;

        public string textOnButton;
        public bool toggle = true;
        public bool on = false;

        public GameObject butObj; // Don't assign.
        public GameObject texObj; // Don't assign.
    }
}