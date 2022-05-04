using UnityEngine;

public class PatrolState : IEnemyState{
    private Enemy enemyThis;
    private float PatrolTime;
    private float PatrolExit;
    public void Execute(){
        PatrolExit = Random.Range(2, 5);
        Patrol();
           enemyThis.Move();
        if (enemyThis.Target != null && enemyThis.InKnifeRange)
        {
            enemyThis.ChangeStates(new RangedState());
            enemyThis.attack = true;
        }
    }
    public void Enter(Enemy enemy){
        enemyThis = enemy;
    }
    public void Exit(){
        
    }  
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "PlayerFire" || other.tag == "Sword")
            enemyThis.Target = PlayerControl.Instance.gameObject;
    }
    private void Patrol(){
        PatrolTime += Time.deltaTime;
        if (PatrolTime >= PatrolExit)
            enemyThis.ChangeStates(new IdleState());
    }
}
