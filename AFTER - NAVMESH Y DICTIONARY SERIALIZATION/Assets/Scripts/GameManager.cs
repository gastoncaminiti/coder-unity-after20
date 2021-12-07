using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //SCORE
    [SerializeField] private int scoreInstanciado;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreInstanciado = 0;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
      
    }

    private void GameOver()
    {
        Debug.Log("EL JUEGO TERMINO");
        scoreInstanciado = 0;
    }

    public static void addScore()
    {
        instance.scoreInstanciado += 1;
    }

    public static int GetScore()
    {
        return instance.scoreInstanciado;
    }
}
