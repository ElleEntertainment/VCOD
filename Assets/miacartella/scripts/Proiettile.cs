using UnityEngine;
using System.Collections;

public class Proiettile : MonoBehaviour {

    public GameObject proiettile = null;
    float tempoTraProiettili = 0.7f; //tempo che deve trascorrere tra 2 spari
    float tempoProssimoSparo = 0;
    float forzaSparo = 50;
    float attesa = 3f;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //                             Orario attuale > tempo previsto per il prossimo sparo
        if (Input.GetKey(KeyCode.I) && Time.time > tempoProssimoSparo)
        {
            tempoProssimoSparo = Time.time + tempoTraProiettili;

            ((AudioSource) GetComponent("AudioSource")).Play();

            //Serve il cast perchè il metodo restituisce un generico Object
            //                                                     posizione doppietta
            GameObject clone = (GameObject)Instantiate(proiettile, transform.position, transform.rotation);
            //Debug.Log("x: " + transform.position.x + "y: " + transform.position.y + "z: " + (transform.position.z + 180));
            //Debug.Log("x: " + transform.position.x + "y: " + transform.position.y + "z: " + transform.position.z);
            //applico una forza per far andare in avanti e velocemente il proiettile
            //il RELATIVE fa considerare il vettore di forza relativo all'oggetto, non usa le coordinate globali
            //in questo modo però se metto a forzaSparo un valore alto, il motore di unity potrebbe non capire che c'è stata una collisione
            clone.rigidbody.AddRelativeForce(Vector3.forward * forzaSparo /* 1 * variabile*/ , ForceMode.Impulse);

            Destroy(clone, attesa);
			Debug.Log("Sparato e cancellato");

        }
	
	}
}
