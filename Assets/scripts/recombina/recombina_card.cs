using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Carga un prefab dependiendo del modo de juego.
 * Creada por Albertoha94 el 13/Dic/16.
 */
public class recombina_card : MonoBehaviour {

	//-- Variables -----------------------------------------------------------------------------------------------------------------
	private bool _winner;
	public bool _canBePressed;
	private GameObject _parent;

	//-- Variables para el movimiento de la carta.
	public bool _willCallback;
	public int _currentSwitch, _maxSwitch;
	private bool _canMove, _move_DirectionUpDown, _move_DirectionLeftRight;
	private float _radio, _degrees;
	private Vector3 _move_posFinal, _move_posInitial;

	//-- Sprites.
	public Sprite _spr_cardBack, _spr_cardCorrect, _spr_cardWrong;

	//-- Variables para el volteo de la carta.
	private bool _flipping, _flip_shrink, _flip_grow, _face;
	private Vector3 _currentScale;

	//-- Metodos Sobrecargados -----------------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () {
		_canMove = false;
		_move_DirectionLeftRight = constants.GORIGHT;
		_move_DirectionUpDown = constants.GODOWN;
		_canBePressed = false;
		_parent = this.gameObject.transform.parent.gameObject;
	}
	
	/*
	 * Update es llamado una vez cada frame.
	 * Realiza las acciones de cambiado de tamaño y traslación de la carta.
	 */
	void Update () {

		//-- Volteo de cartas.
		if (_flipping) {

			//-- Agrandando la carta.
			if (_flip_grow) {
				//Debug.Log ("Creciendo carta");
				transform.localScale = Vector3.Lerp (transform.localScale, 
					new Vector3 (0.9f, _currentScale.y, _currentScale.z), Time.deltaTime * 10.0f);

				//-- Termina si la sprite ya quedo en su tamaño correcto.
				//Debug.Log("LocalScale: " + transform.localScale.x);
				//Debug.Log("CurrenctScale: " + _currentScale.x);
				if (transform.localScale.x >= _currentScale.x) {
					//Debug.Log ("Terminado volteo");
					transform.localScale = _currentScale;
					_flipping = false;
					_flip_grow = false;
				}
			}

			//-- Achicando la carta.
			if (_flip_shrink) {
				//Debug.Log ("Encogiendo carta");
				transform.localScale = Vector3.Lerp (transform.localScale, 
					new Vector3 (0, _currentScale.y, _currentScale.z), Time.deltaTime * 10.0f);

				//-- Cambia la sprite si ya no se ve.
				if (transform.localScale.x <= 0.0001f) {
					SpriteRenderer sr_card = this.gameObject.GetComponent<SpriteRenderer> ();

					if (_face == constants.FLIP_FACEDOWN) {
						sr_card.sprite = _spr_cardBack;
					} else {
						//Debug.Log ("Carta volteada boca arriba: " + _cardNumber);
						if (_winner) {
							sr_card.sprite = _spr_cardCorrect;
						} else {
							sr_card.sprite = _spr_cardWrong;
						}
					}
					_flip_shrink = false;
					_flip_grow = true;
				}
			}
		}
	
		//-- Movimiento de la carta.
		if (_canMove) {
			//Debug.Log ("Moviendo carta");
			//Debug.Log ("Radio de distancia: " + _radio);

			//-- Posicion X.
			float xOrigin = 0;
			if(_move_DirectionLeftRight == constants.GORIGHT) {
				xOrigin = _move_posInitial.x + _radio;
			} else {
				xOrigin = _move_posInitial.x - _radio;
			}
			float newx = _radio * Mathf.Cos(Mathf.Deg2Rad * _degrees) + xOrigin;

			//-- Actualizamos los angulos.
			if (_move_DirectionLeftRight == constants.GORIGHT) {
				_degrees -= 3;
			}
			if (_move_DirectionLeftRight == constants.GOLEFT) {
				_degrees += 3;
			}

			//-- Posicion Y.
			float newy;
			if (_move_DirectionUpDown == constants.GOUP) {
				newy = _radio * Mathf.Sin (Mathf.Deg2Rad * _degrees);
			} else {
				newy = _radio * Mathf.Sin (Mathf.Deg2Rad * _degrees) * -1;
			}

			Vector3 update = new Vector3(newx, newy, getPosition().z);
			transform.position = update;

			if ((_degrees > 180 && _move_DirectionLeftRight == constants.GOLEFT) ||
				(_degrees < 0 && _move_DirectionLeftRight == constants.GORIGHT)) {
				_canMove = false;
				Vector3 update_final = new Vector3(_move_posFinal.x, _move_posFinal.y, _move_posFinal.z);
				transform.position = update_final;

				Debug.Log ("Acabe el switch: " + _currentSwitch);

				//-- Volver a llamar el metodo.
				if (_willCallback) {
					recsingle_set_game sc_parent = _parent.GetComponent<recsingle_set_game> ();
					_currentSwitch++;
					//Debug.Log ("Llamando al switch: " + _currentSwitch);
					_willCallback = false;
					sc_parent.switchCards (_currentSwitch, _maxSwitch);
				}
			}
		}
	}

	/**
	 * Ejecutado al presionar la carta.
	 */
	void OnMouseDown () {
		if (_canBePressed) {
			_canBePressed = false;
			_parent.GetComponent<recsingle_set_game> ().enableCardPress (false);
			StartCoroutine (flipForWin ());
			//yield return WaitForSeconds (3.0f);
			Debug.Log ("Me presionaste");
		} 
	}	

	/**
	 * Regresa la posicion que tiene la carta actualmente.
	 */
	public Vector3 getPosition() {
		return this.gameObject.transform.position;
	}

	/**
	 * Mueve la carta a la posicion que se le indica.
	 * @param oCardBounds		La nueva posicion en la que iran las cartas.
	 * @param oCardDirection	La direccion en la que debe de ir la carta.
	 */
	public IEnumerator moveTo(Vector3 oCardBounds, bool oCardDirection) {
		//Debug.Log ("Moviendo la carta: " + oCardNumber);
		_move_DirectionUpDown = oCardDirection;
		_move_posFinal = oCardBounds;
		_move_posInitial = getPosition ();
		_radio = Vector3.Distance (this.gameObject.transform.position, _move_posFinal) / 2;

		//-- Define la direccion a la que ira la carta.
		//Debug.Log("XCarta: " + this.gameObject.transform.position.x + ", XFinal: " + oCardBounds.x);
		if (this.gameObject.transform.position.x > oCardBounds.x) {
			_move_DirectionLeftRight = constants.GOLEFT;
			_degrees = 0;
			//Debug.Log ("Va a ir a la izquierda");
		} else {
			_move_DirectionLeftRight = constants.GORIGHT;
			_degrees = 180f;
			//Debug.Log ("Va a ir a la derecha");

		}

		//-- Actualizas el numero que tiene como carta.
		//_cardNumber = oCardNumber;
		_canMove = true;

		//Debug.Log ("Carta movida a " + oCardNumber);
		yield return new WaitUntil(() => _canMove == false);
	}

	/**
	 * Realiza una animacion en donde se cambia la sprite de acuerdo al tipo de vuelta que se da.
	 * @param	oFace	La cara que se mostrara.
	 */
	public IEnumerator flip(bool oFace) {
		_flipping = true;
		_flip_shrink = true;
		_face = oFace;
		_currentScale = transform.localScale;
		yield return new WaitUntil (() => _flipping == false);
	}

	/**
	 *	Metodo basado en flip() pero realiza mas acciones despues de terminar el volteo de la carta.
	 */
	private IEnumerator flipForWin() {
		_flipping = true;
		_flip_shrink = true;
		_face = constants.FLIP_FACEUP;
		_currentScale = transform.localScale;
		yield return new WaitUntil (() => _flipping == false);
		GameObject go_announcement = _parent.transform.FindChild("canvas_gameinfo").gameObject;
		go_announcement.SetActive (true);
		Text txt = go_announcement.transform.FindChild("txt_announce").GetComponent<Text> ();
		if (_winner) {
			Debug.Log ("Ganaste");
			txt.text = "Ganaste";
		} else {
			Debug.Log ("Perdiste");
			txt.text = "Perdiste";
			_parent.GetComponent<recsingle_set_game> ().showWinner ();
		}
	}

	//-- Propiedades -----------------------------------------------------------------------------------------------------------------
	public bool Winner {
		get {
			return _winner;
		}

		set {
			_winner = value;
		}
	}
}
