using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandera : MonoBehaviour
{
    //Relacionado a la captura de la bandera
    public List<GameObject> gameObjectsDentro = new List<GameObject>(); //Almacenamos los players dentro del radio
    public bool capturando; //Para saber si la bandera está siendo capturada (tal vez no se necesita)
    public float velocidadDeCaptura; //Para establecer qué tan rápido se captura la bandera
    public float porcentajeCapturado; //Para ir cambiando el color de la bandera a medida que se va capturando
    public int porcentajeAMostrar; //Porcentaje en INT para mostrar
    public GameObject owner;

    //Elementos para cambiar el color de la bandera
    public List<Renderer> GOsParaColorear = new List<Renderer>();

    // Start is called before the first frame update
    void Start()
    {
        capturando = false;
    }
    private void OnTriggerStay(Collider other)
    {
        //Con esto eliminamos de la lista un tanque que haya sido destruido dentro del radio de captura
        foreach (GameObject go in gameObjectsDentro)
        {
            if (go == null) { gameObjectsDentro.Remove(go); }
        }

        if (other.tag == "Tanque")
        {
            if (!gameObjectsDentro.Contains(other.gameObject))
            {
                //Detectamos quién está dentro del trigger y lo añadimos
                gameObjectsDentro.Add(other.gameObject);
            }
        }
        //foreach (GameObject go in gameObjectsDentro)
        //{
        //    if (go == null)
        //    {
        //        gameObjectsDentro.Remove(go);
        //    }
        //}
    }
    private void OnTriggerExit(Collider other)
    {
        if (gameObjectsDentro.Contains(other.gameObject))
        {
            //Dectamos quién sale del trigger
            gameObjectsDentro.Remove(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObjectsDentro.Count == 1)
        {
            capturando = true;
            owner = gameObjectsDentro[0];
            if (porcentajeCapturado < 1)
            {
                porcentajeCapturado += velocidadDeCaptura * Time.deltaTime;
            }
            else
            {
                porcentajeCapturado = 1;
            }
            porcentajeAMostrar = Mathf.FloorToInt(porcentajeCapturado);

            foreach (Renderer go in GOsParaColorear)
            {
                if (owner.name == "TankJ1") //Si el dueño de la bandera es TankJ1, vamos pintando la bandera de rojo
                {
                    go.material.color = new Color(1, 1 - porcentajeCapturado, 1 - porcentajeCapturado);
                }
                else //De lo contrario, pintamos la bandera de Azul
                {
                    go.material.color = new Color(1 - porcentajeCapturado, 1 - porcentajeCapturado, 1);
                }
            }
        }
        else if (gameObjectsDentro.Count == 2)
        {
            capturando = false;
            owner = null;
            porcentajeCapturado = 0;
            foreach (Renderer go in GOsParaColorear)
            {
                go.material.color = new Color(0.3f, 0.3f, 0.3f);
            }
        }
        else { capturando = false; }


    }

    private void FixedUpdate()
    {

    }
}
