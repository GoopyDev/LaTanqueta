using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanyonBala : MonoBehaviour
{
    //public float timeScale;
    public float fuerza;
    private Rigidbody rb;
    public float interpolation;
    public float tiempoTranscurrido;


    private void RestaurarTiempo()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private void DestruirBala()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        tiempoTranscurrido = 0;
        rb = GetComponent<Rigidbody>();
        Invoke("RestaurarTiempo", 1.6f);
        Invoke("DestruirBala", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoTranscurrido += Time.deltaTime;
        Debug.Log(interpolation * tiempoTranscurrido);
        if (tiempoTranscurrido < 1.5f)
        {
            Time.timeScale = Mathf.Lerp(1, 0.1f, interpolation * tiempoTranscurrido);
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

    }

    private void FixedUpdate()
    {
        rb.transform.Translate(new Vector3(0, 1, 1) * fuerza * Time.deltaTime);
    }
}
