using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class ofMoves to be recorded in linked list
public class Move : MonoBehaviour {

    int player;
    int position;
    int won;
    int boardSize;

    // Constructor 
    public Move()
    {
        this.player = -1;
        this.position = -1;
        this.won = -1;
        this.boardSize = 0;
    }

    // Constructor with 4 param
    public Move(int player, int position, int boardSize)
    {
        this.player = player;
        this.position = position;
        this.won = -1;
        this.boardSize = boardSize;
    }

    // constructor 5 parameters
    public Move(int player, int position, int won, int boardSize)
    {
        this.player = player;
        this.position = position;
        this.won = won;
        this.boardSize = boardSize;

    }
	
    // sets the winner to player value or 3 for draw
    // negative means nobody has won yet
    public void SetWinner(int winner)
    {
        won = winner;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
