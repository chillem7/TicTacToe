using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject gridObject;
    public Player[] players;
    private int turn = 0;
    public Text[] buttonList;
    public int max_turns;
    public int numTurns = 0;
    public int[] gameBoard;
    public int gameSize;
    public GameObject endPane;
    public Dropdown[] dropList;
    public Button continueToMenu;
    public Text endPaneText;
    public GameObject gameManager;
    
    private void Awake()
        {
            // Check if Game manager has been instantiated
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }
            
            endPane.SetActive(false);
            SetSquareGameController();
            SetPlayersFromDropdown();
            gameBoard = new int[max_turns];

            // initialize game board to all neg
            for (int i=0; i<gameBoard.Length; i++)
            {
                gameBoard[i] = -1;
            }
        }
    // Attaches the player objects to script
    void SetPlayersFromDropdown()
    {
        players = new Player[2];
        for (int i = 0; i < dropList.Length; i++)
        {
           players[i] = dropList[i].GetComponent<Player>().GetPlayer();

        }
    }   

    // Called at the end of turn, check if player won or drew
    public void EndTurn(int playerValue, int playedPosition)
    {

        numTurns++;
        gameBoard[playedPosition] = playerValue;
        Move move = new Move(playerValue, playedPosition, gameSize);
      
        if ( IsWinner(playedPosition) )
        {
            
            string winner = "Player " + (turn + 1) + " won !!!";

            endPaneText.text = winner;
            endPane.SetActive(true);

            // Enter Winner into Move Object
            move.SetWinner(playerValue);

        }
        else if (numTurns < max_turns)
        {

            NextTurn();
     
        }
        else
        {
            // Enter 3 for draw
            move.SetWinner(3);
            endPaneText.text = "Draw :D";
            endPane.SetActive(true);

        }
        // Add the move to the list
        GameManager.instance.AddMove(move);   
    }

    // Checks if the played position wins the game
    // Returns true if position was a winner and false if not
    private bool IsWinner(int playedPosition)
    {
        // Sets up limits to be checked
        int minHorizointal = (playedPosition / gameSize) * gameSize;
        int maxHorizontal = (minHorizointal + gameSize) - 1;
        int minVertical = playedPosition % gameSize;
        int maxVertical = minVertical + (gameSize * (gameSize -1));

        // Check Horizontal ---
        if (gameSize == Check(minHorizointal, maxHorizontal, 1))
        {
            return true;
        }
        // Check Vertical |
        if (gameSize == Check(minVertical, maxVertical, gameSize))
        {
            return true;
        }
        // Check if the played position was on the backward Diagonal \
        if (playedPosition % (gameSize+1) == 0)
        {
            int maxDiagnal = max_turns - 1;
            int dir = gameSize + 1;
            //check the Diagonal
            return (gameSize == Check(0, maxDiagnal, dir)) ? true : false;
        }
        //  Check if the played position was on the forward Diagonal /
        if (playedPosition % (gameSize - 1) == 0)
        {
            int minDiagnal = gameSize - 1;
            int maxDiagnal = minDiagnal * gameSize;
            int dir = gameSize - 1;
            //check the Diagonal
            return (gameSize == Check(minDiagnal, maxDiagnal, dir)) ? true : false;
        }

        return false;
    }

    // Checks the line to see if all spots are owned by the player for the win
    // Param: spot being checked, Limit of line, incremental direction
    // returns 1 if spot is owned by the player
    private int Check(int n, int limit, int dir)
    {
        // Check if turn is players square
        if (turn == gameBoard[n] )
        {
            // Get next position and check if it is the limit
            int next = n + dir;
            if (next > limit)
            {
                return 1;
            }
            // Return 1 + and check the next spot
            int ans = Check(next, limit, dir) + 1;
            return ans;
            
        }
        return 0; 
    }

    // Attaches game Controller to all the square buttons
    void SetSquareGameController()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<SquareBehaviourScript>().SetGameController(this);
        }
    }

    // Returns Player object for player that has turn
    public Player GetPlayerTurn()
    {
        return (turn == 0) ? players[0] : players[1];
    }

    // Returns Player number of turn
    private void NextTurn()
    {
        turn = (turn == 0) ? 1 : 0;
    }

}





