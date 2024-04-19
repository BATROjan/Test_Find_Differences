using Levels;
using Picture;
using UI;
using UnityEngine;
using Zenject;

namespace GameController
{
    public class GameController : IGameController, ITickable
    {
        private readonly LevelController _levelController;
        private readonly TickableManager _tickableManager;
        private readonly MainCamera.MainCamera _camera;
        private readonly PictureController _pictureController;
        
        private UIRoot _uiRoot;
        
        private const int Distance = 1000;
        private RaycastHit _hit;
        private float _timer = 0;
        
        private bool _isLoose;
        private bool _isInterective;
        public GameController(
            LevelController levelController,
            TickableManager tickableManager,
            MainCamera.MainCamera camera,
            PictureController pictureController)
        {
            _levelController = levelController;
            _tickableManager = tickableManager;
            _camera = camera;
            _pictureController = pictureController;
            _tickableManager.Add(this);
            Start();
        }

        public void Start()
        {
            _isInterective = true;
            _levelController.UnloadPrefabAsync();
            CreatTimer(_levelController.GetTimerTime(1));
            _uiRoot = Object.FindObjectOfType<UIRoot>();
            
            _levelController.OnComplite += SuccesEnd;
            _uiRoot.WinWindow.UIButton.OnClick += Restart;
            _uiRoot.LooseWindow.UIButton.OnClick += Restart;
        }

        public void Restart()
        {
             _uiRoot.WinWindow.gameObject.SetActive(false);
             _uiRoot.LooseWindow.gameObject.SetActive(false);
             _uiRoot.UIPlayingSceneWindow.gameObject.SetActive(true);
             
             _levelController.ClearLevel();
             _levelController.CreatDifferences();
             CreatTimer(_levelController.GetTimerTime(1));
             _isInterective = true;
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        { 
            if (_isInterective)
            {
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    UpdateTimerDisplay();
                }
                else
                {
                    _uiRoot.UIPlayingSceneWindow.timerText.text = "00:00";
                    if (!_isLoose)
                    {
                        _isInterective = false;
                        _isLoose = true;
                        _uiRoot.LooseWindow.gameObject.SetActive(true);
                        _uiRoot.UIPlayingSceneWindow.gameObject.SetActive(false);
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    TouchLogic();
                }
            }
        }

        public void CreatTimer(int time)
        {
            _timer = time;
            _isLoose = false;
        }
        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(_timer / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);
            
            _uiRoot.UIPlayingSceneWindow.timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void SuccesEnd()
        {
            _isInterective = false;
            _uiRoot.WinWindow.gameObject.SetActive(true);
            _uiRoot.UIPlayingSceneWindow.gameObject.SetActive(false);
        }

        private void TouchLogic()
        {
            Ray ray = _camera.SceneCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction*1000);
            if (Physics.Raycast(ray, out hit,1000))
            {
                if (hit.transform)
                {
                    var difference = hit.collider.GetComponent<DifferenceView>();
                    if (difference)
                    {
                        _levelController.CheckDifferences(difference);
                    }
                }
            }
        }
    }
}