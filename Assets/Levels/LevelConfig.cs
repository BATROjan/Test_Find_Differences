using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField] private LevelModel[] LevelModels;
        [NonSerialized] private bool _inited;

        private Dictionary<int, int> _timerDictionary = new Dictionary<int, int>();
        
        public int GetTimeByLevelNumber (int number)
        {
            if (!_inited)
            {
                Init();
            }

            return _timerDictionary[number];
        }
        
        private void Init()
        {
            foreach (var model in LevelModels)
            {
                _timerDictionary.Add(model.LevelNumdber, model.LevelTime);
            }

            _inited = true;
        }
    }
    
    [Serializable]
    public struct LevelModel
    {
        public int LevelNumdber;
        public int LevelTime;
    }
}