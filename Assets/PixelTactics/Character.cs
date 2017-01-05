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
 * Character.cs
 * Author: Nic Wilson
 * Last updated: 3/28/2016
 */

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// An object representing a game Character
	/// This can basically be expressed as one of the cards in the game
	/// It will have properties like Life and Attack,
	/// be able to be under status effects, have certain keywords,
	/// and have different Abilities depending on where it is played from.
	/// </summary>
	public class Character
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Gets or sets the Armor value for this unit
		/// </summary>
		/// <value>The armor.</value>
		public int Armor {
			get {
				return (tarmor>0)?tarmor:0;
			}
			set{
				tarmor = value;
			}
		}

		/// <summary>
		/// Gets or sets the Attack power for this unit
		/// </summary>
		/// <value>The attack.</value>
		public int Attack{
			get{
				return m_attack;
			}
			set{
				m_attack = value;
			}
		}

		public char AttackColor{
			get{
				if (Attack > _attack)
					return 'g';
				else
					return 'w';
			}
		}

		/// <summary>
		/// The Base Armor value for this unit
		/// </summary>
		/// <value>The base armor.</value>
		public int BaseArmor {
			get {
				return armor;
			}
		}

		/// <summary>
		/// The Base Attack value for this unit
		/// </summary>
		/// <value>The base attack.</value>
		public int BaseAttack{
			get{
				return attack;
			}
			set{
				attack = value;
			}
		}

		/// <summary>
		/// The Base Life value for this unit
		/// </summary>
		/// <value>The base life.</value>
		public int BaseLife{
			get{
				return maxhealth;
			}
		}

		/// <summary>
		/// The Base Intercept value for this unit
		/// </summary>
		/// <value><c>true</c> if base intercept; otherwise, <c>false</c>.</value>
		public bool BaseIntercept{
			get{
				return intercept;
			}
		}

		/// <summary>
		/// The Base Attack type for this unt
		/// </summary>
		/// <value><c>true</c> if base ismelee; otherwise, <c>false</c>.</value>
		public bool BaseIsMelee{
			get{
				return melee;
			}
		}

		/// <summary>
		/// The Base Overkill value for this unit
		/// </summary>
		/// <value><c>true</c> if base overkill; otherwise, <c>false</c>.</value>
		public bool BaseOverkill {
			get {
				return overkill;
			}
		}

		/// <summary>
		/// The Base Rooted value for this unit
		/// </summary>
		/// <value><c>true</c> if base rooted; otherwise, <c>false</c>.</value>
		public bool BaseRooted {
			get {
				return rooted;
			}
		}

		public List<String> BaseTypes {
			get {
				return this._types;
			}
		}

		/// <summary>
		/// The Base Zombie value for this unt
		/// </summary>
		/// <value><c>true</c> if base zombie; otherwise, <c>false</c>.</value>
		public bool BaseZombie{
			get{
				return zombie;
			}
		}

		public int CardAttack {
			get {
				return _attack;
			}
		}

		public Ability CardHandAbility {
			get {
				return _handability;
			}
		}

		public int CardLife {
			get {
				return _maxhealth;
			}
		}

		public List<Passive>[] CardPassives {
			get {
				return _passives;
			}
		}

		public List<Trigger>[] CardTriggers{
			get{
				return _triggers;
			}
		}

		/// <summary>
		/// Clears the enhancements
		/// </summary>
		public void ClearEnhancements(){
			enhancements.Clear ();
		}

		/// <summary>
		/// How much Damage this unit has on it
		/// </summary>
		/// <value>The damage.</value>
		public int Damage{
			get{
				return damage;
			}
		}

		/// <summary>
		/// Whether or not this unit is dead
		/// </summary>
		/// <value><c>true</c> if dead; otherwise, <c>false</c>.</value>
		public bool Dead{
			get{
				return isdead;
			}
		}

		/// <summary>
		/// Gets the enhancements on this Character
		/// </summary>
		/// <value>The enhancements.</value>
		public List<Enhancement> Enhancements {
			get {
				List<Enhancement> ret = new List<Enhancement> (
					                        baseenhancements);
				ret.AddRange (enhancements);
				return ret;
			}
		}
	
		/// <summary>
		/// Whether or not this unit has Intercept
		/// </summary>
		/// <value><c>true</c> if intercept; otherwise, <c>false</c>.</value>
		public bool Intercept{
			get{
				return m_intercept;
			}
			set{
				m_intercept = value;
			}
		}

		/// <summary>
		/// Whether or not this unit can only attack in Melee
		/// If false this unit has ranged attack
		/// </summary>
		/// <value><c>true</c> if this instance is melee; otherwise,
		///  <c>false</c>.</value>
		public bool IsMelee {
			get {
				return m_melee;
			}
			set{
				m_melee = value;
			}
		}

		/// <summary>
		/// How much life this unit has
		/// </summary>
		/// <value>The life.</value>
		public int Life{
			get{
				return m_maxhealth;
			}
			set{
				m_maxhealth = value;
			}
		}

		public char LifeColor{
			get{
				if (RemainingLife > _maxhealth && Damage < _maxhealth)
					return 'g';
				else if (RemainingLife < _maxhealth)
					return 'r';
				else
					return 'w';
			}
		}

		/// <summary>
		/// Returns the Name of the Unit (from the Language table)
		/// </summary>
		/// <value>The name.</value>
		public string Name{
			get{
				return CONTROLLER.TABLE.STRINGTABLE.Get (NameCode);
			}
		}

		/// <summary>
		/// The Name String Code for the Unit
		/// </summary>
		/// <value>The name code.</value>
		public string NameCode{
			get{
				return name;
			}
		}

		/// <summary>
		/// Whether or not this unit has the Overkill keyword
		/// </summary>
		/// <value><c>true</c> if overkill; otherwise, <c>false</c>.</value>
		public bool Overkill{
			get{
				return m_overkill;
			}
			set{
				m_overkill = value;
			}
		}

		/// <summary>
		/// Gets the rarity.
		/// </summary>
		/// <value>The rarity.</value>
		public int Rarity{
			get{
				return rarity;
			}
		}

		/// <summary>
		/// How much Life this unit has remaining
		/// </summary>
		/// <value>The remaining life.</value>
		public int RemainingLife {
			get {
				int i = m_maxhealth - damage;
				return (i < 0) ? 0 : i;
			}
		}

		/// <summary>
		/// Whether or not this unit is Rooted
		/// </summary>
		/// <value><c>true</c> if rooted; otherwise, <c>false</c>.</value>
		public bool Rooted{
			get{
				return m_rooted;
			}
			set{
				m_rooted = value;
			}
		}

		/// <summary>
		/// Whether or not this unit is Stunned
		/// </summary>
		/// <value><c>true</c> if stunned; otherwise, <c>false</c>.</value>
		public bool Stunned{
			get{
				return stunned;
			}
			set{
				//Become stunned if not immune to stun
				if (!IMMUNITIES.Contains (Tactics_CoreGameEngine.Damage.TYPE.STUN))
					stunned = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this 
		/// <see cref="Tactics_CoreGameEngine.Character"/> is targetable.
		/// </summary>
		/// <value><c>true</c> if targetable; otherwise, <c>false</c>.</value>
		public bool Targetable{
			get{
				return targetable;
			}
			set{
				targetable = value;
			}
		}

		/// <summary>
		/// Whether or not this unit is Zombified
		/// </summary>
		/// <value><c>true</c> if Zombified; otherwise, <c>false</c>.</value>
		public bool Zombie {
			get {
				return m_zombie;
			}
			set{
				m_zombie = value;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The Hand Ability for this object
		/// </summary>
		public Ability HandAbility;

		/// <summary>
		/// Whether or not this ability has the Gravedigger keyword
		/// </summary>
		public bool Gravedigger = false;
		/// <summary>
		/// Whether or not this Character has moved this turn
		/// </summary>
		public bool Moved = true;
		/// <summary>
		/// Whether or not this unit has modified damage
		/// </summary>
		public bool ModifiedDamage = false;
		/// <summary>
		/// Whether or not this Character has the Upgrade keyword
		/// </summary>
		public bool Upgrade = false;
		/// <summary>
		/// Whether or not this unit's trap can trigger
		/// </summary>
		public bool TrapArmed = false;
	
		/// <summary>
		/// The Modifier value for Gravedigger+
		/// </summary>
		public int GravediggerPlus = 0;
		/// <summary>
		/// The Modifier value for Upgrade+
		/// </summary>
		public int UpgradePlus = 0;

		public bool TAKENDAMAGE = false;
		public int TAKENDAMAGEVALUE = 0;
		public bool TAKENHEALS = false;
		public int TAKENHEALSVALUE = 0;
	
		/// <summary>
		/// The matrix of Triggers for this Character
		/// </summary>
		public List<Trigger>[] Triggers = new List<Trigger>[2];
		/// <summary>
		/// The matrix of Passives for this Character
		/// </summary>
		public List<Passive>[] Passives = new List<Passive>[2];

		/// <summary>
		/// The Player that Controls this unit
		/// </summary>
		public Player CONTROLLER;

		//----------------------------------------------------------------
	
		private bool melee, m_melee, stunned = false, intercept = false,
			m_intercept = false, rooted = false, m_rooted = false,
			overkill = false, m_overkill = false, zombie = false,
			m_zombie = false, isdead = false, targetable = true;

		/// <summary>
		/// The Damage Immunities this Character has
		/// </summary>
		private List<Damage.TYPE> IMMUNITIES = new List<Tactics_CoreGameEngine.Damage.TYPE> ();

		private List<Enhancement> enhancements = new List<Enhancement> ();
		private List<Enhancement> baseenhancements = new List<Enhancement> ();


		private List<String> types, _types;

		private List<Trigger>[] _triggers = new List<Trigger>[2];

		private List<Passive>[] _passives = new List<Passive>[2];

		private Ability _handability;

		private int armor = 0, tarmor = 0, maxhealth, damage, attack,
			m_attack, m_maxhealth, _attack, _maxhealth;

		private string name;

		private int rarity;

		//----------------------------------------------------------------

		/// <summary>
		/// Main Constructor
		/// Initializes the object and sets the variables and the backup
		/// variables
		/// </summary>
		/// <param name="CONTROLLER">The Player that controls this</param>
		/// <param name="name">This unit's name</param>
		/// <param name="attack">This unit's attack</param>
		/// <param name="maxhealth">This unit's health</param>
		/// <param name="typearray">A list of this unit's Types</param>
		public Character (Player CONTROLLER, string name, int attack,
			int maxhealth, int rarity, params string[] typearray)
		{
			//Initialize variables
			this.CONTROLLER = CONTROLLER;
			this.name = name;
			this.attack = attack;
			this._attack = attack;
			this.m_attack = attack;
			this.maxhealth = maxhealth;
			this.m_maxhealth = maxhealth;
			this._maxhealth = maxhealth;
			this.melee = true;
			this.m_melee = true;
			this.types = new List<string>(typearray);
			this._types = new List<string> (typearray);
			this.rarity = rarity;

			Triggers [0] = new List<Trigger> ();
			Triggers [1] = new List<Trigger> ();
			_triggers [0] = new List<Trigger> ();
			_triggers [1] = new List<Trigger> ();
			Passives [0] = new List<Passive> ();
			Passives [1] = new List<Passive> ();
			_passives [0] = new List<Passive> ();
			_passives [1] = new List<Passive> ();
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Adds a base enhancement.
		/// </summary>
		/// <param name="enhancement">An Enhancement.</param>
		public void AddBaseEnhancement(Enhancement enhancement){
			this.baseenhancements.Add (enhancement);
		}

		/// <summary>
		/// Adds Damage to this Unit
		/// </summary>
		/// <param name="D">The Damage object</param>
		public void AddDamage(Damage D){
			//Return if this Character is immune to the Damage Type
			if (IMMUNITIES.Contains (D.Type) || Dead)
				return;

			//Get the amount of Damage
			int DV = D.Value - Armor;

			//Armor doesn't stop spell damage
			if (D.Type == Tactics_CoreGameEngine.Damage.TYPE.SPELL) DV = D.Value;

			//Cap the damage value at 0
			if (DV < 0)
				DV = 0;

			//Apply the damage to the unit
			damage += DV;

			//Flag for damage
			TAKENDAMAGE = true;
			TAKENDAMAGEVALUE += DV;

			//Add a Unit damage trigger if this unit took damage
			if(DV > 0)
				CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket
					(Trigger.TYPE.UNITDAMAGE, CONTROLLER,
						D.Source, null, this));
		}

		/// <summary>
		/// Adds a enhancement.
		/// </summary>
		/// <param name="enhancement">An Enhancement.</param>
		public void AddEnhancement(Enhancement enhancement){
			this.enhancements.Add (enhancement);
		}

		/// <summary>
		/// Adds a Damage Immunity to the Character
		/// </summary>
		/// <param name="type">The Damage Type</param>
		public void AddImmunity(Damage.TYPE type){
			if(!IMMUNITIES.Contains(type))
				IMMUNITIES.Add (type);
		}

		/// <summary>
		/// Adds a Passive to the specified row
		/// </summary>
		/// <param name="p">The Passive</param>
		/// <param name="r">The specified row</param>
		public void AddPassive(Passive p, int r){
			this.Passives [r].Add(p);
		}

		/// <summary>
		/// Add a Trigger to the specified row
		/// Mainly used for external effects
		/// </summary>
		/// <param name="t">The Trigger</param>
		/// <param name="r">The specified row</param>
		public void AddTrigger(Trigger t, int r){
			this.Triggers [r].Add(t);
		}

		/// <summary>
		/// Adds a Type to the Character
		/// </summary>
		/// <param name="t">The Type</param>
		public void AddType(string t){
			if(!types.Contains(t))
				types.Add(t);
		}
			
		/// <summary>
		/// Checks if this unit is dead.
		/// Triggers "Just died" events and a "Create Corpose" if it did
		/// </summary>
		public void CheckDead(){
			//If damage exceeds health
			if (damage >= Life) {
				if (!isdead) { //Just died
					//Make trigger for dying
					CONTROLLER.TABLE.PIPELINE.Add(new TriggerPacket(
						Trigger.TYPE.UNITDIED,CONTROLLER,this,
						CONTROLLER));
					CONTROLLER.TABLE.PIPELINE.Add(new TriggerPacket(
						Trigger.TYPE.CREATECORPSE,CONTROLLER,this,
						CONTROLLER));
					//Reset permanent stat increases
					ResetBaseStats ();
					//Flag that this unit is dead
					isdead = true;
				}
			}
		}

		/// <summary>
		/// Copy this Character with a new Controller
		/// </summary>
		/// <param name="P">The new controller Player</param>
		public Character Copy(Player P){
			//Make a copy of the Types list
			List<string> ntypes = new List<string> ();
			foreach (string t in _types)
				ntypes.Add (t);
			
			//Create a copy of this Character
			Character c = new Character (
			              P,
			              this.name,
			              this._attack,
			              this._maxhealth,
				      this.rarity,
				      ntypes.ToArray());
			//Return the copy
			return c;
		}

		/// <summary>
		/// Heals the for the specified amount
		/// </summary>
		/// <param name="D">The Damage object to heal with</param>
		public void HealDamage(Damage D){
			//If Zombified, reverse the healing effect
			if(Dead) return;

			if (Zombie)
				D.Value = Math.Abs(D.Value) * -1;

			//If Immune to healing, return
			if (IMMUNITIES.Contains (D.Type))
				return;

			//Remove damage equal to the healing amount
			damage -= D.Value;

			//Flag for damage
			TAKENHEALS = true;
			TAKENHEALSVALUE += D.Value;

			//If less than 0 damage, clamp it
			if (damage < 0)
				damage = 0;

			//Add a Unit Heal trigger to the pipeline
			CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket (
				Trigger.TYPE.UNITHEAL,CONTROLLER, D.Source,
				null, this));
		}

		/// <summary>
		/// Removes the specified immunity type
		/// </summary>
		/// <param name="type">The Damage type to remove</param>
		public void RemoveImmunity(Damage.TYPE type){
			IMMUNITIES.Remove (type);
		}

		/// <summary>
		/// Removes the specified passive.
		/// </summary>
		/// <param name="p">The Passive</param>
		/// <param name="r">The row to remove from</param>
		public void RemovePassive(Passive p, int r){
			this.Passives [r].Remove(p);
		}

		/// <summary>
		/// Removes the specified trigger
		/// </summary>
		/// <param name="t">The Trigger</param>
		/// <param name="r">The row to remove from</param>
		public void RemoveTrigger(Trigger t, int r){
			this.Triggers [r].Remove(t);
		}

		/// <summary>
		///  Removes the specified Type
		/// </summary>
		/// <param name="t">The Type</param>
		public void RemoveType(string t){
			types.Remove (t);
		}

		/// <summary>
		/// Resets the base stats of this unit
		/// Used for modifying base stats for permanent bonuses
		/// 	that reset on death
		/// </summary>
		public void ResetBaseStats(){
			attack = _attack;
			maxhealth = _maxhealth;
			armor = 0;
			zombie = false;
			intercept = false;
			rooted = false;
			overkill = false;
			IMMUNITIES.Clear ();
			types = _types;
			Passives = _passives;
			Triggers = _triggers;
			HandAbility = _handability;
			enhancements.Clear ();
			baseenhancements.Clear ();
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the
		///  current <see cref="PixelTactics.Character"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents
		///  the current <see cref="PixelTactics.Character"/>.</returns>
		public override string ToString ()
		{
			if (Dead)
				return "CORPSE (" + Name + ")";
			return "Name: " + Name + "\t" +
				"ATK/HP/DMG: " + m_attack + (m_melee?"":"R") +
				(overkill?"O":"") + " " + m_maxhealth + (intercept?"I":"")
				+ (Armor>0?" "+Armor+"A":"") + " " + damage;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Adds the a Hand ability to this card.
		/// Mostly used for derived classes.
		/// </summary>
		/// <param name="a">The new Hand ability</param>
		protected void _AddHandAbility(Ability a){
			this.HandAbility = a;
			this._handability = a;
		}

		/// <summary>
		/// Adds a passive to this Character in the specified row
		/// Mostly used for derived classes.
		/// </summary>
		/// <param name="p">The new Passive</param>
		/// <param name="r">The row to add it to</param>
		protected void _AddPassive(Passive p, int r){
			this.Passives [r].Add(p);
			this._passives [r].Add(p);
		}

		/// <summary>
		/// Adds a Trigger to this Character in the specified row
		/// Mostly used for derived classes.
		/// </summary>
		/// <param name="t">The trigger to add</param>
		/// <param name="r">The row to add it to</param>
		protected void _AddTrigger(Trigger t, int r){
			this.Triggers [r].Add(t);
			this._triggers [r].Add(t);
		}


	} // End Character class

} // End namespace
	