using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class diamond : MonoBehaviour
{
    //public GameObject diamondUIPrefab; // UI elmas prefab�n�z� buraya s�r�kleyin

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player")) // "Player" etiketini kendi etiketinizle de�i�tirin
    //    {
    //        // �arpma noktas�n� al
    //        Vector3 hitPoint = other.ClosestPointOnBounds(transform.position);

    //        // UI elmas�n� canvas alt�nda �arpma noktas�nda yarat
    //        GameObject diamondUI = Instantiate(diamondUIPrefab);
    //        diamondUI.transform.SetParent(GameObject.Find("Canvas").transform, false);
    //        diamondUI.transform.position = Camera.main.WorldToScreenPoint(hitPoint);

    //        diamondUI.transform.DOLocalMove(new Vector3(10,300,40),3);
    //    }
    //}


    public GameObject uiPrefab; // UI prefab�n�z� buraya s�r�kleyin

    void Start()
    {
        // UI elementini belirli bir konumda yarat
        GameObject uiElement = Instantiate(uiPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Canvas alt�nda do�ru pozisyona yerle�tir
        uiElement.transform.SetParent(GameObject.Find("Canvas").transform, false);
        uiElement.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0));

        // Y ekseninde 100 birim yukar� ta��
        uiElement.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 100f);
    }
}