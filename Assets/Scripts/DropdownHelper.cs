using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHelper : MonoBehaviour
{
    
    public Dropdown dropdown0;
    public Dropdown dropdown1;
    public Sprite[] symbols;
    List<Dropdown.OptionData> symbolItems;

    // Use this for initialization
    void Start()
    {
        dropdown0.ClearOptions();
        dropdown1.ClearOptions();

        symbolItems = new List<Dropdown.OptionData>();

        foreach (var sym in symbols)
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
        
        // Initializes the dropdowns and sets different values
        dropdown0.AddOptions(symbolItems);
        dropdown1.AddOptions(symbolItems);
        dropdown0.value = 0;
        dropdown1.value = 1;
    }

    // Sets options in dropdown for both players
    // Param: the dropdown of the player that changed symbol
    public void SetOptionsItems(Dropdown firstDropdown)
    {
        Dropdown secondDropdown = (dropdown0 == firstDropdown) ? dropdown1 : dropdown0;
        List<Dropdown.OptionData> optionDataList = new List<Dropdown.OptionData>();
        string secondValueText= secondDropdown.options[secondDropdown.value].text;
        string firstValueText = firstDropdown.options[firstDropdown.value].text;
        int firstValue = 0;

        // Itterate through to make list of fropodowns for the next player.
        foreach (var option in symbolItems)
        {

            if (option.text != firstDropdown.options[firstDropdown.value].text) // 
            {
                optionDataList.Add(option);

            }
        }

        // check if the new selection was == to the secondDropdown value
        // If it was change the selection to the first value of optionlist
        if (firstDropdown.options[firstDropdown.value] == secondDropdown.options[secondDropdown.value])
        {
            // sets the oblects value to zero then clears the list and adds the new list
            secondDropdown.value = 0;
            secondDropdown.ClearOptions();
            secondDropdown.AddOptions(optionDataList);
            secondValueText = optionDataList[0].text;
            

        }// if it was not change value to old value
        else
        {
            //cleares the list sets the new list 
            secondDropdown.ClearOptions();
            secondDropdown.AddOptions(optionDataList);

            // Itterates through the new list to get the new index of the old oject
            for (int i = 0; i < optionDataList.Count; i++) //
            { 
                if (optionDataList[i].text == secondValueText)
                {
                    //Sets the value in the new dropdown list
                    secondDropdown.value = i;
                    secondValueText = optionDataList[i].text;

                }
            }

        }

        // Clear optionDataList
        optionDataList.Clear();
        int tempFirstValue = 0;

        // Get selected text from second droppdown
        foreach (var option in symbolItems)
        {
            if (option.text == firstValueText)
            {
                firstValue = tempFirstValue;
            }
            // check if seconDropdown value == item in symbolist if not add it
            if (option.text != secondValueText)
            {
                optionDataList.Add(option);
                tempFirstValue++;
            }
            
        }

        // clear the dropdown options and add new list
        firstDropdown.ClearOptions();       
        firstDropdown.AddOptions (optionDataList);
        firstDropdown.value = firstValue;
        

    }


}
