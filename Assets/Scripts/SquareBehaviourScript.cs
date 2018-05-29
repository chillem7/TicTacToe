using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquareBehaviourScript : MonoBehaviour {

    public Dropdown dropdown;
    public Button btn;
    bool played = false;
    private GameController gameController;
    private Player player;
    public int boardPosition;

    // Sets the square of the player that clicked on it
    // changes the symbol and makes the square non changable
    public void SetSquare()
    {
        player = gameController.GetPlayerTurn();
        btn.image.sprite = player.playerSymbol;
        btn.interactable = false;
        played = true;

        gameController.EndTurn(player.playerValue, boardPosition);

        // Sets a listener If the player symbol is changed
        player.dropdown.onValueChanged.AddListener(delegate
        {
            DrodownValueChanged();
        });

    }

    // Changes the player symbol of the square
    private void DrodownValueChanged()
    {
        btn.image.sprite = player.playerSymbol;
    }

    // Sets the game controller that is in charge of square 
    public void SetGameController(GameController gameController)
    {
        this.gameController = gameController;
    }




}
