using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {
    public float precision;
    public int vida = 100;
    private bool estaVivo = true;
    public string mensajeVictoria = "¡He vencido!";
    public char inicialEscuadron = 'U';
    //Mele es "a hostia limpia"
    public int danyoMele = 1;
    public float tiempoEntreAtaques;

    //OPCION 1, con GameObject
    public GameObject prefabSangreGO;
    //OPCION 2, con ParticleSystem
    public ParticleSystem prefabSangrePS;

    public GameObject brazoDerecho;
    public GameObject brazoIzquierdo;

    private Rigidbody cmpRigidbody;
    private AudioClip dolor;
    // Componentes: RigidBody, AudioSource, Collider .
    // private Material materialPersonaje.
    // private Texture / Texture2D / Texture3D.
    public WeaponBehaviour weaponBehaviour;
    public SoldierBehaviour enemigo;
    public GestorPuntuacionBehaviour gp;

    // Use this for initialization.
	void Start ()
    {
        precision = Random.value;
        tiempoEntreAtaques = 0.1f;
        cmpRigidbody = GetComponent<Rigidbody>();
        ArrancarAtaquesRecurrentes();

        gp = GameObject.Find("GestorPuntuacion").GetComponent<GestorPuntuacionBehaviour>();
        
    }

    // Update is called once per frame.
    void Update () {
        //weaponBehaviour.DispararA(enemigo);
        if (!this.estaVivo)
        {
            cmpRigidbody.transform.Rotate(new Vector3(-0.01f, 0, 0));
        }
        if (!enemigo.estaVivo)
        {
            brazoDerecho.transform.Rotate(new Vector3(0, -1f, 0));
            brazoIzquierdo.transform.Rotate(new Vector3(0, -2f, 0));
        }
	}

    private void AtacarCuerpoACuerpo() {
        DecirMensaje("Voy a atacar a " + enemigo.name);
        float probabilidadImpacto = Random.value;
        bool heAcertado = probabilidadImpacto < this.precision;
        if (enemigo.estaVivo && heAcertado)
        {
            this.enemigo.RecibirAtaque(danyoMele);
            if (!enemigo.estaVivo)
            {
                DecirMensaje(this.mensajeVictoria);
                CancelInvoke("AtacarCuerpoACuerpo");
            }
        }
        else if (!heAcertado)
        {
            DecirMensaje("Arghh, he fallado");
        }
    }

    public void RecibirAtaque(int danyoRecibido)
    {
        this.vida -= danyoRecibido;
        DecirMensaje("Me han atacado y me queda " + this.vida + " de vida");
        Sangrar();

        if (this.vida <= 0)
        {
            Morir();
        }
    }

    private void Sangrar()
    {
        //OPCION 1
        GameObject nuevaSangreGO = Instantiate(prefabSangreGO);
        nuevaSangreGO.transform.position = this.transform.position + Vector3.up;
    }

    private void Morir()
    {
        this.estaVivo = false;
        DetenerAtaquesRecurrentes();
        //this.gameObject.SetActive(false);
        DecirMensaje("Arghh, he muerto");
        gp.SaveScore();
    }

    private void ArrancarAtaquesRecurrentes()
    {
        InvokeRepeating("AtacarCuerpoACuerpo", tiempoEntreAtaques, tiempoEntreAtaques);
    }

    private void DetenerAtaquesRecurrentes()
    {
        this.CancelInvoke("AtacarCuerpoACuerpo");
    }

    public void DecirMensaje(string mensaje)
    {
        Debug.Log(this.name + " dice: " + mensaje);
    }

}
