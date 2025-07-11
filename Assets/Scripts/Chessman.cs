using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // Variables y metodos para Enroque Largo y Corto
    private bool hasMoved = false;
    public bool HasMoved() => hasMoved;
    public void SetHasMoved(bool moved) => hasMoved = moved;

    public string GetPlayer() => player;




    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    private string player;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;
    public Sprite blue_queen, blue_knight, blue_bishop, blue_king, blue_rook, blue_pawn;
    public Sprite yellow_queen, yellow_knight, yellow_bishop, yellow_king, yellow_rook, yellow_pawn;
    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name
        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
            case "blue_queen": this.GetComponent<SpriteRenderer>().sprite = blue_queen; player = "blue"; break;
            case "blue_knight": this.GetComponent<SpriteRenderer>().sprite = blue_knight; player = "blue"; break;
            case "blue_bishop": this.GetComponent<SpriteRenderer>().sprite = blue_bishop; player = "blue"; break;
            case "blue_king": this.GetComponent<SpriteRenderer>().sprite = blue_king; player = "blue"; break;
            case "blue_rook": this.GetComponent<SpriteRenderer>().sprite = blue_rook; player = "blue"; break;
            case "blue_pawn": this.GetComponent<SpriteRenderer>().sprite = blue_pawn; player = "blue"; break;
            case "yellow_queen": this.GetComponent<SpriteRenderer>().sprite = yellow_queen; player = "yellow"; break;
            case "yellow_knight": this.GetComponent<SpriteRenderer>().sprite = yellow_knight; player = "yellow"; break;
            case "yellow_bishop": this.GetComponent<SpriteRenderer>().sprite = yellow_bishop; player = "yellow"; break;
            case "yellow_king": this.GetComponent<SpriteRenderer>().sprite = yellow_king; player = "yellow"; break;
            case "yellow_rook": this.GetComponent<SpriteRenderer>().sprite = yellow_rook; player = "yellow"; break;
            case "yellow_pawn": this.GetComponent<SpriteRenderer>().sprite = yellow_pawn; player = "yellow"; break;
        }
    }

    public void SetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp()
    {
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            //Remove all moveplates relating to previously selected piece
            DestroyMovePlates();

            //Create new MovePlates
            InitiateMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "blue_queen":
            case "yellow_queen":
                LineMovePlate(0, -1);
                LineMovePlate(-1, 0);
                LineMovePlate(-1, -1);
                LineMovePlate(0, 1);
                LineMovePlate(1, 0);
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                break;
            case "black_knight":
            case "white_knight":
            case "blue_knight":
            case "yellow_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "blue_bishop":
            case "yellow_bishop":
                LineMovePlate(-1,- 1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(1, 1);
                break;
            case "black_king":
            case "white_king":
            case "blue_king":
            case "yellow_king":
                SurroundMovePlate();
                TryCastling();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "blue_rook":
            case "yellow_rook":
                LineMovePlate(0, 1);
                LineMovePlate(1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, 0);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 2);
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 2);
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "blue_pawn":
                PawnMovePlate(xBoard-2, yBoard);
                PawnMovePlate(xBoard-1, yBoard);
                break;
            case "yellow_pawn":
                PawnMovePlate(xBoard+2, yBoard);
                PawnMovePlate(xBoard+1, yBoard);
                break;
        }
    }

    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    // Enroque Corto y Largo

    private void TryCastling()
    {
        Game sc = controller.GetComponent<Game>();

        if (hasMoved) return;

        // Enroque corto
        if (sc.CanCastle(player, true)) // true = corto
        {
            if (player == "white" || player == "black")
                MovePlateSpawn(xBoard + 2, yBoard); // Corto: derecha
            else
                MovePlateSpawn(xBoard, yBoard - 2); // Corto vertical: hacia abajo
        }

        // Enroque largo
        if (sc.CanCastle(player, false)) // false = largo
        {
            if (player == "white" || player == "black")
                MovePlateSpawn(xBoard - 2, yBoard); // Largo: izquierda
            else
                MovePlateSpawn(xBoard, yBoard + 2); // Largo vertical: hacia arriba
        }
    }


    public void MovePiece(int x, int y)
    {
        int prevX = GetXBoard();
        int prevY = GetYBoard();

        Game sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();

        // Detectar enroque si soy el rey y me muevo dos casillas
        if (this.name.Contains("_king") && (Mathf.Abs(x - prevX) == 2 || Mathf.Abs(y - prevY) == 2))
        {
            if (sc.TryCastle(this, x, y)) return; // El enroque se realiza y se termina
        }

        // Lógica normal de movimiento
        sc.SetPositionEmpty(prevX, prevY);

        SetXBoard(x);
        SetYBoard(y);
        SetCoords();

        sc.SetPosition(this.gameObject);
        SetHasMoved(true);

        sc.NextTurn();
    }






}
