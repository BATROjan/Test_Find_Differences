namespace Picture
{
    public class PictureController
    {
        private readonly PictureView.Pool _pool;

        public PictureController(PictureView.Pool pool)
        {
            _pool = pool;
        }

        public PictureView SpawnPicture()
        {
           var pictureView = _pool.Spawn();
           return pictureView;
        }
    }
}