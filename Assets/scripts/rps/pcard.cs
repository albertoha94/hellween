using UnityEngine;
using System.Collections;

public class pcard : MonoBehaviour {

	//-- Constantes ---------------------------------------------------------------------------------------------------------------------
	private const int LEFT_MOUSE = 0;

	//-- Variables ----------------------------------------------------------------------------------------------------------------------
	private SpriteRenderer _renderer;
	private GameObject _parent;
	private int _value;

	//-- Metodos sobrecargados ----------------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start () {
		_renderer = gameObject.GetComponent<SpriteRenderer> ();
		_parent = gameObject.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}

	//-- Eventos del mouse --------------------------------------------------------------------------------------------------------------
	void OnMouseDown(){
		if (Input.GetMouseButtonDown (LEFT_MOUSE)) {
			
			//-- Cambiar el valor de lo seleccionado en el padre
			_parent.GetComponent<rps_engine>().cardSelected(this.Value);
		}
	}

	void OnMouseUp(){	}

	//-- Propiedades --------------------------------------------------------------------------------------------------------------------
	public int Value {
		get {
			return this._value;
		}
		set {
			_value = value;
		}
	}
}
