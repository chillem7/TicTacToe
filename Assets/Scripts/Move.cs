using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class ofMoves to be recorded in linked list
public class Move {

    int player;
    int position;
    int won;
    int boardSize;

    // Constructor default for end of game 
    public Move()
    {
        this.player = -1;
        this.position = -1;
        this.won = 3;
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

    // Returns players Id num 
    public int GetPlayer()
    {
        return this.player;
    }

    // Returns played Position 
    public int GetPosition()
    {
        return this.position;
    }

    // Checks if the move won or Drew the game
    public bool IsWon()
    {
        return (won == -1) ? false : true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
