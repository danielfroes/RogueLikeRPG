using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button defaultButton;
    public Button backButton;
    
    void Start(){
        Debug.Log("start");
        SelectDefault();
    }

    public void SelectDefault(){
        if (defaultButton != null){
            defaultButton.Select();
        }

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void SelectBack(){
        if (backButton != null){
            backButton.Select();
        }
    }
}
