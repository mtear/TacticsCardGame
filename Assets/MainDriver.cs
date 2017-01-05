using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;
using System.Collections.Generic;

public class MainDriver : MonoBehaviour {

	public static TableTop MAINTABLE;

	public static bool DRAGGING = false, MOVEDRAGGING = false;

	public static UnityAnimationInterface UAI;
	public static UnityCommandInterface UCI;

	public static List<Command> playercommands = new List<Command> ();
	public static bool turnover = false;

	public static string P1CB = "CardBack1", P2CB = "CardBack2";

	// Use this for initialization
	void Awake () {
		//UnitySystemConsoleRedirector.Redirect ();

		//Make players
		//Make decks
		//Start the static tabletop
		UCI = new UnityCommandInterface();
		Player P1 = new Player ("P1", TableTop.ROWS, TableTop.COLUMNS, TableTop.MAXLIFE, null, UCI);
		Player P2 = new Player ("P2", TableTop.ROWS, TableTop.COLUMNS, TableTop.MAXLIFE, P1, new EnemyAI());
		P1.SetEnemy (P2);

		Deck D1 = new Deck (Utility.GetDebugStarterDeck(P1));
		Deck D2 = new Deck (Utility.GetDebugStarterDeck(P2));

		P1.LoadDeck (D1);
		P2.LoadDeck (D2);

		UnityAnimationInterface uai = new UnityAnimationInterface ();
		MainDriver.UAI = uai;
		MAINTABLE = new TableTop ("EN", P1, P2, uai);

		//Debug.Log ("Game Start!");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (playercommands.Count == 0 && turnover) {
			Pass ();
		}
	}

	public void Pass(){
		//if (MasterMonitor.STATUS != MasterMonitor.IDLE) {
		//	return;
		//}
		string c = "pass";
		Command command = Command.Parse (c, MainDriver.MAINTABLE.P1);
		turnover = true;
		//MainDriver.UCI.Flush (MainDriver.MAINTABLE.P1, command);
		playercommands.Add(command);

		HandMonitorUI.INDEX = -1;
	}
		
}
