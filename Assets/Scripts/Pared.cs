using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pared : MonoBehaviour
{
    public int vidas;

    public void RestarVidas(int damage)
    {
        vidas -= damage;
    }
    // Start is called before the first frame update
    void Start()
    {
        vidas = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (vidas <= 0)
            Destroy(gameObject);
    }
}
