using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAuto : MonoBehaviour
{
    private float tiempo;
    public float powerupTiempo;
    public GameObject powerUp;
    public GameObject mapaDelPowerUp;

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
            GameObject powerUpInstancia = Instantiate(powerUp);
            powerUpInstancia.GetComponent<PowerUpAuto>().anchoMapa = mapaDelPowerUp.GetComponent<MapGenerator>().ancho;
            powerUpInstancia.GetComponent<PowerUpAuto>().profundidadMapa = mapaDelPowerUp.GetComponent<MapGenerator>().profundidad;
            Debug.Log("PowerUpMAPA" + powerUpInstancia.GetComponent<PowerUpAuto>().anchoMapa);
            tiempo = 0; //Reseteamos la variable tiempo
        }
    }
}
