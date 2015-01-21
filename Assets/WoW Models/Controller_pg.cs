using UnityEngine;
using System.Collections;

public class Controller_pg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Sistema di Rotazione (non ce n'è bisogno per ora)
		/*if (Input.GetKey (KeyCode.LeftArrow)) 
			    transform.Rotate (Vector3.up, -1); //-1 sono i gradi di rotazione
		else
			if (Input.GetKey (KeyCode.RightArrow))
					transform.Rotate (Vector3.up, 1);
			else
				if (Input.GetKey (KeyCode.UpArrow))
					transform.Rotate (Vector3.left, 1);
				else
					if (Input.GetKey (KeyCode.DownArrow))
						transform.Rotate (Vector3.left, -1);*/
		// FINE SISTEMA DI ROTAZIONE

		//Usiamo i tasti per muoverci
		if (Input.GetKey (KeyCode.W))
			transform.Translate (Vector3.right/30, Space.Self); // diviso 30 per ridurre la velocità
		if (Input.GetKey (KeyCode.S))
			transform.Translate (Vector3.back, Space.Self);
		if (Input.GetKey (KeyCode.A))
			transform.Translate (Vector3.forward, Space.Self);
		if (Input.GetKey (KeyCode.D))
			transform.Translate (Vector3.back, Space.Self);
		//Fine sistema di movimento semplice
		//Ci sono gli if senza gli else per dare la possibilità di muoversi in più direzioni in una volta
	}
}
