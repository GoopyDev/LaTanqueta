using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;

    //Para reproducir sonidos del juego
    public AudioSource Musica;
    public AudioSource SFX;

    public Dictionary<string, int> higscores;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        higscores.Add("hs_j1", PlayerPrefs.GetInt("hs_j1"));
        Debug.Log("Cargué el siguiente valor: " + PlayerPrefs.GetFloat("hs_j1"));
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("hs_j1", 12000);
    }
}