using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody rb;
    //private Vector3 inercia;
    public float velocidad;
    private float tiempo;
    public float duración;
    public int damage;
    public GameObject miTanque;
    public bool balaDiamond;
    
    private void Awake()
    {
        //miPadre
        rb = GetComponent<Rigidbody>();
        damage = 1;
        balaDiamond = false;
        //diamond = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión: " + collision.gameObject);

        if (collision.gameObject.tag == "Tanque" && collision.gameObject != miTanque)
        {
            Debug.Log("Choque con un tanque! " + collision.gameObject.name);
            collision.gameObject.GetComponent<TankController>().vidas -= 1;
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Pared")
        {
            collision.gameObject.GetComponent<Pared>().vidas -= damage;
            if (!balaDiamond)
            { Destroy(gameObject); } //Si es BalaDiamond, no la destruimos con las paredes.
        }

        if (collision.gameObject.tag == "Indestructible")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "PowerUp")
        {
            Debug.Log("Rocé un PowerUp");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pared")
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Indestructible")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tiempo += Time.deltaTime;
        if (tiempo > 5)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //inercia = rb.velocity;
        //rb.AddRelativeForce((Vector3.forward + new Vector3 (0,0,inercia.z)) * Velocidad * Time.deltaTime, ForceMode.Acceleration);
        rb.AddForce(rb.transform.forward * velocidad * Time.deltaTime, ForceMode.Impulse);
    }
}
