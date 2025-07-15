using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovePlate : MonoBehaviour
{
    private static AudioClip moveSound;
    private static AudioClip captureSound;
    private static AudioSource audioSource;
    private static AudioClip checkmateSound;


    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }

        // Solo inicializar una vez
        if(audioSource == null)
{
            GameObject audioObject = new GameObject("MoveAudio");
            audioSource = audioObject.AddComponent<AudioSource>();
            moveSound = Resources.Load<AudioClip>("Sounds/move_piece");
            captureSound = Resources.Load<AudioClip>("Sounds/capture_piece");
            checkmateSound = Resources.Load<AudioClip>("Sounds/gameover");
        }

    }


    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Destroy the victim Chesspiece
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king")
            {
                PlayCheckmateSound();
                controller.GetComponent<Game>().Winner("La pareja: Amarilla y Azul");
            }
            else if (cp.name == "black_king")
            {
                PlayCheckmateSound();
                controller.GetComponent<Game>().Winner("La pareja: Amarilla y Azul");
            }
            else if (cp.name == "yellow_king")
            {
                PlayCheckmateSound();
                controller.GetComponent<Game>().Winner("La pareja: Blanca y Negra");
            }
            else if (cp.name == "blue_king")
            {
                PlayCheckmateSound();
                controller.GetComponent<Game>().Winner("La pareja: Blanca y Negra");
            }

            Destroy(cp);
        }

        //Set the Chesspiece's original location to be empty
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(), 
            reference.GetComponent<Chessman>().GetYBoard());

        //Move reference chess piece to this position
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        //Update the matrix
        controller.GetComponent<Game>().SetPosition(reference);

        //Switch Current Player
        controller.GetComponent<Game>().NextTurn();

        // Reproducir sonido
        // Reproducir sonido según tipo de jugada
        if (audioSource != null)
        {
            if (attack && captureSound != null)
                audioSource.PlayOneShot(captureSound);
            else if (!attack && moveSound != null)
                audioSource.PlayOneShot(moveSound);
        }


        //Destroy the move plates including self
        reference.GetComponent<Chessman>().DestroyMovePlates();

        // para enroque
        reference.GetComponent<Chessman>().SetHasMoved(true);


    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }

    private void PlayCheckmateSound()
    {
        if (audioSource != null && checkmateSound != null)
        {
            audioSource.PlayOneShot(checkmateSound);
        }
    }



}
