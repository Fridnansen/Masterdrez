using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Escena : MonoBehaviour
{
    public int numeroEscena;

    public void iniciar(int numeroEscena)
    {
        SceneManager.LoadScene(numeroEscena);
    }
}
