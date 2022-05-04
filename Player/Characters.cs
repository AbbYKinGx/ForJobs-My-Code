using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Characters : MonoBehaviour{
    protected internal Animator Anim;
    internal CapsuleCollider2D Capsule;
    [SerializeField]
    protected Bar health;
    [SerializeField] 
    internal EdgeCollider2D SwordCollider2D;
    protected Rigidbody2D Rb;
    protected bool IsGround;
    public float MoveSpeed;
    public float PowerJump;
    [SerializeField]
    protected Transform groundPoints;
    [SerializeField]
    protected internal GameObject Fire;
    [SerializeField]
    protected  int Health;
    [SerializeField]
    private List<string> DamageSource;
    [SerializeField] 
    protected GameObject FirePos;
    [SerializeField]
    protected  float _GroundedRadius;
    [SerializeField]
    protected  LayerMask m_WhatIsGround;
    protected internal bool DestroyFire;
    [SerializeField] 
    protected GameObject FireDestroy;
    public abstract bool IsDead { get; }
    protected bool IsRight;
    public virtual void Start(){
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
    }
    public abstract void Death();
    protected abstract IEnumerator TakeDamage();
    public virtual void FireBall(int value){
           Instantiate(Fire, FirePos.transform.position, FirePos.transform.rotation);
    }
   public virtual void OnTriggerEnter2D(Collider2D other){
       if (DamageSource.Contains(other.tag))
           StartCoroutine(TakeDamage());
   }
    public void ChangeDir(){
        IsRight = !IsRight;
        transform.Rotate(0,180,0);
    }
}
