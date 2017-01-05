using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class TestMouseFunctions : MonoBehaviour {

	private Vector3 screenPoint; private Vector3 offset;
	public int index = 0;

	void OnMouseDown() {
		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!UnityCommandInterface.WAITING)
			return;

		MainDriver.DRAGGING = true;
		screenPoint = Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		offset = gameObject.transform.position - screenPoint;
	}

	void OnMouseDrag() 
	{ 
		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.DRAGGING)
			return;
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		curPosition.z = 0;
		transform.position = curPosition;
	}

	void OnMouseUp(){
		if (MasterMonitor.STATUS != MasterMonitor.IDLE)
			return;
		if (!MainDriver.MAINTABLE.VALID)
			return;
		if (!UnityCommandInterface.WAITING)
			return;

		MainDriver.DRAGGING = false;
		
		LayerMask lm = 1 << LayerMask.NameToLayer ("CardBoard");
		Vector3 origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		origin.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 1, lm);

		if (hit.collider != null) {
			//Character character = MainDriver.MAINTABLE.P1.HAND.Get (index);

			GameObject G = hit.collider.gameObject;
			BoardSquare BS = G.GetComponent<BoardSquare> ();

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
			if (good) {
				Destroy (this.gameObject);
			}
		}
	}
		
}
