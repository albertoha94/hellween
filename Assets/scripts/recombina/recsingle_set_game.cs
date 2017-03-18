using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Prefab para el juego recombina single.
 * Aqui se genera solamente un juego y en base a la respuesta del jugador, gana o pierde.
 * Creada por Albertoha94 el 29/Nov/16.
 */
public class recsingle_set_game : MonoBehaviour {

	//-- Variables --------------------------------------------------------------------------------------------------------------
	private int _gametype = constants.REC_GAMETYPE_MENU;
	private List<GameObject> _l_cards;
	private float _camWidth;
	private float _camHeight;

	//-- Menu de listo.
	private GameObject _go_readyUp;

	//-- Menu de cartas.
	private GameObject _cardmenu;

	//-- Metodos ----------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		Debug.Log ("Comenzando recombina single");

		_l_cards = new List<GameObject> ();

		_cardmenu = transform.FindChild ("prefab_recsingle_menu").gameObject;

		_go_readyUp = this.transform.FindChild("canvas_readyup").gameObject;

		//-- Dimension del fondo igual a la dimension de la camara.
		SpriteRenderer sr_back = GameObject.Find("back_recombina").GetComponent<SpriteRenderer> ();
		double width = sr_back.sprite.bounds.size.x;
		//Debug.Log ("Valor de width: " + width);
		double height = sr_back.sprite.bounds.size.y;
		//Debug.Log ("Valor de height: " + height);
		_camHeight = (float)(Camera.main.orthographicSize * 2.0);
		Debug.Log ("Valor de worldHeight: " + _camHeight);
		_camWidth = _camHeight / Screen.height * Screen.width;
		Debug.Log ("Valor de worldWidth: " + _camWidth);
		float finalWidth = (float)(_camWidth / width);
		//Debug.Log (Valor de finalWidth: " + finalWidth);
		float finalHeight = (float)(_camHeight / height);
		//Debug.Log ("Valor de finalWidth: " + finalHeight);
		sr_back.transform.localScale = new Vector3 (finalWidth, finalHeight);

