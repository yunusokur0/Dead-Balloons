using UnityEngine.Events;


public class BulletSignals : MonoSingleton<BulletSignals>
{
    public UnityAction onBulletTriggerXDamage = delegate { };
    public UnityAction onBulletTriggerIDamage = delegate { };
    public UnityAction onRefreshDamage = delegate { };
    public UnityAction onRefreshBulletSpawn = delegate { };
    public UnityAction<float> onBulletTriggerSpawn = delegate { };
}