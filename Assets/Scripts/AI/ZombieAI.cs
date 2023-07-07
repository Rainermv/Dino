using UnityEngine;
using System.Collections;
using Assets.Scripts.Model;

public class ZombieAI : AI {

	public override void StartAI ( EnemyComponent enemyComponent ){

		enemyComponent.Flip ();
	
	}

	public override void UpdateAI ( EnemyComponent enemyComponent  ){

		if (enemyComponent.CurrentCharacterState.animationType != CharacterAnimationType.Dead && enemyComponent.enemy.IsAlive == false) {

			//rb.AddForce (new Vector2(5000f, 0f));
			enemyComponent.SetState (CharacterAnimationType.Dead);
		}

	}
}
