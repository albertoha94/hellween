using UnityEngine;
using System.Collections;

/**
 * Carga un prefab dependiendo del modo de juego.
 * Creada por Albertoha94 el 29/Nov/16.
 * Cambios:
 * 29/Nov/16.	-Agregado codigo dentro de Start.
 * 02/Dic/16.	-Agregado codigo temporal en Start para que siempre sea Recombina Single.
 * 04/Dic/16:	-Actualizado codigo para instanciar el prefab: prefab_gm_recsingle.
 * 11/Dic/16:	-Actualizado condigo en Start para establecer como padre del engine a la camara.
 */
public class game_loader : MonoBehaviour {

	//-- Variables.

	//-- Gamemodes.
	public GameObject _prefab_recsingle;

	// Use this for initialization
	void Start () {
		Debug.Log ("Iniciando game loader.");

		//-- Temporal, debe ser removido en el producto final.
		global_variables.game = 0;
		Debug.Log ("Asignandole un 0 a game(Recombina single)");

		int game = global_variables.game;
		string msg = constants.GAMEMODE_STRINGS[game];
		Debug.Log ("Se escogio el juego: " + msg);

		switch (game) {
		case constants.GAMEMODE_RECSINGLE:

			//-- Agregar el prefab.
			_prefab_recsingle.GetComponent<recsingle_set_game> ().setNumberOfCards (constants.REC_GAMETYPE_MENU);
			GameObject menu = Instantiate (_prefab_recsingle, transform.position, transform.rotation) as GameObject;
			menu.transform.parent = this.gameObject.transform;
			break;
		}
	}
}
