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
 * PassiveAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/28/2016
 */

using System;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
* 
* 		THIS FILE IS A COLLECTION OF PASSIVE ABILITIES FOR CHARACTER EFFECTS
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TFP : Passive{
		public TFP() : base("", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP1 : Passive //Give Intercept
	{
		public TP1 () : base("PAD000001", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			if (user == target)
				return true;
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive2 : Passive //Give Intercept, Life + 1
	{
		public TestPassive2 () : base("PAD000002", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (target == user)
				return life + 1;
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			if (target == user)
				return true;
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive3 : Passive //Give attack + 2
	{
		public TestPassive3 () : base("PAD000003", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (target == user)
				return damage + 2;
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive4 : Passive //Give life + 1, attack + 1
	{
		public TestPassive4 () : base("PAD000004", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (user == target)
				return damage + 1;
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (user == target)
				return life + 1;
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive5 : Passive //Give Overkill
	{
		public TestPassive5 () : base("PAD000005", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			if (target == user)
				return true;
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive6 : Passive //Give Rooted
	{
		public TestPassive6 () : base("PAD000006", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			if (target == user)
				return true;
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TestPassive7 : Passive //Give attack + 1
	{
		public TestPassive7 () : base("PAD000007", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (target == user)
				return damage + 1;
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP8 : Passive{ //Armor+1 (for friendly)
		public TP8() : base("PAD000008", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			if (user.CONTROLLER == target.CONTROLLER)
				return armor + 1;
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP9 : Passive{ //Attack+3, Ranged Attack
		public TP9() : base("PAD000009", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (user == target)
				return damage + 3;
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			if (user == target)
				return false;
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP10 : Passive{ // +1 Max hand size
		public TP10() : base("PAD000010", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			if (user.CONTROLLER == PLAYER)
				return handsize + 1;
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP11 : Passive{ //Friendlies get +1 Armor
		public TP11() : base("PAD000011", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			if (user.CONTROLLER == target.CONTROLLER)
				return armor + 1;
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP12 : Passive{ //Friendlies get +1 Life
		public TP12() : base("PAD000012", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (user.CONTROLLER == target.CONTROLLER)
				return life + 1;
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP13 : Passive{ //+4 damage to units
		public TP13() : base("PAD000013", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			if (user == attacker && defender != null)
				return damage + 4;
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP14 : Passive{ //+1 life for each friendly corpse
		public TP14() : base("PAD000014", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (target == user) {
				int corpses = 0;
				for (int i = 0; i < TableTop.ROWS; i++) {
					for (int a = 0; a < TableTop.COLUMNS; a++) {
						if (user.CONTROLLER.GAMEBOARD.Board [a, i] != null) {
							if (user.CONTROLLER.GAMEBOARD.Board [a, i].Dead)
								corpses++;
						}
					}
				}
				return life + corpses;
			}
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP15 : Passive{ //Unit in front gets + 2 life
		public TP15() : base("PAD000015", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (target.CONTROLLER == user.CONTROLLER) {
				Point p1 = target.CONTROLLER.GAMEBOARD.LocateInBoard (target);
				Point p2 = user.CONTROLLER.GAMEBOARD.LocateInBoard (user);
				if (p2.y == 1 && p1.y == 0 && p1.x == p2.x) {
					return life + 2;
				}
			}
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP16 : Passive //Give intercept, life+1, armor+1
	{
		public TP16 () : base("PAD000016", 0)
		{
		}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			if (user == target)
				return life + 1;
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			if (user == target)
				return true;
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			if (user == target)
				return armor + 1;
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP17 : Passive{ //Friendlies get ranged attack
		/// <summary>
		/// Friendlies get Ranged Attack
		/// </summary>
		public TP17() : base("PAD000017", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			if (target.CONTROLLER == user.CONTROLLER) {
				return false;
			}
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP18 : Passive{ //Ranged Attack
		/// <summary>
		/// Ranged Attack
		/// </summary>
		public TP18() : base("PAD000018", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			if (user == target)
				return false;
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP19 : Passive{ //Not targetable
		/// <summary>
		/// Not Targetable
		/// </summary>
		public TP19() : base("PAD000019", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			if (user == target)
				return false;
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

	public class TP20 : Passive{ //Immune to spell damage
		/// <summary>
		/// Immune to spell Damage
		/// </summary>
		public TP20() : base("PAD000020", 0){}

		public override int ModifyDamage (int damage, Character target, Character user, Player PLAYER){
			if (user == target) {
				user.AddImmunity (Damage.TYPE.SPELL);
			}
			return damage;
		}

		public override int ModifyLife (int life, Character target, Character user, Player PLAYER){
			return life;
		}

		public override bool ModifyAttackType (bool melee, Character target, Character user, Player PLAYER){
			return melee;
		}

		public override bool RevealYourHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool RevealEnemyHand (bool reveal, Character user, Player PLAYER){
			return reveal;
		}

		public override bool ModifyIntercept (bool intercept, Character target, Character user, Player PLAYER){
			return intercept;
		}

		public override bool ModifyRooted (bool rooted, Character target, Character user, Player PLAYER){
			return rooted;
		}

		public override bool ModifyOverkill (bool overkill, Character target, Character user, Player PLAYER){
			return overkill;
		}

		public override int ModifyArmor (int armor, Character target, Character user, Player PLAYER){
			return armor;
		}

		public override bool ModifyZombie (bool zombie, Character target, Character user, Player PLAYER){
			return zombie;
		}

		public override int ModifyDamageToX (int damage, Character user, Character attacker, Character defender){
			return damage;
		}

		public override int ModifyMaxHandSize (int handsize, Character user, Player PLAYER){
			return handsize;
		}

		public override bool ModifyTargetable (bool targetable, Character target, Character user, Player PLAYER){
			return targetable;
		}

		public override bool ModifyPlayerTargetable (bool targetable, Character user, Player PLAYER){
			return targetable;
		}
	}

} //End namespace