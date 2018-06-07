using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SquareBehaviourScript : MonoBehaviour
{

    public Dropdown dropdown;
    public Button btn;
    public Sprite defaultSprite;
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

    // Sets the square of the player that clicked on it
    // changes the symbol and makes the square non changable
    public void SetSquare(int id)
    {
        player = gameController.GetPlayer(id);
        btn.image.sprite = player.playerSymbol;

        btn.interactable = false;
        played = true;



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

    public void ResetSquare()
    {
        ResetValueChanged();
        player = null;
        btn.image.sprite = this.defaultSprite;
        btn.interactable = true;
        played = false;

    }

    void ResetValueChanged()
    {
        // Checks to make sure player is not null
        if (player != null)
        {
            player.dropdown.onValueChanged.RemoveAllListeners();

        }
    }




}