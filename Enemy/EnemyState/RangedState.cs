using UnityEngine;

public class RangedState : IEnemyState{
    private Enemy _enemy;
    private float TimeFire;
    private float TimeFireReset = 1.4f;
    public void Execute(){
        FireBall();
        if(_enemy.inMeleeRange)
            _enemy.ChangeStates(new MeleeState()); 
        else if (_enemy.Target != null)
            _enemy.Move();
        else
            _enemy.ChangeStates(new IdleState());
    }
    public void Enter(Enemy enemy){
        _enemy = enemy;
    }
    public void Exit(){
      
    }
    public void OnTriggerEnter2D(Collider2D other){
    }
    private void FireBall(){
        TimeFire += Time.deltaTime;
        if (TimeFire >= TimeFireReset){
            TimeFire = 0f;
            _enemy.attack = true;
            _enemy.Anim.SetTrigger("Cast");
            _enemy.Anim.SetBool("now_move" , false);
        }
        else
            _enemy.attack = false;
    }
}
