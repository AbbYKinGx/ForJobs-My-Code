using UnityEngine;

public interface IEnemyState
{
    void Execute();
    void Enter(Enemy enemy);
    void Exit();
    void OnTriggerEnter2D(Collider2D other);
}
