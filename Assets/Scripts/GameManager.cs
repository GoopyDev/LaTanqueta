using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float tiempo;
    public float powerupTiempo;
    public GameObject powerUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo > powerupTiempo)
        {
            Instantiate(powerUp);
            tiempo = 0; //Reseteamos la variable tiempo
        }
    }
}
