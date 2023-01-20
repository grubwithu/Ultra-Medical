using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class DaXiaoZhuanHuan : MonoBehaviour
{
    public GameObject mapBase;
    public GameObject character;
    private const int mapWidth = 1632;
    private const int mapHeight = 940;
    private bool sizeMeasure = false;
    public Text measureText;
    private int target;
    private bool targetInfo;
    public Button bigButton;
    public Button smlButton;

    public RawImage result;
    public Texture trueIco;
    public Texture falseIco;

    private List<GameObject> charList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        init();
        setTarget();
        

        bigButton.onClick.AddListener(() => {
            showResult(targetInfo);
            charList[target].GetComponent<RawImage>().color = Color.white;
            setTarget();
        });
        smlButton.onClick.AddListener(() => {
            showResult(!targetInfo);
            charList[target].GetComponent<RawImage>().color = Color.white;
            setTarget();
        });
    }

    private void init() {
        for (int i = 0; i < 5; i++) {
            for(int y = -360; y <= 360; y+=180) {
                charList.Add(createChar(165 * i + 80, y));
                charList.Add(createChar(-165 * i - 80, y));
            }
        }
    }

    private GameObject createChar(int x, int y) {
        var type = Random.Range(0, 2);
        var size = Random.Range(0, 2);
        var temp1 = Instantiate(character, mapBase.transform);
        temp1.transform.localPosition = new Vector3(x, y, 0);
        temp1.transform.Find("Text").GetComponent<Text>().text = type == 0 ? "小" : "大";
        temp1.transform.Find("Text").GetComponent<Text>().fontSize = size == 0 ? 44 : 85;
        return temp1;
    }

    private void setTarget() {
        target = Random.Range(0, charList.Count);
        charList[target].GetComponent<RawImage>().color = Color.green;
        var size = charList[target].transform.Find("Text").GetComponent<Text>().fontSize;
        var str = charList[target].transform.Find("Text").GetComponent<Text>().text;
        sizeMeasure = Random.Range(0, 2) == 1;
        measureText.text = sizeMeasure ? "大小" : "字义";
        if (sizeMeasure) {
            targetInfo = size == 85;
        } else {
            targetInfo = str == "大";
        }
    }

    private void showResult(bool _rst) {
        result.gameObject.SetActive(true);
        result.GetComponent<RawImage>().texture = _rst ? trueIco : falseIco;
        Invoke("closeResult", 0.5f);
    }
    
    private void closeResult() {
        result.gameObject.SetActive(false);
    }
}
