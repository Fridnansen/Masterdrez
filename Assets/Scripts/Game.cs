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
            Create("white_bishop", 6, 0), Create("white_queen", 8, 0), Create("white_king", 7, 0),
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
            Create("yellow_bishop", 0, 6), Create("yellow_queen", 0, 8), Create("yellow_king", 0, 7),
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
       
       // if ((x < 4 && y < 4) || (x < 4 && y > 11) || (x > 11 && y < 4) || (x > 11 && y > 11))return false;
       //     return true; // revisar
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
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " son los ganadores";
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

    // para Enroque
    public bool CanCastle(string player, bool isShort)
    {
        int kingX = -1, kingY = -1, rookX = -1, rookY = -1;

        switch (player)
        {
            case "white":
                kingX = 7; kingY = 0;
                rookX = isShort ? 11 : 4; rookY = 0;
                break;
            case "black":
                kingX = 8; kingY = 15;
                rookX = isShort ? 11 : 4; rookY = 15;
                break;
            case "yellow":
                kingX = 0; kingY = 7;
                rookX = 0; rookY = isShort ? 11 : 4;
                break;
            case "blue":
                kingX = 15; kingY = 8;
                rookX = 15; rookY = isShort ? 11 : 4;
                break;
        }

        Chessman king = GetPosition(kingX, kingY)?.GetComponent<Chessman>();
        Chessman rook = GetPosition(rookX, rookY)?.GetComponent<Chessman>();

        if (king == null || rook == null || king.HasMoved() || rook.HasMoved()) return false;

        // Verifica que no haya piezas entre el rey y la torre
        int dx = Mathf.Clamp(rookX - kingX, -1, 1);
        int dy = Mathf.Clamp(rookY - kingY, -1, 1);
        int x = kingX + dx, y = kingY + dy;

        while (x != rookX || y != rookY)
        {
            if (GetPosition(x, y) != null) return false;
            x += dx;
            y += dy;
        }

        return true;
    }

    public void PerformCastle(string player, bool isShort)
    {
        int kingX, kingY, rookX, rookY, newKingX, newKingY, newRookX, newRookY;

        switch (player)
        {
            case "white":
                kingX = 7; kingY = 0;
                rookX = isShort ? 11 : 4; rookY = 0;
                newKingX = isShort ? 9 : 5; newKingY = 0;
                newRookX = isShort ? 8 : 6; newRookY = 0;
                break;
            case "black":
                kingX = 8; kingY = 15;
                rookX = isShort ? 11 : 4; rookY = 15;
                newKingX = isShort ? 10 : 6; newKingY = 15;
                newRookX = isShort ? 9 : 7; newRookY = 15;
                break;
            case "yellow":
                kingX = 0; kingY = 7;
                rookX = 0; rookY = isShort ? 11 : 4;
                newKingX = 0; newKingY = isShort ? 9 : 5;
                newRookX = 0; newRookY = isShort ? 8 : 6;
                break;
            case "blue":
                kingX = 15; kingY = 8;
                rookX = 15; rookY = isShort ? 11 : 4;
                newKingX = 15; newKingY = isShort ? 10 : 6;
                newRookX = 15; newRookY = isShort ? 9 : 7;
                break;
            default: return;
        }

        GameObject kingObj = GetPosition(kingX, kingY);
        GameObject rookObj = GetPosition(rookX, rookY);
        if (kingObj == null || rookObj == null) return;

        SetPositionEmpty(kingX, kingY);
        SetPositionEmpty(rookX, rookY);

        Chessman king = kingObj.GetComponent<Chessman>();
        Chessman rook = rookObj.GetComponent<Chessman>();

        king.SetXBoard(newKingX); king.SetYBoard(newKingY); king.SetCoords();
        rook.SetXBoard(newRookX); rook.SetYBoard(newRookY); rook.SetCoords();

        SetPosition(kingObj);
        SetPosition(rookObj);
    }


    public bool TryCastle(Chessman king, int targetX, int targetY)
    {
        string color = king.name.Split('_')[0];
        int x0 = king.GetXBoard();
        int y0 = king.GetYBoard();

        // Blanco y negro: enroque horizontal
        if ((color == "white" || color == "black") && Mathf.Abs(targetX - x0) == 2)
        {
            bool isShort = targetX > x0;
            if (CanCastle(color, isShort))
            {
                PerformCastle(color, isShort);
                return true;
            }
        }
        // Azul y amarillo: enroque vertical
        else if ((color == "blue" || color == "yellow") && Mathf.Abs(targetY - y0) == 2)
        {
            bool isShort = targetY > y0;
            if (CanCastle(color, isShort))
            {
                PerformCastle(color, isShort);
                return true;
            }
        }

        return false;
    }



}
