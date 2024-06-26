using Unity.VisualScripting;
using UnityEngine;

public class LevelLoaderCommand
{
    private readonly LevelManager _levelManager;

    public LevelLoaderCommand(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public void Execute(byte parameter)
    {
        var resourceRequest = Resources.LoadAsync<GameObject>($"LevelPrefabs/level {parameter}");
        resourceRequest.completed += operation =>
        {
            var newLevel = Object.Instantiate(resourceRequest.asset.GameObject(),
                Vector3.zero, Quaternion.identity);
            if (newLevel != null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);
        };
    }
}