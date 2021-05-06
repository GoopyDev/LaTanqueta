using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neo : MonoBehaviour
{
    public Rigidbody rb;
    public bool esquivando;
    private Transform desde;
    private Transform hacia;

    public float velocidadTiempo;

    public void Ralentizar()
    {
        velocidadTiempo = 0.2f;
    }
    public void Normalizar()
    {
        velocidadTiempo = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        esquivando = false;
        desde = transform;
        //hacia.position.x = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Ralentizar();
        }
        if (Input.GetKey(KeyCode.W))
        {

            //rb.rotation = new Quaternion
        }
    }
}
