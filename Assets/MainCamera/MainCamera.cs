using UnityEngine;
using UnityEngine.Serialization;

namespace MainCamera
{
    public class MainCamera : MonoBehaviour
    {
        public Camera SceneCamera => mainCamera;
        
        [SerializeField] private Camera mainCamera;
    }
}