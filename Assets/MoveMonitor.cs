using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class MoveMonitor : MonoBehaviour {

	private Vector3 screenPoint; private Vector3 offset;
	Character character;
	Point location;
	Vector3 startpos;

	Vector3 mousepos;

	bool holding = false;
	int t = 0, tmax = 30;

	public static Character movecharacter;

	void FixedUpdate(){
		if (holding) {
			if (t++ == tmax) {
				InfoPanelMonitor.character = character;
				InfoPanelStateMonitor.TRIGGER = true;

				holding = false;
			}
		} else {
			t = 0;

			if (MainDriver.MOVEDRAGGING && movecharacter == character) {
				OnMouseDrag ();
			}
		}
	}

	void Start(){
		character = GetComponent<CharacterMonitor> ().character;
		location = character.CONTROLLER.GAMEBOARD.LocateInBoard (character);
		startpos = transform.position;
	}

	void OnMouseDown() {
		holding = true;
		mousepos = Input.mousePosition;

		if (MasterMonitor.STATUS != MasterMonitor.IDLE) {
			return;
		}
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!UnityCommandInterface.WAITING)
			return;
		if (character.Moved || character.Rooted)
			return;

		if (character.CONTROLLER != MainDriver.MAINTABLE.P1)
			return;

		MainDriver.MOVEDRAGGING = true;
		movecharacter = character;
		screenPoint = Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		offset = gameObject.transform.position - screenPoint;
	}

	void OnMouseDrag() 
	{ 
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		curPosition.z = 0;

		if (Vector3.Distance (startpos, curPosition) < 1.5)
			return;
		
		holding = false;

		if (MasterMonitor.STATUS != MasterMonitor.IDLE) {
			return;
		}
		if (!MainDriver.MOVEDRAGGING)
			return;
		transform.position = curPosition;
	}

	void OnMouseUp(){
		holding = false;
		if (MasterMonitor.STATUS != MasterMonitor.IDLE) {
			return;
		}
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!UnityCommandInterface.WAITING)
			return;
		if (character.Moved)
			return;

		MainDriver.MOVEDRAGGING = false;

		LayerMask lm = 1 << LayerMask.NameToLayer ("CardBoard");
		Vector3 origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		origin.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 1, lm);

		if (hit.collider != null) {
			//Character character = MainDriver.MAINTABLE.P1.HAND.Get (index);

			GameObject G = hit.collider.gameObject;
			BoardSquare BS = G.GetComponent<BoardSquare> ();

			//Do recruit actions/hand actions
			string c = "move " +location.x + " " + location.y + " " + BS.x + " " + BS.y;
			Command command = Command.Parse (c, MainDriver.MAINTABLE.P1);
			//Debug.Log ("FLUSHING MOVE COMMAND");
			bool good = MainDriver.MAINTABLE.Flush (MainDriver.MAINTABLE.P1, command);

			if (good) { //reset position to new gameobject
				transform.position = G.transform.position
					+ new Vector3(0, 0.8f) + new Vector3(0, 0, -1*BS.x);

				Vector3 ls = new Vector3 (.25f, .25f, 1);
				ls.x = ls.x + ((float)BS.x * 0.025f);
				ls.y = ls.y + ((float)BS.x * 0.025f);
				transform.localScale = ls;

				location.x = BS.x;
				location.y = BS.y;

				GetComponent<CharacterMonitor> ().x = BS.x;
				GetComponent<CharacterMonitor> ().y = BS.y;
			} else {
				transform.position = startpos;
			}
		} else {
			transform.position = startpos;
		}
	}

}
