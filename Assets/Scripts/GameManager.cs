using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates a Linked list to hold all moves accross all the games during a session 
// In memory
// also saves the toggle settings for the main menu
public class GameManager : MonoBehaviour {
    // singleton instance
    public static GameManager instance = null;
    private List<Move> moves;
    private int savedToggle = 3;

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

    // Returns the Last game if incomplete
    public List<Move> GetLastGame()
    {
        List<Move> lastGame = new List<Move>();
        
        foreach (var move in moves)
        {
            // Add Move to list
            lastGame.Add(move);

            // If a new game was started Clear the list and start adding again
            if (move.IsWon())
            {
                lastGame.Clear();
               
            }
            
        }
        return lastGame;
          
    }

    public int GetSavedToggle()
    {
        return savedToggle;
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