		setGameMode ();
	}
	
	// Update is called once per frame
	void Update () { }

	/**
	 * Usada para definir que debe de mostrarse en pantalla.
	 */
	private void setGameMode() {

		//-- Checar gametype.
		switch (_gametype) {
		case constants.REC_GAMETYPE_MENU:
			Debug.Log ("Tipo de juego: menu");
			break;
		case constants.REC_GAMETYPE_3CARDS:
			Debug.Log ("Tipo de juego: 3cards");
			createCards (3);
			break;
		case constants.REC_GAMETYPE_4CARDS:
			Debug.Log ("Tipo de juego: 4cards");
			createCards (4);
			break;
		case constants.REC_GAMETYPE_5CARDS:
			Debug.Log ("Tipo de juego: 5cards");
			createCards (5);
			break;
		}
	}

	/**
	 * Define el numero de cartas que habra en el juego.
	 * Usado de forma externa para definir su primera vista.
	 * @param	oNumber	Define que tipo de juego es.
	 */
	public void setNumberOfCards(int oNumber) {
		_gametype = oNumber;
	}

	/**
	 * Instancia todas las cartas que se requieren en el juego.
	 * @param	oNumberOfCards	El numero de cartas con las que se tiene establecido el juego.
	 */
	private void createCards(int oNumberOfCards) {
		int i = 0;

		//-- Desaparece el menu.
		_cardmenu.SetActive (false);

		//-- Dimension de la carta. (Por  algun motivo, no pude hacerlo en el mismo switch del start, regresaba 0)
		float cardScale = 1f;
		float cardPosExtraVar = 1f;
		switch (oNumberOfCards) {
		case 3:
			cardScale = 0.8f;
			cardPosExtraVar = 0.9f;
			break;
		case 4:
			cardScale = 0.7f;
			cardPosExtraVar = 0.75f;
			break;
		case 5:
			cardScale = 0.6f;
			cardPosExtraVar = 0.65f;
			break;
		}

		//-- Instanciar todas las cartas.
		GameObject card;
		SpriteRenderer sr_card;
		float extraWidth; 
		Vector3 position;
		Vector3 scale = new Vector3(cardScale, cardScale);
		while (i < oNumberOfCards) {

			//-- Instanciando la carta.
			card = (GameObject)Instantiate (Resources.Load ("prefabs/recsingle/prefab_recsingle_card"), 
				transform.position, transform.rotation);

			//-- Dandonle un numero a la carta.
			//card.GetComponent<recombina_card>().CardNumber = i;

			//-- Escala de la carta.
			card.transform.localScale = scale;

			//-- Posicion de la carta.
			sr_card = card.GetComponent<Renderer>() as SpriteRenderer;
			extraWidth = sr_card.bounds.size.x * cardPosExtraVar;
			//Debug.Log ("Extrawidth: " + extraWidth);
			position = new Vector3 (extraWidth + (_camWidth / 2 * -1) + (_camWidth / oNumberOfCards * i), 0f);
			//Debug.Log ("Position of the card: " + position.x);
			card.transform.position = position;

			//-- Set de su padre.
			card.transform.SetParent (this.gameObject.transform);

			//-- Agregarla al arreglo.
			_l_cards.Add (card);

			//-- Siguiente carta.
			i++;
		}

		showReadyUp (true);
	}

	/**
	 * Carga un prefab que se utiliza para anunciar el inicio del juego.
	 * @param	oValue	Si se muestra o no el canvas_readyup.
	 */
	private void showReadyUp(bool oValue) {
		_go_readyUp.SetActive (oValue);
	}

	/**
	 * Metodo usado para el onClick dentro del boton del canvas readyup.
	 * Esconde el canvas del readyup.
	 * Selecciona una carta ganadora.
	 * Establece el numero de iteraciones que se haran con las cartas.
	 */
	public void onClickReadyUp() {
		//Debug.Log ("Comenzando juego");

		//-- Esconde el canvas de readyup.
		showReadyUp(false);

		//-- Escoge una carta ganadora.
		int winner = Random.Range(0, _l_cards.Count);
		//Debug.Log ("Carta ganadora: " + winner);
		_l_cards [winner].GetComponent<recombina_card> ().Winner = true;

		//-- Numero de cambios que se haran.
		int extra = Random.Range(0, 5);
		int noswitches = 5 + extra;
		Debug.Log ("Numero de cambios que se van a hacer: " + noswitches);

		//-- Comienza ciclo de acciones.
		StartCoroutine(makeActions(winner, noswitches));
	}

	/**
	 *	Se encarga de hacer todas las acciones necesarias para hacer el cambio de cartas. 
	 *	@param	oWinnerPos	La posicion de la carta ganadora en el arreglo.
	 *	@param oNoSwitches	El numero de cambios que se va a realizar.
	 */
	private IEnumerator makeActions(int oWinnerPos, int oNoSwitches) {

		//-- Voltea la carta del ganador.
		yield return new WaitForSeconds(1.0f);
		yield return _l_cards [oWinnerPos].GetComponent<recombina_card> ().flip (constants.FLIP_FACEUP);
		yield return new WaitForSeconds (3.0f);
		yield return _l_cards [oWinnerPos].GetComponent<recombina_card> ().flip (constants.FLIP_FACEDOWN);

		//-- Que se hagan los cambios de cartas.
		yield return new WaitForSeconds(3.0f);
		//_flag_NextCard = true;
		//int i = 0;
		//while (i < oNoSwitches) {
		//	if (_flag_NextCard) {
		switchCards(0, oNoSwitches);
		//		_flag_NextCard = false;
		//		i++;
		//		Debug.Log ("Se hizo un volteo de cartas: " + i);
		//	}
		//	Debug.Log ("Ciclo de switch aun sigue funcionando");
		//}
	}

	/**
	 * Cambia la posicion de 2 cartas. Si se excede el numero maximo de cambios entonces habilita las cartas 
	 * para ser presionadas.
	 * @param	oCurrentSwitch	EL cambio en el que va actualmente.
	 * @param	oMaxSwitches	El numero maximo de cambios que se van a tener.
	 */
	public void switchCards(int oCurrentSwitch, int oMaxSwitches) {
		if (oCurrentSwitch < oMaxSwitches) {

			//-- Genera 2 numeros aleatorios diferentes.
			List<int> numbers = new List<int> ();
			numbers.Add (Random.Range (0, _l_cards.Count));
			bool flag_correct = true;
			int dnum;
			while (flag_correct) {
				dnum = Random.Range (0, _l_cards.Count);
				if (dnum != numbers [0]) {
					flag_correct = false;
					numbers.Add (dnum);
				}
			}
			//Debug.Log ("Longitud de la lista: " + numbers.Count);

			//-- Agarra la primera carta.
			recombina_card script1 = _l_cards [numbers [0]].GetComponent<recombina_card> ();
			script1._willCallback = true;
			script1._currentSwitch = oCurrentSwitch;
			script1._maxSwitch = oMaxSwitches;

			//-- Agarra la segunda carta.
			recombina_card script2 = _l_cards [numbers [1]].GetComponent<recombina_card> ();

			//Debug.Log ("Moviendo cartas: " + script1.CardNumber + ", " + script2.CardNumber);

			//-- Mover las cartas.
			StartCoroutine (script1.moveTo (script2.getPosition (), constants.GOUP));
			StartCoroutine (script2.moveTo (script1.getPosition (), constants.GODOWN));
		} else {
			enableCardPress (true);
		}
	}

	/**
	 *	Cambia una bandera dentro de cada carta para que estas puedan ser presionadas.
	 *	oValue	El valor que se le dara a la variable de cada carta.
	 */
	public void enableCardPress(bool oValue) {
		foreach (GameObject card in _l_cards) {
			card.GetComponent<recombina_card> ()._canBePressed = oValue;
		}
		Debug.Log ("Cartas deshabilitadas/habilitadas para presionar");
	}

	/**
	 *	Muestra el ganador. Ocurre solamente cuando el jugador escoge una carta equivocada.
	 */
	public void showWinner() {
		foreach (GameObject go in _l_cards) {
			recombina_card script = go.GetComponent<recombina_card> ();
			if (script.Winner == true) {
				StartCoroutine (script.flip (constants.FLIP_FACEUP));
			}
		}
	}
}
