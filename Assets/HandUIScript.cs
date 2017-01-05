using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;
using System;
using UnityEngine.UI;

public class HandUIScript : MonoBehaviour {

	private Vector3 screenPoint; private Vector3 offset;
	public int index = 0;

	public static int dragindex = -1;
	int timer = 0, timermax = 30;
	bool validinfo = false;

	RectTransform rt;

	Transform infop;

	void Start(){
		rt = GetComponent<RectTransform> ();

		infop = transform.Find ("InfoPanel");
	}

	public static Point RECRUITPOINT;

	void FixedUpdate(){
		if (MainDriver.DRAGGING && index == dragindex) {
			if (timer++ == timermax && validinfo) {
				InfoPanelMonitor.character = MainDriver.MAINTABLE.P1.HAND.Get (index);
				InfoPanelStateMonitor.TRIGGER = true;

				validinfo = false;
			}

			if (!validinfo) {
				//dragindex = -1;
				timer = 0;
			}

			//transform.position = Input.mousePosition;
		}

		if (!MainDriver.DRAGGING) {
			RECRUITPOINT = null;
		}
			
	}

	public void OnDown() {
		dragindex = index; validinfo = true;

		if (UnityAnimationInterface.ANIMATING)
			return;
		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.MAINTABLE.VALID)
			return;

		MainDriver.DRAGGING = true;

		HandMonitorUI.INDEX = index;

		//rt.pivot = new Vector2 (.5f, .5f);
		//transform.position = Input.mousePosition;
	}

	public void OnDrag() 
	{ 
		validinfo = false;
		if (UnityAnimationInterface.ANIMATING)
			return;
		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!MainDriver.DRAGGING)
			return;

		rt.pivot = new Vector2 (.5f, .5f);

		/*Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		curPosition.z = 0;
		transform.position = curPosition;*/
		transform.position = Input.mousePosition;

		//Get hover point
		LayerMask lm = 1 << LayerMask.NameToLayer ("CardBoard");
		Vector3 origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		origin.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 1, lm);

		if (hit.collider != null) {
			GameObject G = hit.collider.gameObject;
			BoardSquare BS = G.GetComponent<BoardSquare> ();
			Character character = MainDriver.MAINTABLE.P1.HAND.Get (index);
			if (BS.Player) {
				RECRUITPOINT = new Point (BS.x, BS.y);

				string infotext = InfoPanelMonitor.RowText (character, BS.y);
				if (infotext == character.CONTROLLER.TABLE.STRINGTABLE.Get ("FOOBAR")
					|| ( !character.CONTROLLER.GAMEBOARD.ValidRecruit(character, BS.x, BS.y) && BS.y < 2)
				) {
					infop.gameObject.SetActive (false);
				} else {
					infop.Find ("Text").GetComponent<Text> ().text = infotext;
					infop.gameObject.SetActive (true);
				}
			} else {
				RECRUITPOINT = null;
				infop.gameObject.SetActive (false);
			}
		} else {
			RECRUITPOINT = null;
		}
	}

	public void OnUp(){
		infop.gameObject.SetActive (false);

		timer = 0; dragindex = -1; validinfo = false;

		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!UnityCommandInterface.WAITING)
			return;

		rt.pivot = new Vector2 (0, .5f);

		MainDriver.DRAGGING = false;

		LayerMask lm = 1 << LayerMask.NameToLayer ("CardBoard");
		Vector3 origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		origin.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 1, lm);

		if (hit.collider != null) {
			Character character = MainDriver.MAINTABLE.P1.HAND.Get (index);

			GameObject G = hit.collider.gameObject;
			BoardSquare BS = G.GetComponent<BoardSquare> ();
			if (!BS.Player)
				return;

			//Do recruit actions/hand actions
			bool good = false;
			string c = "";
			if (BS.y < 2) {
				c = "recruit " + BS.x + " " + BS.y + " " + index;
			} else {
				c = "hand " + index + " " + BS.x;
			}
			Command command = Command.Parse (c, MainDriver.MAINTABLE.P1);


			good = MainDriver.UCI.Flush (MainDriver.MAINTABLE.P1, command);

			HandMonitorUI.INDEX = -1;

			//temp
			if (command != null && command.Type == Command.TYPE.ACTIVE) {
				UnityCommandInterface.ACTIVEINDEX = BS.x;
				if (!((Active)character.HandAbility).RequestsTarget) {
					//MainDriver.UAI.ActiveAnimation (BS.x, character);
				}
			}


			if (good) {
				Destroy (this.gameObject);
			}
		}
	}

}
