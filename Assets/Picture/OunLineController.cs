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

        public OutLineView Spawn(Transform transform)
        {
           var line =  _pool.Spawn(transform);
           return line;
        }

        public void Despawn(OutLineView outLineView)
        {
            _pool.Despawn(outLineView);
        }
    }
}