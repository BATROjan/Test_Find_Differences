using System;
using UnityEngine;

namespace UI
{
    public abstract class UIWindow : MonoBehaviour
    {
        public Action OnShow;
        public Action OnHide;
        public abstract void Show();
        public abstract void Hide();
    }
}