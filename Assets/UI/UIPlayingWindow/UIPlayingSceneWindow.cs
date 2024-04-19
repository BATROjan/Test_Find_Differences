using System;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIPlayingSceneWindow : UIWindow
    {
        public Action OnShow;
        public Action OnHide;
        
        public override void Show()
        {
            OnShow?.Invoke();
        }

        public override void Hide()
        {
            OnHide?.Invoke();
        }
    }
}