using UnityEngine;
using System.Collections;

/**
 * Carga un prefab dependiendo del modo de juego.
 * Creada por Albertoha94 el 29/Nov/16.
 * Cambios:
 * 29/Nov/16.	-Agregado codigo dentro de Start.
 */
public class game_loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int game = global_variables.GAME;
		string msg = constants.GAMEMODE_STRINGS [game];
		Debug.Log ("Se escogio el juego: " + msg);

		switch (game) {
		case constants.GAMEMODE_RECSINGLE:
			//-- Agregar el prefab.
			break;
		}
	}
}
