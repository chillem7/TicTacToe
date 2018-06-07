
using UnityEngine;
using UnityEditor;

public class StringTester : EditorWindow
{

    public GameController gameController;
    public int[] start;
    public int[] direction;  
    public int player = 0;
    bool finalStateReached; 

    private void Awake()
    {
        finalStateReached = false;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
    }

    void InitializeArray(int size)
    {
        start = new int[size];
        direction = new int[size];

        for (int i = 0; i < size; i++)
        {
            start[i] = -1;
            direction[i] = -1;
        }
    }

    // Checks the string through finite states
    public string CheckTest(string test)
    {
        //Check if there was a string sent 
        if (test == null)
        {
            Debug.Log("Empty String Argument: (Check String)");
            return null;
        }
        
        // Parse string into an array
        string[] arguments = test.Split(' ');

        // Check for too many arguments
        if (arguments.Length > 3)
        {
            Debug.Log("Too Many Arguments: only 2 winning states possible (CheckTest)");
            return null;
        }

        InitializeArray(arguments.Length - 1);

        string msg = StateOne(arguments);

        return (finalStateReached) ? msg : null;
    }

    // State 1  First Letter must be an R C or D next state looks for number
    // Params: String array of arguments to check
    string StateOne(string[] args)
    {
        int index = 0;
        if (args.Length == index)
        {
            Debug.Log("String Array out of Bound Incorect format (StateOne)");
            return null;
        }
        string arg = args[index];
        char first = (char)arg.ToUpper()[0];

        switch (first)
        {
            case 'R':
                direction[index] = 1;
                start[index] = gameController.gameSize;
                return "Row " + StateTwo(args, index);
            case 'C':
                direction[index] = gameController.gameSize;
                start[index] = 1;
                return "Column " + StateTwo(args, index);
            case 'D':
                direction[index] = 0;
                return "Diagnal " + StateTwo(args, index);
            default:
                Debug.Log("Incorrect first argument: Must be R C D (StateOne)");
                return null;
        }
    }

    // State 2  Second Letter must be A Number 
    // Number must correspond to a possible option
    // Params: String array of arguments to check index of the state
    string StateTwo(string[] args, int index)
    {
        // Check if there is a string in the indexed value
        if (args.Length == index )
        {
            Debug.Log("String Array out of Bound Incorect format (StateTwo)");
            return null;
        }
        string arg = args[index];
        int second;

        if (arg.Length != 2)
        {
            Debug.Log("Argument can only contain two charachters (StateTwo)");
            return null;
        }
        else if (!int.TryParse(arg.Substring(1), out second))
        {
            Debug.Log("Second Charachter Must be integer (StateTwo)");
            return null;
        }
       
        // Get possible number of iterations
        if (direction[index] == 0)
        {
            switch (second)
            {
                case 1:
                    direction[index] = gameController.gameSize + 1;
                    start[index] = 0;
                    return "Forward " + StateThree(args, index + 1);
                case 2:
                    start[index] = gameController.gameSize - 1;
                    direction[index] = gameController.gameSize - 1;
                    return "Backward " + StateThree(args, index + 1);

                default:
                    Debug.Log("Was Not Diagnal (StateTwo)");
                    return null;

            }

        }// Make sure row or column is not out of index game size
        else if(second <= gameController.gameSize)
        {
            switch (second)
            {
                case 1:
                    start[index] *= 0;
                    return " 1 " + StateThree(args, index + 1);
                case 2:
                    start[index] *= 1;
                    return " 2 " + StateThree(args, index + 1);
                case 3:
                    start[index] *= 2;
                    return " 3  " + StateThree(args, index + 1);
                case 4:
                    start[index] *= 3;
                    return " 4 " + StateThree(args, index + 1);
                

                default:
                    Debug.Log("Was Not Inside the possible gameSize (StateTwo)");
                    return null;
            }
        }
        return null;
    }


    // State 3  Checks the next string for P R C D goes back to State 2 
    //    if it is another winning state and goes to state 4 if it is a player
    // Params: String array of arguments to check Index of the state
    string StateThree(string[] args, int index)
    {
        if (args.Length == index)
        {
            Debug.Log("String Array out of Bound Incorect format (StateThree)");
            return null;
        }
        string arg = args[index];
        char first = (char)arg.ToUpper()[0];

        switch (first)
        {
            case 'P':
                return StateFour(args, index);
            case 'R':
                direction[index] = 1;
                start[index] = gameController.gameSize;
                return "Row " + StateTwo(args, index);
            case 'C':
                direction[index] = gameController.gameSize;
                start[index] = 1;
                return "Column " + StateTwo(args, index);
            case 'D':
                direction[index] = 0;
                return "Diagnal " + StateTwo(args, index);
            default:
                Debug.Log("Incorrect first argument: Must be R C D (StateOne)");
                return null;
        }

    }

    // State 4  Checks the next string for the number of Player 
    // Sets all perameters for the test moves if string got to final state
 
    // Params: String array of arguments to check Index of the state
    string StateFour(string[] args, int index)
    {
        if (args.Length == index)
        {
            Debug.Log("String Array out of Bound Incorect format (StateFour)");
            return null;
        }
        else if (args.Length > (index + 1))
        {
            Debug.Log("Got to final State with too many Arguments");
            return null;
        }
        
        string arg = args[index];
        int second;

        if (arg.Length != 2)
        {
            Debug.Log("Argument can only contain two charachters (StateFour)");
            return null;
        }
        else if (!int.TryParse( arg.Substring(1), out second) )
        {
            Debug.Log("Second Charachter Must be integer (StateFour)");
            return null;
        }

        // Get possible number of iterations
        if (second == 1)
        {
            player = 0;
            finalStateReached = true;
            return "Player 1";

        }
        else if (second == 2)
        {
            player = 1;
            finalStateReached = true;
            return "Player 2";

        }
        else
        {
            Debug.Log("Second Charachter Must be Player ID 1 or 2 (StateFour)");
            return null;

        }
    }
    
    
}
