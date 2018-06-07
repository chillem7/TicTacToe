using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationHelper : MonoBehaviour {

    [Tooltip("Dropdown box of player selecatable symbols")]
    public Dropdown dropdown;

    [Tooltip("The images of chosen objects")]
    public Sprite[] symbols;

    // Use this for initialization
    void Start () {
        dropdown.ClearOptions();

        List<Dropdown.OptionData> symbolItems = new List<Dropdown.OptionData>();
        foreach(var sym in symbols)
        {
            string symbolName = sym.name;
            // Index the spot of the - 
            int dash = sym.name.IndexOf('-');
            // Check if dash is found if so change the name of tstring name
            if (dash >= 0)
            {
                symbolName = symbolName.Substring(dash + 1);
            }
            // Create Variable of new dropdown opption data
            var symbolOption = new Dropdown.OptionData(symbolName, sym);
            // Add to list
            symbolItems.Add(symbolOption);

        }
        // Add entire list to dropodown Option
        dropdown.AddOptions(symbolItems);
	}
	
	
}
