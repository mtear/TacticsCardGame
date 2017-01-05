using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class ReticleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		if (!UnityCommandInterface.TARGETING)
			mousePosition.x += 100;
		transform.position = Vector2.Lerp(transform.position, mousePosition, 1);
		transform.position += new Vector3 (0, 0, -5);
	}

	void OnMouseUp(){
		if (!UnityCommandInterface.TARGETING)
			return;

		UnityCommandInterface.TARGETING = false;

		LayerMask lm = 1 << LayerMask.NameToLayer ("CardBoard");
		Vector3 origin = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		origin.z = 0;
		RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 1, lm);

		if (hit.collider != null) {
			GameObject G = hit.collider.gameObject;
			BoardSquare BS = G.GetComponent<BoardSquare> ();

			Target T = null;

			if (BS.y == 2)
				return;
			if (BS.y == 4) {
				//player
				Player P = MainDriver.MAINTABLE.P1;
				if (!BS.Player)
					P = MainDriver.MAINTABLE.P2;
				T = new Target (P, null);
			}
			if(T == null){
				Character targetchar;
				if (BS.Player)
					targetchar = MainDriver.MAINTABLE.P1.GAMEBOARD.Board [BS.x, BS.y];
				else
					targetchar = MainDriver.MAINTABLE.P2.GAMEBOARD.Board [BS.x, BS.y];

				 T = new Target (null, targetchar);
			}

			UnityCommandInterface.ACTIVE.TARGET = T;
			MainDriver.MAINTABLE.CURRENTTURN
				.ActiveContinue (UnityCommandInterface.ACTIVE.TargetContinue(),
					UnityCommandInterface.ACTIVETARGETINGCARD, T);

		}
	}
}
