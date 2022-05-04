using UnityEngine;

public class MeleeState : IEnemyState
{
    private Enemy _enemy;
    private float _timeAttack;
    private float TimeAttackReset =0.9f;
    public void Execute()
    {
        Attacks();
        if(_enemy.InKnifeRange && !_enemy.inMeleeRange)
            _enemy.ChangeStates(new RangedState());
        else if(_enemy.Target == null)
            _enemy.ChangeStates(new IdleState());
    }

    public void Enter(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void Exit()
    {
       
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       
    }
    private void Attacks(){
        _timeAttack += Time.deltaTime;
        if (_timeAttack >= TimeAttackReset)
        {
            _enemy.attack = true;
            _timeAttack = 0f;
            _enemy.Anim.SetTrigger("Attack");
            _enemy.Anim.SetBool("now_move" , false);
        }
        else
            _enemy.attack = false;
    }
}
