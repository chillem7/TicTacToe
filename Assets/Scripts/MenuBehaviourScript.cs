using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviourScript : MonoBehaviour {

    public GameObject loadingImage;
    public Toggle toggle3, toggle4;
    public GameObject gameManager;

    private void Awake()
    {
        // Check if Game manager has been instantiated
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        // Loads toggle setting
        if (GameManager.savedToggle != 0)
        {
            SetPreviouseToggle();
        }
    }

    // sets the previouse toggle
    public void SetPreviouseToggle()
    {
        Toggle onToggle = (GameManager.savedToggle == 3) ? toggle3 : toggle4;

        onToggle.isOn = true;

        Toggle offToggle = (GameManager.savedToggle != 4) ? toggle3 : toggle4;

        offToggle.isOn = false;
    }

    // Loads checked scene
    public void LoadBoard()
    {
        //Starts the loading screen while Scene is being loaded
        loadingImage.SetActive(true);
        
        SceneManager.LoadScene(GetCheckedScene());
        
    }

    int GetCheckedScene()
    {
        // Check if toggle for 3x3 is activated
        // If not check toggle for 4x4 else return 0
        if (toggle3.isOn)
        {
            //Debug.Log("getCheckedScene() : Toggle3", toggle3);
            GameManager.instance.SetSavedToggle(3);
            return 1;
        }
        else if (toggle4.isOn)
        {
            //Debug.Log("getCheckedScene() : Toggle4", toggle4);
            GameManager.instance.SetSavedToggle(4);
            return 2;
        }
        else
        {
            //Debug.Log( "getCheckedScene() : No toggles On");
            return 0;
        }
    }



    //Quite button trigger
    public void TriggerQuit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
            Application.Quit();

    }

}


