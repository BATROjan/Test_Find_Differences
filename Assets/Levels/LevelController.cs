using System.Collections.Generic;
using Picture;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Levels
{
    public class LevelController
    {
        private readonly OunLineController _outLineController;
        private List<int> _differenceNumder = new List<int>();
        private int _countOfDifferences = 3;
        public string prefabKey = "Level_1";
        private AsyncOperationHandle<GameObject> loadOperation;
        private LevelView _levelView;
        
        public LevelController(OunLineController outLineController)
        {
            _outLineController = outLineController;
        }

        public void CreatDifferences()
        {
            GetRandomNumber();
            for (int i = 0; i < _differenceNumder.Count; i++)
            {
                var picture = Random.Range(0, 2);
                _levelView.PictureViews[picture].DifferenceViews[_differenceNumder[i]].SpriteRenderer.sprite = null;
                for (int j = 0; j < 2; j++)
                {
                    _outLineController.Spawn(_levelView.PictureViews[j].DifferenceViews[_differenceNumder[i]].transform);
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