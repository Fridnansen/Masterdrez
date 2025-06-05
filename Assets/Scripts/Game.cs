using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[16, 16];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] playerYellow = new GameObject[16];
    private GameObject[] playerBlue = new GameObject[16];

    //current turn
    private string currentPlayer = "white";

    //Game Ending
    private bool gameOver = false;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        playerWhite = new GameObject[] { Create("white_rook", 4, 0), Create("white_knight", 5, 0),
            Create("white_bishop", 6, 0), Create("white_queen", 7, 0), Create("white_king", 8, 0),
            Create("white_bishop", 9, 0), Create("white_knight", 10, 0), Create("white_rook", 11, 0),
            Create("white_pawn", 4, 1), Create("white_pawn", 5, 1), Create("white_pawn", 6, 1),
            Create("white_pawn", 7, 1), Create("white_pawn", 8, 1), Create("white_pawn", 9, 1),
            Create("white_pawn", 10, 1), Create("white_pawn", 11, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 4, 15), Create("black_knight",5,15),
            Create("black_bishop",6,15), Create("black_queen",7,15), Create("black_king",8,15),
            Create("black_bishop",9,15), Create("black_knight",10,15), Create("black_rook",11,15),
            Create("black_pawn", 4, 14), Create("black_pawn", 5, 14), Create("black_pawn", 6, 14),
            Create("black_pawn", 7, 14), Create("black_pawn", 8, 14), Create("black_pawn", 9, 14),
            Create("black_pawn", 10, 14), Create("black_pawn", 11, 14) };
        playerYellow = new GameObject[] { Create("yellow_rook", 0, 4), Create("yellow_knight", 0, 5),
            Create("yellow_bishop", 0, 6), Create("yellow_queen", 0, 7), Create("yellow_king", 0, 8),
            Create("yellow_bishop", 0, 9), Create("yellow_knight", 0, 10), Create("yellow_rook", 0, 11),
            Create("yellow_pawn", 1, 4), Create("yellow_pawn", 1, 5), Create("yellow_pawn", 1, 6),
            Create("yellow_pawn", 1, 7), Create("yellow_pawn", 1, 8), Create("yellow_pawn", 1, 9),
            Create("yellow_pawn", 1, 10), Create("yellow_pawn", 1, 11) };
        playerBlue = new GameObject[] { Create("blue_rook", 15, 4), Create("blue_knight",15,5),
            Create("blue_bishop",15,6), Create("blue_queen",15,7), Create("blue_king",15,8),
            Create("blue_bishop",15,9), Create("blue_knight",15,10), Create("blue_rook",15,11),
            Create("blue_pawn", 14, 4), Create("blue_pawn", 14, 5), Create("blue_pawn", 14, 6),
            Create("blue_pawn", 14, 7), Create("blue_pawn", 14, 8), Create("blue_pawn", 14, 9),
            Create("blue_pawn", 14, 10), Create("blue_pawn", 14, 11) };

        //Set all piece positions on the positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
            SetPosition(playerYellow[i]);
            SetPosition(playerBlue[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "blue";
        }
        else if (currentPlayer == "blue")
        {
            currentPlayer = "black";
        }
        else if (currentPlayer == "black")
        {
            currentPlayer = "yellow";
        }
        else if (currentPlayer == "yellow")
        {
        currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Masterdrez"); //Restarts the game by loading the scene over again
        }
    }
    
    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " es el ganador";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}
