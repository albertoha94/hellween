using UnityEngine;
using System.Collections;

/**
 * Contiene todas las constantes usadas dentro del juego.
 * Creada por Albertoha94 el 29/Nov/16.
 * Cambios:
 * 29/Nov/16.	-Agregada variable GAMEMODE_RECSINGLE.
 * 				-Agregada variable GAMEMODE_RECLADDER.
 * 				-Agregada variable GAMEMODE_RPS.
 * 				-Agregada variable WIDTH.
 * 				-Agregada variable HEIGHT.
 * 				-Agregadav variable GAMEMODE_STRINGS.
 */
public class constants : MonoBehaviour
{
	//-- Variables.
	public const int GAMEMODE_RECSINGLE = 0;
	public const int GAMEMODE_RECLADDER = 1;
	public const int GAMEMODE_RPS = 2;
	public int WIDTH = Screen.width;
	public int HEIGHT = Screen.height;
	public string [] GAMEMODE_STRINGS = {
		"Recombina Single", "Recombina Ladder", "Piedra, Papel, Tijera"
	};
}

