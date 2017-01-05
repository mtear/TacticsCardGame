/*************************************************************************
 * 
 * FELICITY CONFIDENTIAL
 * __________________
 * 
 *  [2016] - [2016] Felicity Entertainment
 *  All Rights Reserved.
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Felicity Entertainment and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Felicity Entertainment and its 
 * suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or 
 * copyright law. Dissemination of this information or reproduction
 * of this material is strictly forbidden unless prior written
 * permission is obtained from Felicity Entertainment.
 */

/*
* StringTable.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class StringTable
	{
		private Dictionary<string, string> MASTERTABLE;

		private String language;
		public String LANGUAGE {
			get {
				return language;
			}
		}

		public StringTable(String L, Player P){
			this.language = L;
			Initialize (L, P);
		}

		public void Initialize(String L, Player P){
			if (MASTERTABLE == null) {
				MASTERTABLE = new Dictionary<string, string> ();

				//Add dictionaries
				MASTERTABLE = Load(L, P);
			}
		}

		private Dictionary<string, string> Load(String LanguageCode, Player P){
			Dictionary<string, string> MASTER = new Dictionary<string, string> ();
			Dictionary<string, string> LOOKUP = GetDictionary (LanguageCode);

			MASTER.Add ("FOOBAR", LOOKUP ["FOOBAR"]);
			MASTER.Add ("ONGOING EFFECT", LOOKUP ["ONGOING EFFECT"]);
			MASTER.Add ("Upgrade", LOOKUP ["Upgrade"]);
			MASTER.Add ("Gravedigger", LOOKUP ["Gravedigger"]);
			MASTER.Add ("WIN", LOOKUP ["WIN"]);
			MASTER.Add ("LOSE", LOOKUP ["LOSE"]);
			MASTER.Add ("FR", LOOKUP ["FR"]);
			MASTER.Add ("RR", LOOKUP ["RR"]);
			MASTER.Add ("HA", LOOKUP ["HA"]);
			for (int j = 0; j < 2; j++) {
				foreach (Character c in P.DECK.Contents) {
					if (!MASTER.ContainsKey (c.NameCode))
						MASTER.Add (c.NameCode, LOOKUP [c.NameCode]);
					foreach (string type in c.BaseTypes) {
						if (!MASTER.ContainsKey (type))
							MASTER.Add (type, LOOKUP [type]);
					}
					for (int i = 0; i < TableTop.ROWS; i++) {
						foreach (Passive p in c.Passives[i]) {
							if (!MASTER.ContainsKey (p.DescriptionCode))
								MASTER.Add (p.DescriptionCode, LOOKUP [p.DescriptionCode]);
							foreach (Ability a in p.CreatableAbilities()) {
								if (!MASTER.ContainsKey (a.DescriptionCode))
									MASTER.Add (a.DescriptionCode, LOOKUP [a.DescriptionCode]);
							}
							foreach (Character a in p.CreatableCharacters()) {
								if (!MASTER.ContainsKey (a.NameCode))
									MASTER.Add (a.NameCode, LOOKUP [a.NameCode]);
							}
						}
					}
					for (int i = 0; i < TableTop.ROWS; i++) {
						foreach (Trigger t in c.Triggers[i]) {
							if (!MASTER.ContainsKey (t.DescriptionCode))
								MASTER.Add (t.DescriptionCode, LOOKUP [t.DescriptionCode]);
							foreach (Ability a in t.CreatableAbilities()) {
								if (!MASTER.ContainsKey (a.DescriptionCode))
									MASTER.Add (a.DescriptionCode, LOOKUP [a.DescriptionCode]);
							}
							foreach (Character a in t.CreatableCharacters()) {
								if (!MASTER.ContainsKey (a.NameCode))
									MASTER.Add (a.NameCode, LOOKUP [a.NameCode]);
							}
						}
					}
					if (c.HandAbility != null && !MASTER.ContainsKey (c.HandAbility.DescriptionCode))
						MASTER.Add (c.HandAbility.DescriptionCode, LOOKUP [c.HandAbility.DescriptionCode]);
				}
				P = P.ENEMY;
			}

			return MASTER;
		}

		private static Dictionary<string, string> GetDictionary(String LanguageCode){
			//Dictionary
			Dictionary<string, string> D = null;

			if (LanguageCode == "EN") D = EN ();
			if (LanguageCode == "ES") D = ES ();

			if (D == null)
				D = EN ();

			return D;
		}

		private static Dictionary<string, string> EN(){
			//Dictionary
			Dictionary<string, string> D = new Dictionary<string, string> ();

			//null value
			D.Add("FOOBAR", "(none)");

			//In game text
			D.Add("WIN", "YOU WIN");
			D.Add ("LOSE", "<color=cyan>YOU LOSE</color>");
			D.Add("ONGOING EFFECT", "ONGOING EFFECT");
			D.Add ("FR", "FRONT ROW");
			D.Add ("RR", "BACK ROW");
			D.Add ("HA", "HAND ABILITY");

			//Keywords
			D.Add("Upgrade", "Upgrade");
			D.Add("Gravedigger", "Gravedigger");

			//Passive abilities' descriptions
			D.Add ("PAD000001", "<b>Intercept</b>");
			D.Add ("PAD000002", "<b>Intercept</b>\nLife+1");
			D.Add ("PAD000003", "Attack+2");
			D.Add ("PAD000004", "Life+1\nAttack+1");
			D.Add ("PAD000005", "<b>Overkill</b>");
			D.Add ("PAD000006", "<b>Rooted</b>");
			D.Add ("PAD000007", "Attack+1");
			D.Add ("PAD000008", "Armor+1");
			D.Add ("PAD000009", "Attack+3, Ranged Attack");
			D.Add ("PAD000010", "Max Hand Size + 1");
			D.Add ("PAD000011", "Friendlies get Armor+1");
			D.Add ("PAD000012", "Friendlies get Life+1");
			D.Add ("PAD000013", "+4 Damage to Units");
			D.Add ("PAD000014", "Life+1 for each Friendly Corpse");
			D.Add ("PAD000015", "Front Unit gets Life+2");
			D.Add ("PAD000016", "<b>Intercept</b>\nLife+1\nArmor+1");
			D.Add ("PAD000017", "Ranged Attack");
			D.Add ("PAD000018", "Ranged Attack");
			D.Add ("PAD000019", "Not Targetable");
			D.Add ("PAD000020", "Immune to Spell Damage");

			//Trigger abilities' descriptions
			D.Add("TA000001", "<b>Recruit:</b> Draw a card. Take 1 Damage");
			D.Add ("TA000002", "<b>Trap:</b> Do 3 Damage to a Recruited Front Enemy Unit");
			D.Add ("TA000003", "Attack+1 when an Enemy is Recruited");
			D.Add ("TA000004", "Draw a card at start of turn");
			D.Add ("TA000005", "Heal 2 at start of turn");
			D.Add ("TA000006", "<b>Trap:</b> A Random Enemy takes 2 Damage when Opponent Clears Corpse");
			D.Add ("TA000007", "<b>Trap:</b> Discard a card and Counter a trap");
			D.Add ("TA000008", "Friendlies Heal 1 when Enemy plays a Trap");
			D.Add ("TA000009", "Heal 1 Player Damage when you Draw a Card");

			//Ongoing abilities' descriptions
			D.Add("TO000001", "ONGOING 3: Friendlies get Armor 1");
			D.Add ("TO000002", "ONGOING 3: Friendlies get Ranged Attack");

			//Active abilities' descriptions
			D.Add("AA000001", "Draw a card");
			D.Add("AA000002", "Enemy discards a card. Draw a card");
			D.Add("AA000003", "Do 2 Damage to a Unit");
			D.Add ("AA000004", "Do 2 Damage to All Units");
			D.Add ("AA000005", "Heal a Player for 3 and they Discard a Card");
			D.Add ("AA000006", "Destroy a Trap from each Player");

			//Unit names
			D.Add ("UN000001", "Basic Adventurer");
			D.Add ("UN000002", "Basic Warrior");
			D.Add ("UN000003", "Basic Slime");
			D.Add ("UN000004", "Basic Ogre");
			D.Add ("UN000005", "Basic Wall");
			D.Add ("UN000009", "Elf Mystic");
			D.Add ("UN000010", "Commando");
			D.Add ("UN000011", "Bubba");
			D.Add ("UN000012", "Angry Activist ♀");
			D.Add ("UN000014", "Sneaky Trapper");
			D.Add ("UN000019", "Paper Boy");
			D.Add ("UN000020", "Bone Bully");
			D.Add ("UN000024", "Power Lass");
			D.Add ("UN000025", "Shoto Fighter");
			D.Add ("UN000026", "Business Ferret");
			D.Add ("UN000027", "Lift Bro");
			D.Add ("UN000028", "Lumberjacked");
			D.Add ("UN000029", "Commando 2");
			D.Add ("UN000031", "Nurse Missy");

			//Types
			D.Add("TYP000001", "Basic");
			D.Add("TYP000002", "Human");
			D.Add ("TYP000003", "Slime");
			D.Add ("TYP000004", "Monster");
			D.Add ("TYP000005", "Wall");
			D.Add ("TYP000006", "Elf");
			D.Add ("TYP000007", "Super");
			D.Add ("TYP000008", "Undead");
			D.Add ("TYP000009", "Activist");
			D.Add ("TYP000010", "Business");
			D.Add ("TYP000011", "MMA");
			D.Add ("TYP000012", "Buff");

			return D;
		}

		private static Dictionary<string, string> ES(){
			//Dictionary
			Dictionary<string, string> D = new Dictionary<string, string> ();

			//null value
			D.Add("FOOBAR", "(ninguna)");

			//Passive abilities' descriptions
			D.Add ("PA000001", "<b>Intercepción</b>");

			//Trigger abilities' descriptions

			//Ongoing abilities' descriptions

			//Unit names
			D.Add ("UN000001", "Aventurero");
			D.Add ("UN000002", "Guerrero");
			D.Add ("UN000003", "Machote");
			D.Add ("UN000004", "Ciudadano");

			return D;
		}

		public String Get(String C){
			return MASTERTABLE [C];
		}
	} // End StringTable class

} // End namespace

