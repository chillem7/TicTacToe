using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveCreator  
{
    
    int boardSize;
    int[] start;
    int[] direction;
    int gameSize;
    int player;
    float pause;
    int[] board;
    public GameController gameController;
    
    // Constructor for Direct Tests
    public TestMoveCreator(int[] start, int[] direction, int player)
    {
        this.start = start;
        this.direction = direction;
        this.player = player;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        pause = 0.1F;
        gameSize = gameController.gameSize;

    }

    // Constructor takes a String Tester object
    public TestMoveCreator(StringTester st)
    {
        this.start = st.start;
        this.direction = st.direction;
        this.player = st.player;
        gameController = st.gameController;
        pause = 0.01F;
        gameSize = gameController.gameSize;

    }

    // Makes a gameboard array with moves
    public void MakeMoves()
    {
        // Create the board initialize to -1
        board = new int[gameSize * gameSize];
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = -1;
         
        }
        // Check to make sure Arrays are of the same size
        if (start.Length != direction.Length)
        {
            Debug.Log("Index unequal: (MakeMoves)+" +          
                   "\n Game size " + gameSize +
                   "\n Start Length " + start.Length +
                   "\n Direction Length" + direction.Length);
            return;
        }

        // Iterate through all winning states
        for (int i = 0; i < start.Length; i++)
        {
            // Check to see if index will go out of bounds
            if ( ((direction[i] * (gameSize - 1)) + start[i]) > (gameSize * gameSize))
            {
                Debug.Log("Index out of Bounds: (MakeMoves)+" +
                    "\n Winning States" + i +
                    "\n Game size " + gameSize +
                    "\n Start Pos " + start[i] +
                    "\n Direction " + direction[i]);
                return;

            }
            // Iterate through and add player id to board in direction of winning state
            for (int ii = 0, j = start[i]; ii < gameSize; ii++, j += direction[i])
            {
                board[j] = player;

            }
        }
    }

    //Plays moves in gameboard array on current gameBoard
    public void PlayMoves(float speed)
    {
        // Check that the Game Board has not been switched
        if (GameObject.Find("GameController").GetComponent<GameController>() != gameController)
        {
            Debug.Log("Incorrect Game Controller: can not play on board");
            return;
        }
        pause = speed ;
        int lastPlayed = 0;

        // Itterate through  the Game Board
        for (int i = 0; i < board.Length; i++)
        {
            // If played on boardArray then make the move on screen
            if (board[i] == player)
            {
                pause += speed;
                gameController.MakeMove(new Move(player, i, board.Length), pause);
                lastPlayed = i;
            }
        }

        pause += (speed * 4);

        // Check for win
        gameController.LastMove(player, lastPlayed, pause);

    }

}


    