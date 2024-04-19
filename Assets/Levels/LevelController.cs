using System;
using System.Collections.Generic;
using Picture;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Levels
{
    public class LevelController
    {
        public Action OnComplite;

        private readonly LevelConfig _levelConfig;
        private readonly OunLineController _outLineController;
        
        private List<int> _differenceNumder = new List<int>();
        private List<OutLineView> _outLineViews = new List<OutLineView>();
        
        private Dictionary<int, List<OutLineView>> _OuLinedictionary =
            new Dictionary<int, List<OutLineView>>();
        private Dictionary<int, Sprite> _SpriteDictionary = 
            new Dictionary<int, Sprite>();

        private int _countOfDifferences = 3;
        public string prefabKey = "Level_1";
        private AsyncOperationHandle<GameObject> loadOperation;
        private LevelView _levelView;
        
        public LevelController(
            LevelConfig levelConfig,
            OunLineController outLineController)
        {
            _levelConfig = levelConfig;
            _outLineController = outLineController;
        }

        public void ClearLevel()
        {
            foreach (var value in _SpriteDictionary)
            {
                for (int i = 0; i < 2; i++)
                {
                    _levelView.PictureViews[i].DifferenceViews[value.Key-1].SpriteRenderer.sprite = value.Value;
                }
            }

            foreach (var line in _outLineViews)
            {
                _outLineController.Despawn(line);
            }

            _differenceNumder.Clear();
            _OuLinedictionary.Clear();
            _SpriteDictionary.Clear();
            _outLineViews.Clear();
        }

        public void CreatDifferences()
        {
            GetRandomNumber();
            for (int i = 0; i < _differenceNumder.Count; i++)
            {
                var picture = Random.Range(0, 2);
                var difference = _levelView.PictureViews[picture].DifferenceViews[_differenceNumder[i]];
                
                _SpriteDictionary.Add(difference.Number, difference.SpriteRenderer.sprite);
                difference.SpriteRenderer.sprite = null;
                List<OutLineView> list = new List<OutLineView>();
                
                for (int j = 0; j < 2; j++)
                {
                    var line = _outLineController.Spawn(_levelView.PictureViews[j].DifferenceViews[_differenceNumder[i]].transform);          
                    line.gameObject.SetActive(false);
                    
                    list.Add(line);
                    _outLineViews.Add(line);
                }
                _OuLinedictionary.Add(difference.Number, list);
            }
        }

        public int GetTimerTime(int levelNumder)
        {
            return _levelConfig.GetTimeByLevelNumber(levelNumder);
        }
        
        public void CheckDifferences(DifferenceView differenceView)
        {
            if (_OuLinedictionary.ContainsKey(differenceView.Number))
            {
                var lines = _OuLinedictionary[differenceView.Number];
                foreach (var line in lines)
                {
                    line.gameObject.SetActive(true);
                    _OuLinedictionary.Remove(differenceView.Number);
                    if (_OuLinedictionary.Count == 0)
                    {
                        OnComplite?.Invoke();
                    }
                }
            }
        }

        public async void UnloadPrefabAsync()
        {
            loadOperation = Addressables.LoadAssetAsync<GameObject>(prefabKey);
            await loadOperation.Task;
        
            if (loadOperation.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefabInstance = loadOperation.Result;
            
                Addressables.ReleaseInstance(prefabInstance);
                var level = GameObject.Instantiate(prefabInstance);
                _levelView = level.GetComponent<LevelView>();
                CreatDifferences();
                Debug.Log("Префаб выгружен успешно");
            }
            else
            {
                Debug.LogError("Ошибка загрузки префаба: " + loadOperation.Status);
            }
        }

        private void GetRandomNumber()
        {
            while (_differenceNumder.Count < _countOfDifferences)
            {
                var numder = Random.Range(0, _levelView.PictureViews[0].DifferenceViews.Length);
                if (!_differenceNumder.Contains(numder))
                {
                    _differenceNumder.Add(numder);
                }
            }
        }

        private void OnDestroy()
        {
            if (loadOperation.IsValid())
            {
                Addressables.Release(loadOperation);
            }
        }
    }
}