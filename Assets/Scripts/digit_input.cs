using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class digit_input : MonoBehaviour
{
    public Button[] buttons;
    public Text digitText;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++) {
            int temp = i;
            buttons[i].onClick.AddListener(()=>{
                digitText.text += temp.ToString();
            });
        }
    }
}
