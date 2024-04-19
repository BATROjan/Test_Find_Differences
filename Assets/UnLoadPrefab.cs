using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

public class UnloadPrefab : MonoBehaviour
{
    public string prefabKey = "Level_1"; // Ключ префаба в Addressables

    private AsyncOperationHandle<GameObject> loadOperation;

    void Start()
    {
        //UnloadPrefabAsync();
    }

    private async void UnloadPrefabAsync()
    {
        loadOperation = Addressables.LoadAssetAsync<GameObject>("Level_1");
        await loadOperation.Task;
        
        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefabInstance = loadOperation.Result;
            
            Addressables.ReleaseInstance(prefabInstance);
            Instantiate(prefabInstance);
            Debug.Log("Префаб выгружен успешно");
        }
        else
        {
            Debug.LogError("Ошибка загрузки префаба: " + loadOperation.Status);
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
