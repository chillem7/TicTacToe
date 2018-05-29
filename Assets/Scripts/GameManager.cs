using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a Linked list to hold all moves accross all the games during a session 
// In memory
// also saves the toggle settings for the main menu
public class GameManager : MonoBehaviour {
    // singleton instance
    public static GameManager instance = null;
    protected static List<Move> moves;
    public static int savedToggle = 3;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            moves = new List<Move>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        // keeps active when scenes change
        DontDestroyOnLoad(gameObject);
    }

    // Adds move to the linked list in memory
    public void AddMove(Move move)
    {
        moves.Add(move);
   
    }

    //Saves the toggle location
    // Param: 3 for 3x3, and 4 for 4x4
    public void SetSavedToggle(int savedToggleNum)
    {
        savedToggle = savedToggleNum;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
