using TMPro;
using UnityEngine;


public class miniGame : MonoBehaviour
{
    [SerializeField] private GameObject wallStage;
    public Transform transsform;
    public Material[] colors;

    //private void Awake()
    //{
    //    NewWallStage();
    //}

    //private void NewWallStage()
    //{
    //    for (int i = 0; i <= 50; i++)
    //    {
    //        var ob = Instantiate(wallStage, transsform);
    //        ob.transform.localPosition = new Vector3(0, 0, i * 9);

    //        Material color = colors[i % colors.Length];

    //        ob.GetComponent<Renderer>().material = color;
    //        ob.transform.GetChild(0).GetComponent<TextMeshPro>().text = "X" + (i + 1);

    //        Transform firstChild = ob.transform.GetChild(1);
    //        MiniGameManager miniGameManager = firstChild.gameObject.AddComponent<MiniGameManager>();
    //        miniGameManager.stageID = i;
    //        Transform firstChild1 = ob.transform.GetChild(2);
    //        MiniGameManager miniGameManager1 = firstChild1.gameObject.AddComponent<MiniGameManager>();
    //        miniGameManager1.stageID = i;
    //        Transform firstChild2 = ob.transform.GetChild(3);
    //        MiniGameManager miniGameManager2 = firstChild2.gameObject.AddComponent<MiniGameManager>();
    //        miniGameManager2.stageID = i;
    //    }
    //}
}