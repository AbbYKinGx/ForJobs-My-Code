using System.Collections;
using UnityEngine;

public class Enemy : Characters{
    private IEnemyState _currState;
    public GameObject Target { get; set; }
    [SerializeField] 
    private float meleeRandge;
    [SerializeField] 
    private float knifeRandge;
    internal bool attack;
    [SerializeField] 
    private Transform leftEdge; 
    [SerializeField] 
    private Transform RightEdge; 
    public bool inMeleeRange {
        get {
            if (Target != null)
                return Vector2.Distance(transform.position, Target.transform.position) <= meleeRandge;
            return false;
        }    
    }
    public bool InKnifeRange {
        get {
            if (Target != null)
                return Vector2.Distance(transform.position, Target.transform.position) <= knifeRandge;
            return false;
        }
    }
    public override bool IsDead
    {
        get { return  Health <= 0; }
    }
    public override void Start(){
        base.Start();
        if(PlayerControl.Instance != null)
            PlayerControl.Instance.Dead += new DeadEvent(RemoveTarget);
        ChangeStates(new IdleState());
    }

    public override void Death()
    {
       Instantiate(CoinsManager.Instance.CoinPrefab,new Vector3(transform.position.x , transform.position.y + 1) , Quaternion.identity);
       Destroy(gameObject);  
    }

    protected override IEnumerator TakeDamage()
    {
        Health -= 10;
        if(!IsDead)
            Anim.SetTrigger("Damage");
        else
        {
            Anim.SetTrigger("Dead");
            yield return null;
        }
    }

    private void Update(){
        health.SetValueBar(Health , 100);
        if (DestroyFire)
        {
            FireDestroy.SetActive(false);
            DestroyFire = false;
        }
        if (IsDead)
            return;
        if (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("KinGx_Damage") && _currState != null)
            _currState.Execute();
        LookTarget();
    }
    public void ChangeStates(IEnemyState newState) {
        if (_currState != null)
            _currState.Exit();
        _currState = newState;
        _currState.Enter(this);
    }
    private void LookTarget(){
        if (Target == null) return;
        float xDir = Target.transform.position.x - transform.position.x;
        
        Anim.SetBool("now_move" , true);
        if (xDir > 0 && IsRight || xDir < 0 && !IsRight)
            ChangeDir();
    }

    public void RemoveTarget()
    {
        Target = null;
        ChangeStates(new PatrolState());
    }
    public void Move(){
        if(attack) 
            return;
        if (!IsRight && transform.position.x <= RightEdge.position.x || IsRight && transform.position.x >= leftEdge.position.x){
            Anim.SetBool("now_move", true);
            transform.Translate(Vector2.right * (MoveSpeed * Time.deltaTime / 2));
        }
        else if(_currState is PatrolState)
            ChangeDir();
        else if (_currState is RangedState)
        {
            Target = null;
            ChangeStates(new IdleState());
        }
    }
    
    public override void OnTriggerEnter2D(Collider2D other){
        base.OnTriggerEnter2D(other);
        _currState.OnTriggerEnter2D(other);
        if (other.CompareTag("PlayerFire"))
            FireDestroy.SetActive(true);
    }
}
