using UnityEngine;
using System.Collections;

public class Proiettile : MonoBehaviour {

    public GameObject proiettile = null;
    GameObject telecamera;
    float tempoTraProiettili = 0.7f; //tempo che deve trascorrere tra 2 spari
    float tempoProssimoSparo = 0;
    float forzaSparo = 50;
    float attesa = 3f;
	// Use this for initialization
	void Start () {
        telecamera = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
        //                             Orario attuale > tempo previsto per il prossimo sparo
        //Debug.Log("update proiettile");
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > tempoProssimoSparo)
        {
            Debug.Log("SPARO");
            tempoProssimoSparo = Time.time + tempoTraProiettili;

            ((AudioSource) GetComponent("AudioSource")).Play();

            //Serve il cast perchè il metodo restituisce un generico Object
            //                                                     posizione doppietta
            GameObject clone = (GameObject)Instantiate(proiettile, transform.position, telecamera.transform.rotation);
            //applico una forza per far andare in avanti e velocemente il proiettile
            //il RELATIVE fa considerare il vettore di forza relativo all'oggetto, non usa le coordinate globali
            //in questo modo però se metto a forzaSparo un valore alto, il motore di unity potrebbe non capire che c'è stata una collisione
            clone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * forzaSparo /* 1 * variabile*/ , ForceMode.Impulse);
            
            Destroy(clone, attesa);

        }
	
	}
}
