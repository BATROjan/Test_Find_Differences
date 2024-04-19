using System.IO;
using UnityEngine;

namespace SaveData
{
    public class SaveJSON
    {
        public void Save(int currentLevel)
        {
            SaveData data = new SaveData();
            data.level = currentLevel;

            string jsonData = JsonUtility.ToJson(data);
#if UNITY_EDITOR
            File.WriteAllText(Application.dataPath + "/save.json", jsonData);
#else
            File.WriteAllText(Application.persistentDataPath + "/save.json", jsonData);
#endif
        }
        
        public int LoadLevel()
        {
            string filePath = "";
#if UNITY_EDITOR
            filePath = Application.dataPath + "/save.json";
#else
            filePath = Application.persistentDataPath + "/save.json";
#endif
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
                return data.level;
            }
            else
            {
                return 1;
            }
        }
        
        [System.Serializable]
        public class SaveData
        {
            public int level;
        }
    }
}