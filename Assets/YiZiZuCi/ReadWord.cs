using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadWord : MonoBehaviour
{
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GameObject.FindObjectOfType<AudioSource>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            var temp = this.transform.Find("Text").gameObject;
            wordRead(temp.GetComponent<Text>().text);
        });
    }

    private void wordRead(string word) {
        //WWW www = new WWW("https://tts.youdao.com/fanyivoice?word=" + word + "&le=zh&keyfrom=speaker-target");
        WWW www = new WWW("https://fanyi.sogou.com/reventondc/synthesis?text=" + word + "&speed=2&lang=zh-CHS&from=translateweb&speaker=6");

        if (www.error != null) {
            return;
        }
        var ac = www.GetAudioClip(true, true, AudioType.MPEG);
        audioSrc.clip = ac;
        audioSrc.Play();
    }
}
