using UnityEngine;

namespace Picture
{
    public class OunLineController
    {
        private readonly OutLineView.Pool _pool;

        public OunLineController(OutLineView.Pool pool)
        {
            _pool = pool;
        }

        public void Spawn(Transform transform)
        {
            _pool.Spawn(transform);
        }
    }
}