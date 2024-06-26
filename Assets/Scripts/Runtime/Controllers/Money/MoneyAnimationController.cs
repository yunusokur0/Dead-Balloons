using DG.Tweening;
using UnityEngine;

public class MoneyAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject DollarImage;
    [SerializeField] private GameObject pileofcoinparent;

    private Vector3[] initialpoz;
    private Quaternion[] initialrotetion;
    private int coinno=14;

    private void Start()
    {
        initialpoz = new Vector3[coinno];
        initialrotetion = new Quaternion[coinno];

        for (int i = 0; i < pileofcoinparent.transform.childCount; i++)
        {
            initialpoz[i] = pileofcoinparent.transform.GetChild(i).position;
            initialrotetion[i] = pileofcoinparent.transform.GetChild(i).rotation;
        }
    }

    private void Reset()
    {
        for (int i = 0; i < pileofcoinparent.transform.childCount; i++)
        {
            pileofcoinparent.transform.GetChild(i).position = initialpoz[i];
            pileofcoinparent.transform.GetChild(i).rotation = initialrotetion[i];
        }
    }

    public void Rewardpieofcoin()
    {
        Reset();
        var delay = 0f;
        pileofcoinparent.SetActive(true);
        for (int i = 0; i < pileofcoinparent.transform.childCount; i++)
        {
            pileofcoinparent.transform.GetChild(i).DOScale(1F, .3f).SetDelay(delay).SetEase(Ease.OutBack);
            pileofcoinparent.transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(DollarImage.transform.position.x, DollarImage.transform.position.y), .75f).SetDelay(delay + 0.5f).SetEase(Ease.InBack);
            pileofcoinparent.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.OutFlash);
            pileofcoinparent.transform.GetChild(i).DOScale(0F, 0.1F).SetDelay(delay + 1.6f).SetEase(Ease.OutQuint);
            delay += .08f;
        }
    }

    private void OnEnable() => Subscription();

    private void Subscription()
    {
        CoreGameSignals.Instance.dolaranim += Rewardpieofcoin;
    }

    private void UnSubscription()
    {
        CoreGameSignals.Instance.dolaranim -= Rewardpieofcoin;
    }

    private void OnDisable() => UnSubscription();
}
