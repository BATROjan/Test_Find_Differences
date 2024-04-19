using UnityEngine;
using Zenject;

namespace Picture
{
    public class PictureView : MonoBehaviour
    {
        public DifferenceView[]  DifferenceViews => differenceViews;
        [SerializeField] private DifferenceView[] differenceViews;
        
        public class Pool: MonoMemoryPool<PictureView>
        {
            
        }
    }
}