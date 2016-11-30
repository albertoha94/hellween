using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class recombina_engine : MonoBehaviour {

	//-- Variables.
	private List<GameObject> _cards;
	GameObject _txt_game;


	// Use this for initialization
	void Start () {

		//-- Esconde las cartas.
		_cards = new List<GameObject>();
		_cards.Add (transform.FindChild ("carta_1").gameObject);
		_cards.Add (transform.FindChild ("carta_2").gameObject);
		_cards.Add (transform.FindChild ("carta_3").gameObject);
		int i = 0;
		while (i < _cards.Count) {
			_cards [i].SetActive (false);
			//_cards [i].GetComponent<Animator> ().enabled = false;
		}

		//-- Esconde el texto.
		_txt_game = GameObject.Find("txt_announcer");
		_txt_game.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
