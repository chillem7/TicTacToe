


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

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
    public bool testing = false;
    public GameObject testingPane;

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

        InitBoard();
    }

    private void Update()
    {
        if (testing)
        {
            testingPane.SetActive(true);
        }
        else
        {
            testingPane.SetActive(false);
        }
    }


    // Resets the Gameboard back to default
    public void ResetGame()
    {
        // If End Pane is visable, hide it 
        if (endPane.activeSelf)
        {
            // endPane.SetActive (false);
        }

        // Reset All squares that had been used
        for (int i = 0; i < gameBoard.Length; i++)
        {
            if (gameBoard[i] >= 0)
            {
                buttonList[i].GetComponentInParent<SquareBehaviourScript>().ResetSquare();

            }

            // Set the space back to -1 to show it has not been played
            gameBoard[i] = -1;

        }
        numTurns = 0;
        turn = 0;
    }

    // Makes a test move on the Board
    // Param: Test move to be made
    public void MakeMove(Move move, float pause)
    {
        turn = move.GetPlayer();
        int pos = move.GetPosition();
        if (buttonList[pos] == null)
        {
            Debug.Log("Null Reference: Button List Deleted (MakeMove)  ", this);
            return;
        }
        SquareBehaviourScript sbs = buttonList[pos].GetComponentInParent<SquareBehaviourScript>();

        if (sbs == null)
        {
            Debug.Log("Null Rerence: Square Behavior Script  ", sbs);
            return;
        }
        //sbs.SetSquare();
        gameBoard[pos] = turn;
        StartCoroutine(TimedWait(sbs, pause, turn));
        numTurns++;


    }

    public void LastMove(int playerValue, int playedPosition, float pause)
    {

        StartCoroutine(TimedWaitEndTurn(playerValue, playedPosition, pause));
    }


    IEnumerator TimedWait(SquareBehaviourScript sbs, float pause, int id)
    {
        yield return new WaitForSecondsRealtime(pause);
        sbs.SetSquare(id);

    }

    IEnumerator TimedWaitEndTurn(int playerValue, int playedPosition, float pause)
    {
        yield return new WaitForSecondsRealtime(pause);
        EndTurn(playerValue, playedPosition);
        yield return new WaitForSecondsRealtime(pause );
        endPane.SetActive(false);

    }



    // Called at the end of turn, check if player won or drew
    public void EndTurn(int playerValue, int playedPosition)
    {

        numTurns++;
        gameBoard[playedPosition] = playerValue;

        Move move = new Move(playerValue, playedPosition, gameSize);

        if (IsWinner(playedPosition))
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
        // Add the move to the list if not in testing Mode
        if (!testing)
        {
            if (GameManager.instance == null)
            {
                Debug.Log("Null Reference: GameManager (EndTurn)", gameManager);
                return;
            }

            GameManager.instance.AddMove(move);

        }
    }

    // Checks if the played position wins the game
    // Returns true if position was a winner and false if not
    private bool IsWinner(int playedPosition)
    {
        // Sets up limits to be checked
        int minHorizointal = (playedPosition / gameSize) * gameSize;
        int maxHorizontal = (minHorizointal + gameSize) - 1;
        int minVertical = playedPosition % gameSize;
        int maxVertical = minVertical + (gameSize * (gameSize - 1));

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
        if (playedPosition % (gameSize + 1) == 0)
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
        if (turn == gameBoard[n])
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

    // Returns Player object for player that has id
    public Player GetPlayer(int id)
    {
        return (id == 0) ? players[0] : players[1];
    }

    // Returns Player number of turn
    public void NextTurn()
    {
        turn = (turn == 0) ? 1 : 0;
    }


    private void InitBoard()
    {
        // initialize game board to all neg
        for (int i = 0; i < gameBoard.Length; i++)
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
}
