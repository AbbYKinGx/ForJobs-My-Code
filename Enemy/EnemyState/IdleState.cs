using UnityEngine;

public class IdleState :IEnemyState{
    private Enemy enemyThis;
    private float idleTime;
    private float idleExit;
    public void Enter(Enemy enemy){
        idleExit = Random.Range(2, 5);
        enemyThis = enemy;
    }
    public void Execute(){
        //Debug.LogError("Im Live and This idle");
        Idle();
        if (enemyThis.Target != null)
            enemyThis.ChangeStates(new PatrolState());
    }
    public void Exit(){
       
    }
    
    public void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "PlayerFire")
            enemyThis.Target = PlayerControl.Instance.gameObject;
    }

    private void Idle(){
        enemyThis.Anim.SetBool("now_move" , false);
        idleTime += Time.deltaTime;
        if (idleTime >= idleExit)
            enemyThis.ChangeStates(new PatrolState());
    }
}
