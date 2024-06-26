using System.Collections;
using UnityEngine;

public class GunAttackController : MonoBehaviour
{
    [SerializeField] private Transform fireTransform;
    [SerializeField] private GameObject levelHolder;
    [SerializeField] private byte bulletSpeed;
    [SerializeField] private float deactiveDelay;
    [SerializeField] private float spawnRate;

    private float _spawnRate;
    private BulletSpawmManager _bulletSpawmManager;

    private void Start()
    {
        _bulletSpawmManager = FindObjectOfType<BulletSpawmManager>();
    }
    public void StartCoroutine()
    {
        StartCoroutine(Fire());
    }

    public void StopCorountine()
    {
        StopAllCoroutines();
    }

    public IEnumerator Fire()
    {
        yield return new WaitForSeconds(0.3f);
        while (true)
        {
            CoreGameSignals.Instance.onVibrate?.Invoke(15);
            _spawnRate = _bulletSpawmManager.spawmRate;
            var playerBullet = PoolSignals.Instance.onGetPoolObject?.Invoke(PoolType.Bullet);
            SetBulletProperties(playerBullet);
            StartCoroutine(DeactivateBullet(playerBullet));
            yield return new WaitForSeconds(_spawnRate + spawnRate);
        }
    }

    private void SetBulletProperties(GameObject playerBullet)
    {
        playerBullet.transform.position = fireTransform.position;
        playerBullet.transform.SetParent(levelHolder.transform);
        playerBullet.SetActive(true);
        Rigidbody rb = playerBullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }

    private IEnumerator DeactivateBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(deactiveDelay);
        PoolSignals.Instance.onReturnToPool?.Invoke(bullet, PoolType.Bullet);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay += StartCoroutine;
        CoreGameSignals.Instance.onLevelSuccessful += StopCorountine;
        CoreGameSignals.Instance.onLevelFailed += StopCorountine;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onPlay -= StartCoroutine;
        CoreGameSignals.Instance.onLevelSuccessful -= StopCorountine;
        CoreGameSignals.Instance.onLevelFailed -= StopCorountine;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}