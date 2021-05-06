using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    //GameObjects referenciados
    public GameObject luces;    //Luces del tanque
    public GameObject turret;   //GameObject Torreta
    private Rigidbody rbChasis; //Rigidbody del tanque
    private Rigidbody rbTurret; //Rigidbody de la torreta
    public Transform miCamara;  //La camara del tanque, para poder desasociarla en caso de muerte

    //Movimientos del tanque
    public float velocidad;
    public float velocidadMax;
    public float velocidad_rotacion;
    public float velocidad_turret;
    public string axisHorizontal, axisVertical, axisTurret, fire1, lights;

    //Caracteristicas del tanque
    public int vidas; //La vida del tanque

    //Variables relacionadas con la bala
    public GameObject BalaDeCanon;     //GO que usamos para instanciar la bala
    public Transform spawnBullet;      //Brinda la posición donde se instanciarán las balas
    public bool disparado;             //Para saber si se disparó y evitar balas duplicadas, o limitarlas
    private bool enfriando;            //Para saber si estamos recargando
    public float contadorEnfriamiento; //Para contar el tiempo transcurrido de enfriamiento
    public float tiempoDeEnfriamiento; //Cuánto deseamos que demore el enfriamiento
    public bool balasFire, balasDiamond, balasMachineGun, balaEspecial; //Balas especiales
    public float contadorBalaEspecial; //Para contar el tiempo de los PowerUps de balas
    public float contadorMachineGun;   //Para contar el tiempo entre los disparos de MachineGun
    public float intervaloBalasMG;     //Para ajustar el intervalo de tiempo entre cada bala MG
    public int balasPorRafaga;         //Define cuántas balas se dispararán en cada ráfaga
    public int balasMGRestantes;       //La cantidad de balas que faltan disparar con el modo MachineGun
    public int duracionBalaEspecial;   //Duracion de los PowerUps de balas

    //Para la muerte del tanque
    public GameObject deathScreen;     //Activaremos este filtro gris si el tanque muere

    private void ResetearBalaEspecial()
    {
        balasFire = false;
        balasDiamond = false;
        balasMachineGun = false;
        balaEspecial = false;
        balasMGRestantes = balasPorRafaga;
        disparado = false;
    }
    public void ObtenerPowerUp(int bonificacion)
    {
        //Una bala especial se utiliza
        balaEspecial = true;
        //Apagamos los efectos anteriores de otras balas (excepto que sea HP o Velocidad)
        if ((bonificacion != 0) || (bonificacion != 4))
        {
            balasFire = false;
            balasDiamond = false;
            balasMachineGun = false;
        }
        //Aplicamos bonificación segun el nro obtenido
        switch (bonificacion)
        {
            case 0: // Otorgar +1 HP
                if (vidas < 3) { vidas++; }
                break;
            case 1: // Otorgar Balas Fire
                balasFire = true;
                break;
            case 2: // Otorgar Balas Diamond
                balasDiamond = true;
                break;
            case 3: // Otorgar Balas MachineGun
                balasMachineGun = true;
                break;
            default:// Otorgar Velocidad
                velocidad += 20;
                if (velocidad > velocidadMax) { velocidad = velocidadMax; }
                break;
        }
    }

    private void Disparar()
    {
        GameObject bala = Instantiate(BalaDeCanon);
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        Bala balaComponent = bala.GetComponent<Bala>();
        rb.transform.position = spawnBullet.position;
        rb.transform.rotation = spawnBullet.rotation;
        balaComponent.miTanque = gameObject;

        //***Seccion para las balas especiales***//
        if (balasFire)           //Estas balas tienen un daño de 3 puntos sobre las paredes.
        {
            balaComponent.damage = 3;
            enfriando = true;    //Activamos el enfriamiento para luego poder volver a disparar
            //disparado = false; //Habilitamos volver a disparar con esto
        }
        else if (balasDiamond)   //Estas balas tienen daño de 3 puntos sobre las paredes y además...
        {                        //no se destruyen con ellas, de modo que las atraviesan
            balaComponent.balaDiamond = true;
            bala.GetComponent<SphereCollider>().isTrigger = true;
            enfriando = true;    //Activamos el enfriamiento para luego poder volver a disparar
            //disparado = false; //Habilitamos volver a disparar con esto
        }
        else if (balasMachineGun)//Estas balas se disparan en ráfagas
        { balasMGRestantes--; }  //Se restan balas hasta que se termine la ráfaga

        else                     //Para las balas comunes
        { enfriando = true; }    //Activamos el enfriamiento para luego poder volver a disparar
            //disparado = false; //Habilitamos volver a disparar con esto
    }

    private void Awake()
    {
        vidas = 3;
        balasMGRestantes = balasPorRafaga;
        rbChasis = GetComponent<Rigidbody>();
        rbTurret = turret.GetComponent<Rigidbody>();
        disparado = false;
        enfriando = false;
        ResetearBalaEspecial();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        //Evaluamos si tenemos vidas restantes
        if (vidas <= 0)
        {
            Debug.Log("Mataron al tanque " + gameObject.name);
            deathScreen.SetActive(true);
            miCamara.parent = null;
            Destroy(gameObject);
        }

        //Si obtuvimos Balas MachineGun
        if (balasMachineGun)
        {
            contadorMachineGun += Time.deltaTime; //Vamos contando el tiempo hasta llegar al deseado entre cada bala
            //Si había disparado, chequeamos si está activo el modo MachineGun y disparamos sin un evento de Input
            if (disparado && (contadorMachineGun > intervaloBalasMG))  //Esto no se ejecutará si se acaban las balas disponibles en modo MachineGun...
            { Disparar(); } //...ya que disparado se vuelve falso al disparar X cantidad de balas
            if (contadorMachineGun > intervaloBalasMG)
            {
                contadorMachineGun = 0;                //Reseteamos el contador para la siguiente bala de la rafaga
                if (balasMGRestantes == 0)
                {
                    enfriando = true;                  //Activamos el enfriamiento para luego poder volver a disparar
                    balasMGRestantes = balasPorRafaga; //Restablecemos las balas restantes
                }
            } 
        }

        // Chequeamos si se presiona una tecla de disparo para lanzar una bala
        if (Input.GetButtonDown(fire1) && !disparado)
        {
            disparado = true;       //Servirá con las balas MachineGun. Para el resto se resetea ni bien se usa
            contadorMachineGun = 0; //Reinicio el contador para el espacio entre balas en modo MachineGun
            Disparar();             //Ejecuto el disparo y allí se decide cómo se comportan las balas según el modo actual
        }

        //Si durante el juego se activan las balas especiales iniciamos un contador
        if (balaEspecial)
        {
            contadorBalaEspecial += Time.deltaTime;
            if (contadorBalaEspecial > duracionBalaEspecial)
            {
                ResetearBalaEspecial();
                contadorBalaEspecial = 0;
            }
        }

        //Chequeamos si recientemente se ejecutó un disparo y aguardamos el tiempo de enfriamiento
        if (enfriando)
        {
            contadorEnfriamiento += Time.deltaTime;
            if (contadorEnfriamiento > tiempoDeEnfriamiento)
            {
                disparado = false;
                contadorEnfriamiento = 0;
                enfriando = false;
            }
        }

        // Si se presiona L Se apagan las luces (Cambiar para el Player 2, que sean independientes)
        if (Input.GetButtonDown(lights))
        {
            Debug.Log("Alterno luces!");
            luces.SetActive(!luces.gameObject.activeInHierarchy);
        }
    }

    void FixedUpdate()
    {
        //Movimiento hacia adelante/atras
        float movVertical = Input.GetAxis(axisVertical); //Obtenemos el Axis Vertical
        if (movVertical >  0.20f) { movVertical = 0.20f; } //Limito el Axix Vertical para menos aceleración
        if (movVertical < -0.20f) { movVertical = -0.20f; } //Limito el Axix Vertical para menos aceleración
        rbChasis.AddRelativeForce(new Vector3(0, 0, movVertical) * velocidad * Time.deltaTime, ForceMode.Impulse);

        //Rotacion del tanque
        float movHorizontal = Input.GetAxis(axisHorizontal); //Obtenemos el Axis Horizontal
        Quaternion rot = Quaternion.Euler(new Vector3(0, movHorizontal, 0) * velocidad_rotacion * Time.deltaTime);
        rbChasis.MoveRotation(rbChasis.rotation * rot);

        //Rotacion de la torreta
        float movTurret = Input.GetAxis(axisTurret); //Obtenemos el Axis Turret que creamos para la torreta
        Quaternion rotTorreta = Quaternion.Euler(new Vector3(0, movTurret, 0) * velocidad_turret * Time.deltaTime);
        rbTurret.MoveRotation(rbTurret.rotation * rotTorreta);

        //Debug.Log("Movimiento Horizontal: " + movHorizontal + " Movimiento Vertical: " + movVertical + " Tiempo: " + Time.deltaTime);
    }
}
