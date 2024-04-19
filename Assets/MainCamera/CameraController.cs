namespace MainCamera
{
    public class CameraController
    {
        private MainCamera _mainCameraView;

        private CameraController(MainCamera mainCameraView)
        {
            _mainCameraView = mainCameraView;
        }
        
        public MainCamera GetCameraView()
        {
            return _mainCameraView;
        }
    }
}