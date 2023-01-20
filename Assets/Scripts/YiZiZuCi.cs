using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class YiZiZuCi : MonoBehaviour
{
    public Canvas canvas;
    public GameObject word;
    public Text character;
    public Button nextBtn;
    private List<GameObject> wordsObj;
    private List<string> characters;
    private List<List<string>> words;

    // Start is called before the first frame update
    void Start()
    {
        wordsObj = new List<GameObject>();
        generateWordLayer();
        readDic();
        getNewOne();

        nextBtn.onClick.AddListener(() => {
            getNewOne();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateWordLayer() {
        for (int y = 350; y >= -450; y -=150) {
            var temp = Instantiate(word, canvas.transform);
            temp.transform.localPosition = new Vector3(100, y, 0);
            wordsObj.Add(temp);
            temp = Instantiate(word, canvas.transform);
            temp.transform.localPosition = new Vector3(598, y, 0);
            wordsObj.Add(temp); 
        }
    }

    private void readDic() {
        characters = new List<string>();
        words = new List<List<string>>();
        var dicRes = Resources.Load<TextAsset>("dictionary");
        var lines = dicRes.text.Split("\n");
        for (int i = 0; i < lines.Length; i+=2) {
            characters.Add(lines[i]);
            words.Add(new List<string>(lines[i + 1].Split("-")));
            words.Last().RemoveAt(words.Last().Count - 1);
        }
    }

    private void getNewOne() {
        var target = UnityEngine.Random.Range(0, characters.Count);
        character.text = characters[target];
        List<int> indexs= new List<int>();
        while (indexs.Count != wordsObj.Count) {
            int index;
            do {
                index = UnityEngine.Random.Range(0, words[target].Count);

            } while (indexs.Contains(index));
            var word = wordsObj[indexs.Count].transform.Find("Text").gameObject;
            word.GetComponent<Text>().text = words[target][index];
            indexs.Add(index);
        }
    }
}
