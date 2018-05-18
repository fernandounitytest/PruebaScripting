using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour {
    public AudioClip audioRecarga;
    public AudioClip audioDisparo;

    public int capacidadCargador = 10;
    private int balasEnCargador;

    public float balasPorSegundo = 1.0f;
    public float precision = 0.9f;

    public int danyo = 1;

    bool estoyRecargando = false;
    public float tiempoRecarga = 0.5f;

    public SoldierBehaviour soldadoPortador;

    void Start () {
        this.balasEnCargador = this.capacidadCargador;
	}

    public void DispararA(SoldierBehaviour enemigo)
    {
        if(balasEnCargador>0 && !estoyRecargando)
        {
            soldadoPortador.DecirMensaje("Disparando a " + enemigo.name + " con " + this.name);
            soldadoPortador.DecirMensaje("Me quedan " + balasEnCargador + " balas");
            balasEnCargador -= 1;
            bool disparoAcierta = Random.value < precision;
            if (disparoAcierta)
            {
                enemigo.RecibirAtaque(danyo);
            }
            else
            {
                soldadoPortador.DecirMensaje("Jarl, he fallado");
            }
        }
        else if (balasEnCargador == 0 && !estoyRecargando)
        {
            Recargar();
        }
    }

    public void Recargar()
    {
        soldadoPortador.DecirMensaje("Iniciando recarga de " + this.name);
        estoyRecargando = true;
        AudioSource.PlayClipAtPoint(audioRecarga, this.transform.position);
        Invoke("FinalizarRecarga", tiempoRecarga);
    }
    private void FinalizarRecarga()
    {
        soldadoPortador.DecirMensaje("Finalizando recarga " + this.name);
        balasEnCargador = capacidadCargador;
        estoyRecargando = false;
    }
   
}
