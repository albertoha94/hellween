using UnityEngine;
using System.Collections;

/**
 * Igual que el metodo pcard pero este no cuenta con eventos del mouse.
 */
public class iacard : MonoBehaviour {

	//-- Variables ---------------------------------------------------------------------------------------------------------------------
	public Sprite _front;

	//-- Metodos sobrecargados ----------------------------------------------------------------------------------------------------------

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	/**
	 * Cambia el sprite, usado en las animaciones.
	 */
	public void changeSprite(){
		GetComponent<SpriteRenderer> ().sprite = _front;
	}
}
