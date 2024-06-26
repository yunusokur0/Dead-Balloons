using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class diamond : MonoBehaviour
{
    //public GameObject diamondUIPrefab; // UI elmas prefabýnýzý buraya sürükleyin

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")) // "Player" etiketini kendi etiketinizle deðiþtirin
    //    {
    //        // Çarpma noktasýný al
    //        Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);

    //        // UI elmasýný canvas altýnda çarpma noktasýnda yarat
    //        GameObject diamondUI = Instantiate(diamondUIPrefab);
    //        diamondUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
    //        diamondUI.transform.position = Camera.main.WorldToScreenPoint(hitPoint);

    //        diamondUI.transform.DOLocalMove(new Vector3(10,300,40),3);
    //    }
    //}


    public GameObject uiPrefab; // UI prefabýnýzý buraya sürükleyin

    void Start()
    {
        // UI elementini belirli bir konumda yarat
        GameObject uiElement = Instantiate(uiPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Canvas altýnda doðru pozisyona yerleþtir
        uiElement.transform.SetParent(GameObject.Find("Canvas").transform, false);
        uiElement.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));

        // Y ekseninde 100 birim yukarý taþý
        uiElement.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 100f);
    }
}