using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canyon : MonoBehaviour
{
    //Relacionado a los objetos e instancias
    public GameObject bala_canyon;
    public Transform bulletSpawn;

    //Relacionado al tiempo
    private bool reproduciendo;

    private void Disparar()
    {
        GameObject canyonazo = Instantiate(bala_canyon);
        Rigidbody rb_bala = canyonazo.GetComponent<Rigidbody>();
        rb_bala.transform.position = bulletSpawn.position;
        rb_bala.transform.rotation = bulletSpawn.rotation;
    }

    private void Pausar()
    {
        reproduciendo = !reproduciendo;
        Time.timeScale = System.Convert.ToInt32(reproduciendo);
    }



    // Start is called before the first frame update
    void Start()
    {
        reproduciendo = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
    }
}
