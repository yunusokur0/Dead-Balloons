using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] internal GameObject levelHolder;
    [SerializeField] private byte totalLevelCount;
    [SerializeField] private List<Material> skyBoxList;
    private LevelLoaderCommand _levelLoader;
    private LevelDestroyerCommand _levelDestroyer;
    private byte _currentLevel;

    private void Awake()
    {
        Init();
        _currentLevel = GetActiveLevel();
    }

    private void Start()
    {
        NewSkyBox();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        SetLevelText();
    }

    private void Init()
    {
        _levelLoader = new LevelLoaderCommand(this);
        _levelDestroyer = new LevelDestroyerCommand(this);
    }

    private byte GetLevelID()
    {
        return _currentLevel;
    }

    private byte GetActiveLevel()
    {
        if (!ES3.FileExists()) return 0;
        return (byte)(ES3.KeyExists("Level") ? ES3.Load<byte>("Level") % totalLevelCount : 0);
    }

    private void NewSkyBox()
    {
        if(_currentLevel>=10)
        {
            RenderSettings.skybox = skyBoxList[_currentLevel/10];
            DynamicGI.UpdateEnvironment();
        }    
    }

    private void OnNextLevel()
    {
        _currentLevel++;
        SaveSignals.Instance.onSaveGameData?.Invoke();
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        BulletSignals.Instance.onRefreshDamage?.Invoke();
        BulletSignals.Instance.onRefreshBulletSpawn?.Invoke();
        NewSkyBox();
        SetLevelText();
        SpinPanelOpen();
    }

    private void OnRestartLevel()
    {
        SaveSignals.Instance.onSaveGameData?.Invoke();
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        BulletSignals.Instance.onRefreshDamage?.Invoke();
        BulletSignals.Instance.onRefreshBulletSpawn?.Invoke();
        SetLevelText();
    }

    private void SpinPanelOpen()
    {
        if ((_currentLevel + 1) % 4 == 0)
        {
            CoreUISignals.Instance.onOpenPanel?.Invoke(UIPanelTypes.Spin, 5);
        }
    }

    private void SetLevelText()
    {
        UISignals.Instance.onSetLevelText?.Invoke(_currentLevel);
    }

    private void OnEnable() => SubscribeEvents();
    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _levelLoader.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
        SaveSignals.Instance.onGetLevelID += GetLevelID;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= _levelLoader.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
        SaveSignals.Instance.onGetLevelID -= GetLevelID;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
    }
    private void OnDisable() => UnsubscribeEvents();
}