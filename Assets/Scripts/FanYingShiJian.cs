using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanYingShiJian : MonoBehaviour
{
    public Image upImage;
    public Image downImage;
    public Image leftImage;
    public Image rightImage;
    private Image[] images;
    public Button upBtn;
    public Button downBtn;
    public Button leftBtn;
    public Button rightBtn;
    private Button[] buttons;
    private float waitingTime = 3.0f;
    public Image waiting;
    public Text waitingNum;
    public Text timeText;
    public Text successText;
    public Text failText;
    
    private float nextPicTime = 0.1f;
    private List<float> timeRecorder = new List<float>();
    private float currentTime;
    private bool timing = false;
    private int randomIndex = -1;
    private int successTime = 0;
    private int failTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        upImage.enabled = false;
        downImage.enabled = false;
        leftImage.enabled = false;
        rightImage.enabled = false;
        images = new Image[]{upImage, downImage, leftImage, rightImage};
        buttons = new Button[]{upBtn, downBtn, leftBtn, rightBtn};
        for (int i = 0; i < 4; i++) {
            int temp = i;
            buttons[i].onClick.AddListener(()=>{
                if (timing) {
                    timing = false;
                    if (randomIndex == temp) {
                        timeRecorder.Add(currentTime);
                        successTime++;
                        successText.text = "成功次数：" + successTime.ToString();
                    } else {
                        failTime++;
                        failText.text = "失败次数：" + failTime.ToString();
                    }
                    currentTime = 0f;
                    images[randomIndex].enabled = false;
                    randomIndex = -1;
                    nextPicTime = Random.Range(0.5f, 2.5f);
                    averageTime();
                }
            });
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (waitingTime >= 0) {  // 开始动画
            waitingTime -= Time.deltaTime;
            int temp = (int)waitingTime + 1;
            waitingNum.text = temp.ToString(); 
        } else if (waitingTime < 0 && waiting.enabled == true) { // 项目开始
            waiting.enabled = false;
            waitingNum.enabled = false;
        } else {
            if (nextPicTime > 0) { // 图片即将出现
                nextPicTime -= Time.deltaTime; 
            } else if (nextPicTime <= 0) {
                if (timing) {  // 计时,检测点击
                    currentTime += Time.deltaTime;
                    //timeText.text = currentTime.ToString() + " s";
                } else {  // 图片出现，刷新计时
                    randomIndex = Random.Range(0, 4);
                    //Debug.Log(randomIndex);
                    images[randomIndex].enabled = true;
                    timing = true;
                    currentTime = 0f;
                }
            }
        }

    }

    private void averageTime() {
        if (timeRecorder.Count == 0) {
            timeText.text = "平均用时：---";
            return;
        }
        float sum = 0f;
        for (int i = 0; i < timeRecorder.Count; i++) {
            sum += timeRecorder[i];
        }
        sum /= timeRecorder.Count;
        timeText.text = "平均用时：" + sum.ToString("0.00") + " s";
    }
}
