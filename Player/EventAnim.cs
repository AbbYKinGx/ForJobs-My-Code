using UnityEngine;

public class EventAnim : MonoBehaviour{
    private PlayerControl player;
    private Enemy _enemy;
    private void Awake(){
        player = GetComponentInParent<PlayerControl>();
        _enemy = GetComponentInParent<Enemy>();
    }
    private void MoveEvent() {
        if(player != null && !player.isWind)
                player.MoveSpeed = 14f;
        else if( player.isWind)
            player.MoveSpeed = 8f;
    }
    private void RemoveMoveEvent() {
        player.MoveSpeed = 0;
    }
    private void RemoveMoveEnemyEvent() {
        _enemy.MoveSpeed = 0;
    }
    private void MoveEnemyEvent() {
        if(_enemy != null)
            _enemy.MoveSpeed = 4.5f;
    }
    private void RemoveCapsule(){
/*        var capsuleSize = player.Capsule.size;
        var capsuleOffset = player.Capsule.offset;
        capsuleOffset.y = -1.162902f;
        capsuleSize.y= 1.252511f;
        player.Capsule.offset = capsuleOffset;
        player.Capsule.size = capsuleSize;*/
    }
    private void AddCapsule(){
/*        var capsuleSize = player.Capsule.size;
        var capsuleOffset = player.Capsule.offset;
        capsuleOffset.y= -0.1027589f;
        capsuleSize.y = 3.372798f;
        player.Capsule.offset = capsuleOffset;
        player.Capsule.size = capsuleSize;*/
    }
    private void isAttackFireballAdd(){
        player.FireBall(1);
    }
    private void isAttackFireEnemy(){
       _enemy.FireBall(1);
    }
    public void MeeleAttackEnemyFalse(){
       if(_enemy.SwordCollider2D.enabled ) 
           _enemy.SwordCollider2D.enabled = false;
    }
    public void MeeleAttackPlayerFlase(){
        if(player.SwordCollider2D.enabled ) 
            player.SwordCollider2D.enabled = false;
    }
    public void MeeleAttackEnemyTrue(){
        if(!_enemy.SwordCollider2D.enabled ) 
            _enemy.SwordCollider2D.enabled = true;
    }
    public void MeeleAttackPlayerTrue(){
        if(!player.SwordCollider2D.enabled ) 
            player.SwordCollider2D.enabled = true;
    }

    public void DeadEvent()
    {
        player.Capsule.enabled = false;
    }
    public void LiveEvent()
    {
        player.Capsule.enabled = true;
    }

    public void PlayerDead()
    {
        CoinsManager.Instance.CollectedHealths--;
    }
    public void FireDestroy()
    {
        player.DestroyFire = true;
    }
    public void FireballDestroy()
    {
        Fireball.Instance.Destroy = true;
    }
    public void EnemyFireDestroy()
    {
        _enemy.DestroyFire = true;
    }
    public void PlayerDash()
    {
        player.IsDash = true;
    }

    public void StopDash(){
        player.direction = 0;
        player.move = false;
    }

    public void AddMoney()
    {
        PlayerControl.Instance.PlayerGame.SetActive(false);
        PlayerControl.Instance.AddMoney = true;
        Application.LoadLevel("CompleteLevel");
        MenuManager._nextLevel = true;
        MenuManager.Level =  MenuManager.Level +1;
    }
} 
