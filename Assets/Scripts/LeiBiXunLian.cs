using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeiBiXunLian : MonoBehaviour
{
    private TextAsset similarRes;
    private List<Words> similarDic;
    public Button nextBtn;
    public Text word1;
    public Button read1;
    public Text word2;
    public Button read2;
    public AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        similarRes = Resources.Load<TextAsset>("similar");
        var lines = similarRes.text.Split('\n');
        similarDic = new List<Words>();
        for (int i = 0; i < lines.Length; i++) {
            //Debug.Log(lines[i]);
            var temp = new Words(lines[i]);
            if (temp.Count >= 2) { similarDic.Add(temp); }
        }
        //similarDic[0].printInfo();

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
        var targetWords = similarDic[UnityEngine.Random.Range(0, similarDic.Count)];
        var index1 = UnityEngine.Random.Range(0, targetWords.Count);
        int index2;
        do {
            index2 = UnityEngine.Random.Range(0, targetWords.Count);
        } while (index1 == index2);
        word1.text = targetWords.getWord(index1);
        word2.text = targetWords.getWord(index2);
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

class Words {
    public string Name { get => name; }
    public char Type { get => type; }
    public int Count { get => words.Count; }

    private string name;
    private char type;
    private List<string> words = new List<string>();
    public Words(string line) {
        name = line.Substring(0, 7);
        type = line[7];
        for (int i = 8; i < line.Length; i++) {
            if (line[i] == ' ' && i >= line.Length) { continue; } else {
                string temp = "";
                while (i < line.Length && line[i] != ' ') {
                    temp += line[i];
                    i++;
                }
                if (temp != "" && temp.Length == 2) words.Add(temp);
            }
        }
    }
    public void printInfo() {
        Debug.Log("项目编号：" + this.name);
        Debug.Log("词语个数：" + Convert.ToString(this.words.Count)
                               + "  项目类型：" + this.type);
        string temp = "";
        words.ForEach(word => {
            temp += (word + " ");
        });
        Debug.Log(temp);
    }

    public string getWord(int index) {
        return words[index];
    }
}