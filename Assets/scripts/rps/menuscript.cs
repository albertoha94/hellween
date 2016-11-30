using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class menuscript : MonoBehaviour {

	/**
	 * Regresa al menu de la app.
	 */
	public void back(){
		SceneManager.LoadScene ("");
	}

	/**
	 * Reinicia el juego.
	 */
	public void reset(){
	}

	/**
	 * Inicia el juego
	 */
	public void play(){

		//-- Escondemos el boton.
		GameObject pbutton = GameObject.Find("b_play");
		pbutton.GetComponent<Animator> ().enabled = true;
		pbutton.SetActive (false);

		//-- Iniciamos el juego.
		GameObject p = GameObject.Find ("rps_engine");
		p.GetComponent<rps_engine> ().setGame ();
		Debug.Log ("menuscript|play -----> setGame de rps_engine ejecutado.");
	}

}
