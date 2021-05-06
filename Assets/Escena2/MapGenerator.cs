using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Prefabs")]

    public GameObject prefabPiso;
    public GameObject prefabPared;
    public GameObject prefabParedLimite;
    public GameObject prefabLuz;
    public GameObject Jugador1;
    public GameObject Jugador2;
    public GameObject bandera;

    [Header("Tamaño del mapa")]
    public int ancho;
    public int profundidad;
    public float alturaPared;

    [Header("Objetos a crear")]
    public int[] probabilidades;
    private float nroAleatorio;

    private void Awake()
    {


        //Creamos el piso según las medidas ingresadas
        GameObject piso = Instantiate(prefabPiso);
        piso.transform.localScale = new Vector3(ancho, 1, profundidad);
        piso.transform.position = new Vector3(((ancho * 10) / 2) - 0.5f, 0, ((profundidad * 10) / 2) - 0.5f);


        //Creamos las paredes indestructibles que rodean el mapa
        //Pared #1 Inferior
        GameObject paredLimite1 = Instantiate(prefabParedLimite);
        paredLimite1.transform.localScale = new Vector3((ancho * 10) + 2, 1, 1);
        paredLimite1.transform.position = new Vector3(((ancho * 10) / 2) - 0.5f, 0.5f, -1);
        //Pared #2 Izquierda
        GameObject paredLimite2 = Instantiate(prefabParedLimite);
        paredLimite2.transform.localScale = new Vector3(1, 1, (profundidad * 10) + 2);
        paredLimite2.transform.position = new Vector3(-1f, 0.5f, ((profundidad * 10) / 2) - 0.5f);
        //Pared #3 Superior
        GameObject paredLimite3 = Instantiate(prefabParedLimite);
        paredLimite3.transform.localScale = new Vector3((ancho * 10) + 2, 1, 1);
        paredLimite3.transform.position = new Vector3(((ancho * 10) / 2) - 0.5f, 0.5f, (profundidad * 10));
        //Pared #4 Derecha
        GameObject paredLimite4 = Instantiate(prefabParedLimite);
        paredLimite4.transform.localScale = new Vector3(1, 1, (profundidad * 10) + 2);
        paredLimite4.transform.position = new Vector3((ancho * 10), 0.5f, ((profundidad * 10) / 2) - 0.5f);

        //Creamos el bucle para recorrer las posiciones e ir llenandolas con objetos o dejarlas vacías
        for (int z = 0; z < (profundidad * 10); z++) //Inicio con las Z, para ir llenando por filas, y no por columnas (por gusto)
        {
            for (int x = 0; x < (ancho * 10); x++) //En cada fila, recorremos todas las posiciones en X
            {
                //***Condiciones para instanciar los jugadores***//
                // Si la posición es (Z0, X0) instanciamos el Tanque1
                if ((z == 0) && (x == 0))
                {
                    GameObject player1 = Instantiate(Jugador1);
                    player1.transform.position = new Vector3(0, 0, 0);
                    player1.name = "TankJ1";
                }
                // Si la posición es (ZMax-1, XMax-1), es decir la última posicion de la matriz, instanciamos el Tanque1
                else if ((z == (profundidad * 10) - 1) && (x == (ancho * 10) - 1))
                {
                    GameObject player2 = Instantiate(Jugador2);
                    player2.transform.position = new Vector3((ancho * 10) - 1, 0, (profundidad * 10) - 1);
                    player2.name = "TankJ2";
                }

                //Instanciamos la bandera en el centro del mapa
                else if ((z == (profundidad * 10) / 2) && (x == (ancho * 10) / 2))
                {
                    bandera = Instantiate(bandera);
                    bandera.transform.position = new Vector3(x, 0, z);
                }

                //***Instancia de objetos aleatorios con probabilidades***//
                else
                {
                    nroAleatorio = Random.Range(0, 100);
                    //Probabilidades para no instanciar nada
                    if (nroAleatorio < probabilidades[0])
                    {
                        //No hacer nada, por ahora
                    }
                    //Probabilidades para instanciar una pared
                    else if (nroAleatorio > probabilidades[0])
                    {
                        alturaPared = Random.Range(-0.5f, 1);
                        GameObject pared = Instantiate(prefabPared);
                        pared.transform.position = new Vector3(x, alturaPared, z);
                    }
                }

                Debug.Log("Estoy en el punto " + z + " : " + x);
            }
        }

        ////Creamos luces para iluminar todo el mapa (En principio, una cada 10 unidades)
        //for (int i = 0; i < ancho + 1; i++)
        //{
        //    for (int j = 0; j < profundidad + 1; j++)
        //    {
        //        GameObject luz = Instantiate(prefabLuz);
        //        luz.transform.position = new Vector3(i * 10, 5, j * 10);
        //    }
        //}
    }
}
