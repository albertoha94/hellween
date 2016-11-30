using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * Almacena los metodos de cada botron dentro del menu.
 * Creada por Albertoha94 el 29/Nov/16.
 * Cambios:
 * 29/Nov/16.	-Agregado metodo goToRecSingle.
 */
public class main_menu : MonoBehaviour {

	/**
	 * Manda a la escena de recombina single.
	 */
	public void goToRecSingle() {
		global_variables.GAME = constants.GAMEMODE_RECSINGLE;
		SceneManager.LoadScene("game");
	}
}
