using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehaviourScript : MonoBehaviour {

    public GameObject loadingImage;
    public Toggle toggle3, toggle4;

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
            Debug.Log("getCheckedScene() : Toggle3", toggle3);
            return 1;
        }
        else if (toggle4.isOn)
        {
            Debug.Log("getCheckedScene() : Toggle4", toggle4);
            return 2;
        }
        else
        {
            Debug.Log( "getCheckedScene() : No toggles On");
            return 0;
        }
    }

    //Quite button trigger
    public void TriggerQuit()
    {
        Application.Quit();
    }
    
}


