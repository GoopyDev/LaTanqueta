using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private readonly System.Random random = new System.Random(); //Con esto creamos un generador de numeros random
    public float duracion;
    private float tiempo;
    public float rotacion;
    private int bonificacion;
    public Material bonusHP, bonusFire, bonusDiamond, bonusMachinegun, bonusVelocidad;
    public Renderer bonusImg;
    private Rigidbody rb;

    private Vector3 ElegirPosicion()
    {
        bool posicionLibre = false; //Boolean que será True cuando encontremos un lugar vacío
        Vector3 posicionElegida = Vector3.zero; //Aqui almacenaremos la ubicacion libre para devolverla
        int counter = 0; //Medida de seguridad, por si fallara el bucle While
        //System.Random random = new System.Random(); //Con esto creamos un generador de numeros random

        while (!posicionLibre) //Mientras no se halle una posicion vacia:
        {
            int a = random.Next(-18, 19);   //Generamos nro random entre -18 y 18 para la coordenada X
            int b = random.Next(-18, 19);   //Generamos nro random entre -18 y 18 para la coordenada Z
                                            //Nota: la coordenada Y siempre será 0
            
            //IMPORTANTE, en el siguiente IF el valor Y es 0.6f para no detectar el suelo. Luego se cambia a 0 para ubicar correctamente el PowerUp
            //Debug.Log("CheckSphere" + Physics.CheckSphere(posicionparcial, 0.5f));
            if (!Physics.CheckSphere(new Vector3(a, 0.6f, b), 0.8f)) //Physics.CheckSphere evalua si hay un Collider en la posicion, en un radio de 0.8f
            {
                posicionElegida = new Vector3(a, 0, b); //Aquí asignamos los valores, ya que tendríamos una ubicación libre
                posicionLibre = true; //Con esto damos cierre al bucle While
                //Debug.Log("Tengo una posicion libre!");
            }

            counter++;
            if (counter > 100)
            {
                //Debug.Log("Llegué a 100!");
                break;
            }
            //Debug.Log("Contador: " + counter + " Posicion: " + a + " " + b);
        }
        return posicionElegida;
    }

    private void ElegirPowerUp()
    {
        //Creamos dos arrays, uno con texturas y otro con cadenas
        Material[] materiales = { bonusHP, bonusFire, bonusDiamond, bonusMachinegun, bonusVelocidad };
        //string[] bonificaciones = { "+1 HP", "Balas Fire", "Balas Diamond", "Balas Machinegun", "+20 Velocidad" };
        
        //System.Random random = new System.Random(); //Con esto creamos un generador de numeros random
        int nro_random = random.Next(0, 5);         //Generamos un numero random y lo almacenamos para usarlo luego

        bonusImg.material = materiales[nro_random];
        bonificacion = nro_random;

        //Debug.Log(materiales[nro_random]);
        //Debug.Log("Otorgo: " + bonificacion);
    }

    // Start is called before the first frame update
    void Start()
    {
        ElegirPowerUp();
        Vector3 posicion = ElegirPosicion();
        rb = GetComponent<Rigidbody>();
        transform.position = posicion;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo > duracion)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        //Rotacion del PowerUp
        rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, rotacion * Time.deltaTime, 0)));
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Me chocó un " + other.gameObject.tag);
        if (other.gameObject.tag == "Tanque")
        {
            //Debug.Log("Otorgo " + bonificacion + " a " + other.gameObject.name);
            other.gameObject.GetComponent<TankController>().ObtenerPowerUp(bonificacion); //Pasamos el nro de PowerUp al tanque
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "FuegoAliado")
        {
            Debug.Log("Maldita bala!!");
        }
    }
}
