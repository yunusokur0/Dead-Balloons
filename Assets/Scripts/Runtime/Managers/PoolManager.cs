using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private CD_Pool _pooldata;
    private PoolInstantiateCommand _poolInstantiateCommand;

    private void Awake()
    {
        _pooldata = GetPoolData();
        Init();      
    }

    private void Start()
    {
        _poolInstantiateCommand.Execute();
    }

    private void Init()
    {
        _poolInstantiateCommand = new PoolInstantiateCommand(_pooldata, this);
    }

    private CD_Pool GetPoolData()
    {
        return Resources.Load<CD_Pool>("Data/CD_Pool");
    }

    public GameObject OnGetPoolObject(PoolType poolType)
    {
        var parent = transform.GetChild((int)poolType);
        var obj = parent.childCount != 0
            ? parent.transform.GetChild(0).gameObject
            : null;
        return obj;
    }

    private void OnReturnToPool(GameObject pooledObject, PoolType poolType)
    {
        pooledObject.SetActive(false);
        pooledObject.transform.position = transform.position;
        pooledObject.transform.parent = transform.GetChild((byte)poolType);
    }

    private void OnEnable() => SubscribeEvent();

    private void SubscribeEvent()
    {
        PoolSignals.Instance.onGetPoolObject += OnGetPoolObject;
        PoolSignals.Instance.onReturnToPool += OnReturnToPool;
    }

    private void UnSubscribeEvent()
    {
        PoolSignals.Instance.onGetPoolObject -= OnGetPoolObject;
        PoolSignals.Instance.onReturnToPool -= OnReturnToPool;
    }

    private void OnDisable() => UnSubscribeEvent();
}