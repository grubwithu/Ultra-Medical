using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class return_menu : MonoBehaviour
{
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(()=>{
            SceneManager.LoadScene("swipe menu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
