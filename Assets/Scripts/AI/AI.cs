using UnityEngine;
using System.Collections;

public abstract class AI {

	public abstract void StartAI ( EnemyComponent enemy );
	public abstract void UpdateAI ( EnemyComponent enemy  );

}
