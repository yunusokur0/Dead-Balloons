using UnityEngine;

public class PoolInstantiateCommand
{
    private readonly CD_Pool _data;
    private readonly PoolManager _poolManager;

    public PoolInstantiateCommand(CD_Pool poolData, PoolManager poolManager)
    {
        _data = poolData;
        _poolManager = poolManager;
    }

    public void Execute()
    {
        var data = _data.PoolData;
        for (int i = 0; i < data.Count; i++)
        {
            GameObject typeContainer = new GameObject(data[i].Type.ToString());
            typeContainer.transform.SetParent(_poolManager.transform);

            for (int j = 0; j < data[i].Count; j++)
            {
                GameObject spawnedObject = Object.Instantiate(data[i].Prefabs, typeContainer.transform);
                spawnedObject.SetActive(false);
            }
        }
    }
}