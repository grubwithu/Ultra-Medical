using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class enter_item : MonoBehaviour
{   
    private Button btn;
    private void Start() {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(()=>{
            SceneManager.LoadScene(gameObject.transform.name);
        });
    }
    private void Update() {
        
    }
}
