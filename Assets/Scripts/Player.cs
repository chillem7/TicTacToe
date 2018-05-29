using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Dropdown dropdown;
    public int playerValue;
    public Sprite playerSymbol;
    private GameController gameController;

    // Sets players image and dropdown
    public void SetPlayer()
    {
        playerSymbol = dropdown.captionImage.sprite;
        DrodownValueChanged();
    }

   public void DrodownValueChanged() 
    {
        DropdownHelper dropdownHelper = gameObject.GetComponentInParent(typeof(DropdownHelper)) as DropdownHelper;

        dropdownHelper.SetOptionsItems(dropdown);

    }

    public Player GetPlayer()
    {
        return this;
    }

}
