using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public class AnimationInterface
	{

		public TableTop TABLE;

		public AnimationInterface ()
		{
		}

		public void SetTable(TableTop TABLE){
			this.TABLE = TABLE;
		}

		public virtual void SwitchTurnBreak(){
			TABLE.SwitchTurnContinue ();
		}

		public virtual void AttackBreak(Character a, Character t, List<Character> Units){
			TABLE.AttackContinue (Units);
		}

		public virtual void SettleBreak(){
			TABLE.SettleState (TABLE.CURRENTTURN);
		}

		public virtual void TemporaryPlayActiveSound(String namecode){

		}

		public virtual void AttackStartBreak(){
			TABLE.FullAttackStart (TABLE.CURRENTTURN);
		}

		public virtual void PrepareNextTurn(){
			TABLE.SwitchTurn3 ();
		}

		public virtual void EndGameBreak(){

		}

		public virtual void ActiveAnimation(int x, Character character){

		}

		public virtual void TriggerEffectAnimation(TriggerPair T){
			TABLE.PopEffectContinue (T);
		}

		public virtual void TriggerAnimation(TriggerPacket TP){
			TABLE.SettleState1p5 ();
		}

		public virtual void AnimationLock (){

		}

		public virtual void AnimationUnlock(){

		}
	}
}

