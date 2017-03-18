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
 * 				-Agregada variable GAMEMODE_STRINGS.
 * 02/Dic/16:	-Agregada constante REC_GAMETYPE_MENU.
 * 				-Agregada constante REC_GAMETYPE_3CARDS.
 * 				-Agregada constante REC_GAMETYPE_4CARDS.
 * 				-Agregada constante REC_GAMETYPE_5CARDS.
 * 11/Dic/16:	-Cambiada variable WIDTH a static.
 * 				-Cambiada variable HEIGHT a static.
 * 14/Dic/16:	-Agregada variables GORIGHT, GOLEFT, GOUP, GODOWN.
 * 				-Agregada variables REC_CARD1, REC_CARD2, REC_CARD3, REC_CARD4, REC_CARD5.
 * 16/Dic/16:	-Agregada variables FLIP_FACEUP, FLIP_FACEDOWN;
 */
public class constants : MonoBehaviour
{
	public const int GAMEMODE_RECSINGLE = 0;
	public const int GAMEMODE_RECLADDER = 1;
	public const int GAMEMODE_RPS = 2;
	public static int WIDTH = Screen.width;
	public static int HEIGHT = Screen.height;
	public static string [] GAMEMODE_STRINGS = {
		"Recombina Single", "Recombina Ladder", "Piedra, Papel, Tijera"
	};

	//-- El lado de la carta
	public const bool FLIP_FACEUP = true;
	public const bool FLIP_FACEDOWN = false;

	//-- Recombina game modes.
	public const int REC_GAMETYPE_MENU = 0;
	public const int REC_GAMETYPE_3CARDS = 1;
	public const int REC_GAMETYPE_4CARDS = 2;
	public const int REC_GAMETYPE_5CARDS = 3;

	//-- Variables para mover las cartas.
	public const bool GORIGHT = false;
	public const bool GOLEFT = true;
	public const bool GOUP = true;
	public const bool GODOWN = false;
	public const int REC_CARD1 = 0;
	public const int REC_CARD2 = 1;
	public const int REC_CARD3 = 2;
	public const int REC_CARD4 = 3;
	public const int REC_CARD5 = 4;
}

