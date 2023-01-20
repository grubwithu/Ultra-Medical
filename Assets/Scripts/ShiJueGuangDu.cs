using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShiJueGuangDu : MonoBehaviour
{
    public GameObject[] prefabs;
    private List<GameObject> objects;
    public Canvas canvas;
    private float waitingTime = 3.0f;
    public Image waiting;
    public Text waitingNum;
    public Button menuBtn;
    public RawImage menu;
    public Text showTimeText;
    private float showTime;
    public Slider showTimeSlider;
    public Button closeBtn;
    public Text answerText;
    public Button okBtn;
    private bool menuOn = false;
    public GameObject answer;
    private bool answerOn = false;
    private float timing = 0f;
    public RawImage result;
    public Texture trueImage;
    public Texture falseImage;

    private List<GameObject> items;
    // Start is called before the first frame update
    void Start()
    {
        showTime = showTimeSlider.value;
        showTimeSlider.onValueChanged.AddListener((float value)=>{
            showTimeText.text = value.ToString("0.00") + " s";
            showTime = value;
        });
        menuBtn.onClick.AddListener(()=>{
            menu.gameObject.SetActive(true);
            if (waitingTime > 0) {
                waiting.gameObject.SetActive(false);
            }
            if (answerOn) {
                answer.gameObject.SetActive(false);
            }
            menuOn = true;
        });
        closeBtn.onClick.AddListener(()=>{
            menu.gameObject.SetActive(false);
            if (waitingTime > 0) {
                waiting.gameObject.SetActive(true);
            }
            if (answerOn) {
                answer.gameObject.SetActive(true);
            }
            menuOn = false;
        });
        okBtn.onClick.AddListener(()=>{
            int clientAnswer = int.Parse(answerText.text);
            answer.SetActive(false);
            if (clientAnswer == objects.Count) {
                result.gameObject.SetActive(true);
                result.gameObject.GetComponent<RawImage>().texture = trueImage;
            } else {
                result.gameObject.SetActive(true);
                result.gameObject.GetComponent<RawImage>().texture = falseImage;
            }
            Invoke("reinit", 2.0f);
        });
        objects = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuOn) {

        } else {
            if (waitingTime >= 0) {  // 开始动画
                waitingTime -= Time.deltaTime;
                int temp = (int)waitingTime + 1;
                waitingNum.text = temp.ToString(); 
            } else if (waitingTime < 0 && waiting.IsActive() == true) { // 项目开始
                waiting.gameObject.SetActive(false);
                timing = showTime;
                this.generate();
            } else {
                if (timing >= 0) {
                    timing -= Time.deltaTime;
                } else if (!answerOn) {
                    objects.ForEach((GameObject g)=>{
                        g.gameObject.SetActive(false);
                    });
                    answerOn = true;
                    answer.SetActive(true);
                }
            }
        }
        
    }

    private void generate() {
        var target = prefabs[Random.Range(0, prefabs.GetLength(0))];
        var number = Random.Range(6, 14);
        for (int i = 0; i < number; i++) {
            var temp = Instantiate(target, canvas.transform);
            Replace:
            var xPos = Random.Range(-900, 901);
            var yPos = Random.Range(-425, 426);
            temp.transform.localPosition = new Vector3(xPos, yPos, 0);
            bool validate = true;
            objects.ForEach((GameObject g) => {
                var vec = temp.transform.localPosition - g.transform.localPosition;
                if (vec.magnitude < 70f) {
                    validate = false;
                }
            });
            if (!validate) goto Replace;
            objects.Add(temp);
        }
    }
    
    private void reinit() {
        waitingTime = 3.0f;
        if (!menuOn) {
            waiting.gameObject.SetActive(true);
        }
        result.gameObject.SetActive(false);
        //menuOn = false;
        answerOn = false;
        answerText.text = "";
        objects.ForEach((GameObject g) => {
            Destroy(g);
        });
        objects = new List<GameObject>();
    }
}
