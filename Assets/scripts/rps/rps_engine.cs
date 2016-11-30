using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class rps_engine : MonoBehaviour {

	//-- Constantes --------------------------------------------------------------------------------------------------------------------
	private const int PIEDRA = 0;
	private const int PAPEL = 1;
	private const int TIJERA = 2;

	private const int WIN = 0;
	private const int LOSE = 1;
	private const int DRAW = 2;

	//-- Variables ---------------------------------------------------------------------------------------------------------------------
	List<GameObject> _pcard, _iacard;
	GameObject _b_replay, _img_checkia, _img_checkp, _txt;
	private int _playerval, _iaval, _winner;
	public Sprite _spr_notready, _spr_ready;

	//-- Use this for initialization
	void Start () {
		//-- Inicia los objetos
		_pcard = new List<GameObject>();
		_iacard = new List<GameObject>();

		//-- Objetos del jugador.
		//-- Deshabilitamos sus animators.
		GameObject p = transform.FindChild ("pcard_r").gameObject;
		p.GetComponent<pcard> ().Value = PIEDRA;
		p.GetComponent<Animator> ().enabled = false;
		_pcard.Add (p);
		p = transform.FindChild ("pcard_p").gameObject;
		p.GetComponent<Animator> ().enabled = false;
		p.GetComponent<pcard> ().Value = PAPEL;
		_pcard.Add (p);
		p = transform.FindChild ("pcard_s").gameObject;
		p.GetComponent<Animator> ().enabled = false;
		p.GetComponent<pcard> ().Value = TIJERA;
		_pcard.Add (p);

		//-- Objetos de la ia.
		//-- Los movemos fuera de la camara.
		p = transform.FindChild ("card_back_1").gameObject;
		p.GetComponent<Animator> ().enabled = false;
		_iacard.Add (p);
		p = transform.FindChild ("card_back_3").gameObject;
		p.GetComponent<Animator> ().enabled = false;
		_iacard.Add (p);
		p = transform.FindChild ("card_back_2").gameObject;
		p.GetComponent<Animator> ().enabled = false;
		_iacard.Add (p);

		//-- Esconder objetos.
		_b_replay = GameObject.Find ("b_reset");
		_b_replay.SetActive(false);
		_img_checkia = GameObject.Find ("check_ia");
		_img_checkia.SetActive(false);
		_img_checkp = GameObject.Find ("check_player");
		_img_checkp.SetActive(false);
		_txt = GameObject.Find ("txt_announcer");
		_txt.SetActive(false);
		GameObject pbutton = GameObject.Find("b_play");
		pbutton.GetComponent<Animator> ().enabled = false;
	}
	
	//-- Update is called once per frame
	void Update () { }

	//-- Metodos -----------------------------------------------------------------------------------------------------------------------
	/**
	 * Compara el valor de el jugador(_playerval) y le da uno a la ia(_iaval).
	 */
	void compare(){

		//-- Switch para comparar los valores.
		switch (this.Playerval) {
		case PIEDRA:
			switch (this.Iaval) {
			case PIEDRA:
				_winner = DRAW;
				break;
			case PAPEL:
				_winner = LOSE;
				break;
			case TIJERA:
				_winner = WIN;
				break;
			}
			break;
		case PAPEL:
			switch (this.Iaval) {
			case PIEDRA:
				_winner = WIN;
				break;
			case PAPEL:
				_winner = DRAW;
				break;
			case TIJERA:
				_winner = LOSE;
				break;
			}
			break;
		case TIJERA:
			switch (this.Iaval) {
			case PIEDRA:
				_winner = LOSE;
				break;
			case PAPEL:
				_winner = WIN;
				break;
			case TIJERA:
				_winner = DRAW;
				break;
			}
			break;
		}

		//-- Mostrar el resultado.
		string res = "";
		switch (_winner) {
		case WIN:
			res = "Ganado";
			break;
		case DRAW:
			res = "Empatado";
			break;
		case LOSE:
			res = "Perdido";
			break;
		}
		Debug.Log ("Compare --> Resultado final: " + res);

		_txt.GetComponent<Text> ().text = "Has " + res;
	}

	/**
	 * Prepara el juego para el primer uso.
	 */
	public void setGame(){

		//-- Mostramos carta player.
		for (int i = 0; i < _pcard.Count; i++) {
			GameObject d = _pcard [i];
			d.GetComponent<Animator> ().enabled = true;
		}

		//-- Mostramos cartas ia.
		for (int i = 0; i < _iacard.Count; i++) {
			GameObject d = _iacard [i];
			d.GetComponent<Animator> ().enabled = true;
		}

		//-- Mostrar las imagenes de listo.
		_img_checkia.SetActive(true);
		_img_checkp.SetActive (true);
		_txt.SetActive (true);
	}

	/**
	 * Esconde cartas del jugador menos la seleccioanda.
	 */
	public void cardSelected(int oCardPos){

		//-- Set de la variable.
		this.Playerval = oCardPos;
		Debug.Log ("rps_engine|cardSelected --> Valor de oCardPos = " + oCardPos);

		//-- Escondemos las cartas del jugador que no se seleccionaron.
		string[] animations_p = { "fadeout_pcard_r", "fadeout_pcard_p", "fadeout_pcard_s"};
		for (int i = 0; i < _pcard.Count; i++) {
			if (i != oCardPos) {
				_pcard [i].GetComponent<Animator> ().Play (animations_p [i]);
			} 
		}

		//-- Mostramos la imagen como lista.
		_img_checkp.GetComponent<SpriteRenderer>().sprite = _spr_ready;

		//-- La ia escoge carta.
		cardSelectIA();
	}

	/**
	 * Esconde cartas de la ia menos la seleccioanda.
	 */
	private void cardSelectIA(){

		//-- Le damos un valor a la ia.
		int iaval = Random.Range (0, 3);
		Debug.Log ("cardSelectIA --> Valor de ia: " + iaval);
		this.Iaval = iaval;

		//-- Escondemos las cartas de la ia que no se seleccionaron.
		string[] animations_ia = { "fadeout_iacard_1", "fadeout_iacard_3", "fadeout_iacard_2"};
		string[] animations_flip = { "flip_card_back_1", "flip_card_back_3", "flip_card_back_2" };
		for (int i = 0; i < _iacard.Count; i++) {
			if (i != iaval) {
				_iacard[i].GetComponent<Animator>().Play(animations_ia[i]);
			}
			else {
				_iacard [i].GetComponent<Animator> ().Play (animations_flip[i]);
			}
		}

		//-- Mostramos la imagen como lista.
		_img_checkia.GetComponent<SpriteRenderer>().sprite = _spr_ready;

		//-- Ya se tienen amobs valores, se compara.
		compare();
	}

	//-- Properties --------------------------------------------------------------------------------------------------------------------
	public int Playerval {
		get {
			return this._playerval;
		}
		set {
			_playerval = value;
		}
	}

	public int Iaval {
		get {
			return this._iaval;
		}
		set {
			_iaval = value;
		}
	}
}
