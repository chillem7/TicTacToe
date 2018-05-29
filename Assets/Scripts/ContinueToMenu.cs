using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueToMenu : MonoBehaviour {

    public int scene;

    // LoadSceneMode the main menu
	public void LoadMainMenu()
    {
        SceneManager.LoadScene(scene);
    }
}
