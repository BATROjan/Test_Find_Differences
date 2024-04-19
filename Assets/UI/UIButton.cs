using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClick;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}