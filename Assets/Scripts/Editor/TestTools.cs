using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class TestTools : EditorWindow
{

    GameManager gameManager;
    string directionsString = "Format: Winning State or States and player who plays them.\n" +
        "Can enter one or two winning states. Click Add after set of winning states are enterred " +
        "for each game. Enter next game if desired. Run will automatically run one game. " +
        "Wait tell animations are complete before starting next game or reset back to previouse game\n\n" +
        "Commands are not case sensative, but need to point to a specific Row, Column, or Diagnal\n\n" +
        "Examples: \"R1 P1\",  for a win by player 1 across the first Row \n" +
        "   \"D2 C3 P2\", for a win by player 2 across Forward Diagnal and 3 Column\n\n" +
        "Examples of input not taken:  \"D3 C3 P2\"  There is no diagnal three" +
        "      \"C0 R5 P2\"  Columns start at C1 and The max row is whatever size of\n " +
        "                    game being tested either 3 for 3x3 or 4 for 4x4";

    string comandString = "     \\    D1 = Back slash diagnal \n" +
        "     /    D2 = Forward slash diagnal\n" +
        "     ---  R = Row starting with R1 \n" +
        "     |    C = Column starting with C1 \n" +
        "          Player 1 = P1, Player 2 = P2";
    string testParamString = "";
    string lastPlayedString = "";
    string testString;
    float speedFloat;
    bool groupEnabled;
    bool myBool;
    bool addBool = false;
    
   
    Vector2 scrollPosition, scrollPosidtionActiveGame;
    List<StringTester> testCommandList;
    

    //add to window tab
    [MenuItem("Window/Test Tools")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(TestTools));

    }

    private void Awake()
    {
        Debug.Log("Starting Test Tools : (Awake)");
        // Make sure Editor Application is not open before Editor tool
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
            Debug.Log("Editor Must be Closed when starting Test Tool");

        }

        testString = "r3 c1 P2";
        testCommandList = new List<StringTester> ();
        speedFloat =.1f;
    }

    // Window Code
    private void OnGUI()
    {
        // Editor Window Directions for use
        GUILayout.Label("Directions: ", EditorStyles.boldLabel);
        GUILayout.Box(directionsString, EditorStyles.helpBox);
        GUILayout.Label("Comands:", EditorStyles.boldLabel);
        GUILayout.Label(comandString);

        // Editor Display Command List
       EditorGUILayout.BeginHorizontal();
        {
            
            testString = EditorGUILayout.TextField("Enter Comand", testString);
            addBool = GUILayout.Button("Add",
                GUILayout.MinWidth(50),
                GUILayout.MaxWidth(100),
                GUILayout.ExpandWidth(false));
            if (addBool)
            {
                string addString = AddTest(testString);
                testParamString += (addString == null) ?  "" : addString + " \n";
                addBool = false;
                //scrollPosition = new Vector2(0, 0);
            }
        }       
        EditorGUILayout.EndHorizontal();

        // Editor window Button for comands
        EditorGUILayout.BeginHorizontal();
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition,
                GUILayout.ExpandWidth(true),
                GUILayout.MinHeight(50),
                GUILayout.MaxHeight(70));

                GUILayout.Label(testParamString, 
                    EditorStyles.textArea, 
                    GUILayout.ExpandHeight(true),
                    GUILayout.ExpandWidth(true));

            GUILayout.EndScrollView();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Clear Test Games"))
            {
                testParamString = "";
                testCommandList.Clear();
            
            }
            if (GUILayout.Button("Run Test"))
            {
                RunTest();
            }
            if (GUILayout.Button("Reset To Prior"))
            {
                ResetBackToPrevios();
            }
        }
        EditorGUILayout.EndHorizontal();
        
        GUILayout.Label(" Current Active Game", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        {
            scrollPosidtionActiveGame = GUILayout.BeginScrollView(scrollPosidtionActiveGame,
                GUILayout.ExpandWidth(true),
                GUILayout.MinHeight(100),
                GUILayout.ExpandHeight(true));

            GUILayout.Label(lastPlayedString,
                EditorStyles.textArea,
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true));

            GUILayout.EndScrollView();

            if ( GUILayout.Button("Refresh", GUILayout.MaxWidth(150)) )
            {
                lastPlayedString = LastPlayedToString();
            }           
        }
        EditorGUILayout.EndHorizontal();

    }

    // Runs The next Comand on Test List 
    // Deletes Comand and updates the List and label
    public void RunTest()
    {
        // Check that the Scene not on the main menue
        if (SceneManager.GetActiveScene().buildIndex < 1 &&
            SceneManager.GetActiveScene().buildIndex > 2)
        {
            testCommandList.Clear();
            testParamString = "";
            Debug.Log("Must be Playing Active Game");
            return;
        }
        else if (!EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            Debug.Log("Editor Application is not playing or is paused.");
            testCommandList.Clear();
            testParamString = "";
            return;

        }

        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        // Grab the first String Test argument execute the test in the scene
        StringTester test = testCommandList[0];
        gameController.testing = true;
        gameController.ResetGame();
        TestMoveCreator testMoveCreator = new TestMoveCreator(test);
        testMoveCreator.MakeMoves();
        testMoveCreator.PlayMoves(speedFloat);

        // Find the start of the next Test name and make a new updated list
        int endLine = testParamString.IndexOf('\n');
        if (endLine >= 0)
        {
            testParamString = testParamString.Substring(endLine + 1);

        }
        else
        {
            testParamString = "";
        }
        // Romove the Test that was just run
        if (testCommandList.Count >= 1)
        {
            testCommandList.RemoveAt(0);

        }        
    }

    // Checks if string follows format and adds it to the list of tests
    // Param: The test string in format of colom row or diagnal and player to test
    string AddTest(string test)
    {
        StringTester st = ScriptableObject.CreateInstance<StringTester>();
        test = st.CheckTest(test);

        if (test != null)
        {
            // Add to the list of commands to0 be checked
            testCommandList.Add(st);
            return test;
        }

        return null;
    }

    // Resets the scene board back to the previous configuration before the test started
    // Resets Back to prevouse game if it is found or to blank game board
    void ResetBackToPrevios()
    {
        // Check if the game manager is refrenced to anything
        if (gameManager == null)
        {
            Debug.Log("Null Refrence: GameManager (LastPlayedToString)", gameManager);
            return;
        }

        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.ResetGame();

        // Floats for how quick pieces will be played
        float wait = 0.2f;
        float pause = 0.1f;

        // Get a list of previous movese and check if its empty
        List<Move> moves = gameManager.GetLastGame();
        if (moves.Count == 0)
        {
            Debug.Log("Game Manager Moves List Empty (ResetBackToPrevious)", gameManager);
            gameController.testing = false;
            return;
        }
        // Iterate through and make moves from previouse board
        foreach (var move in moves)
        {
            pause += wait;
            gameController.MakeMove(move, pause);
            
        }
        // Let the Game Controller setup for next player input and turn off testing
        gameController.NextTurn();
        gameController.testing = false;

    }

    // Makes a string of all positions on game board 
    string LastPlayedToString()
    {
        
        // Make sure game is being Played and check if game manager is referenced
        if (!EditorApplication.isPlaying)
        {
            return "Editor Not Running";
        }
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
            Debug.Log("Null Refrence: GameManager (LastPlayedToString)", gameManager);
            return " Game Manager Null Error: ";
        }
        
        string list = "Index   Player    Position:\n";

        List<Move> moves = gameManager.GetLastGame();

        // Check to make sure list is referenced and has moves
        if (moves == null)
        {
            return "Null";
        }
        else if (moves.Count == 0)
        {
            return "None";
        }
        
        int index = 0;
        // Iterate through Moves list and add the strings to be returned
        foreach (var move in moves)
        {
            string player;

            // change player Id to string
            if (move.GetPlayer() >= 0)
                player = (move.GetPlayer() == 0) ? " Player 1  " : " Player 2  ";
            else
                player = " None      ";

            string position = "Pos=" + move.GetPosition() ;

             list += (++index)+ "." + player + position + "\t\n";
        }
        return list;
    }

    private void Update()
    {
        // Check if the Application is Playing and game Manager is set
        if (EditorApplication.isPlaying && gameManager == null)
        {             
            gameManager = GameManager.instance;
             Debug.Log("GameManager null being updated: (Update)");

        }// Make sure game Manager is not used and clear variables
        else if (!EditorApplication.isPlaying)
        {
            gameManager = null;
            testCommandList.Clear();
            testParamString = "";
            Debug.Log("Must be Playing Active Game to add to list");
        }
        else if(EditorApplication.isPlaying)
        {
            // update only if there is a gameManager found and is Playing
            lastPlayedString = LastPlayedToString();
        }
    }
}

   
