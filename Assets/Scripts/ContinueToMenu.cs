using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueToMenu : MonoBehaviour {

    public int scene;

	public void LoadMainMenu()
    {
        SceneManager.LoadScene(scene);
    }
}
