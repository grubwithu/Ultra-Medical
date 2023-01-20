using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanYiLeiBi : MonoBehaviour
{
    private TextAsset contraryRes;
    private List<Pair<string>> contraryDic;
    public Button nextBtn;
    public Text word1;
    public Button read1;
    public Text word2;
    public Button read2;
    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        contraryRes = Resources.Load<TextAsset>("contrary");
        var lines = contraryRes.text.Split("\n");
        contraryDic = new List<Pair<string>>();
        for (int i = 0; i < lines.Length; i++) {
            var words = lines[i].Split("=");
            contraryDic.Add(new Pair<string>(words[0], words[1]));
        }

        nextBtn.onClick.AddListener(() => {
            getNewWord();
        });
        read1.onClick.AddListener(() => {
            wordRead(word1.text);
        });
        read2.onClick.AddListener(() => {
            wordRead(word2.text);
        });

        getNewWord();
    }

   private void getNewWord() {
        var targetWords = contraryDic[UnityEngine.Random.Range(0, contraryDic.Count)];
        word1.text = targetWords.first;
        word2.text = targetWords.second;
    }

    private void wordRead(string word) {
        WWW www = new WWW("https://tts.youdao.com/fanyivoice?word=" + word + "&le=zh&keyfrom=speaker-target");
        if (www.error != null) {
            return;
        }
        var ac = www.GetAudioClip(true, true, AudioType.MPEG);
        audioSrc.clip = ac;
        audioSrc.Play();
    }
}

class Pair<T> {
    public T first;
    public T second;

    public Pair(T value1, T value2) {
        this.first = value1;
        this.second = value2;
    }
}