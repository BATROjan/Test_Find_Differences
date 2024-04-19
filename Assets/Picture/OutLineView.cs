using UnityEngine;
using Zenject;

namespace Picture
{
    public class OutLineView : MonoBehaviour
    {
        [SerializeField] private Transform viewTransform;

        public void Reinit(Transform transform)
        {
            viewTransform.transform.position = transform.position;
        }
        public class Pool : MonoMemoryPool<Transform, OutLineView>
        {
            protected override void Reinitialize(Transform p1, OutLineView item)
            {
                base.Reinitialize(p1, item);
                item.Reinit(p1);
            }
        }
    }
}