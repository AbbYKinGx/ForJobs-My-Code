using System.Collections;
using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.EditorScripts;
using UnityEngine;
public delegate void DeadEvent();
public class PlayerControl : Characters
{
	[SerializeField] 
	internal GameObject PlayerGame;
	private static PlayerControl instance;
	internal float direction;
	internal bool move;
	private bool MyAttack;
	private AudioSource audio;
	[SerializeField] 
	internal bool isWind;
	public static PlayerControl Instance{
		get{
			if (instance == null){
				instance = FindObjectOfType<PlayerControl>();
			}
			return instance;
		}
	}
	internal float _moveX;
	private bool isAttack;
	public event DeadEvent Dead;
	private SpriteRenderer _sprite;
	[SerializeField]
	private float isAttackTime;
	[SerializeField] 
	internal GameObject dashAnim;
	[SerializeField] 
	internal GameObject MaxJump;
	internal bool IsDash;
	internal bool Jump;
	internal bool _CompleteGame; 
	internal bool AddMoney;
	public override bool IsDead{
		get{
			if(Health <= 0)
				OnDead();
			return  Health <= 0;
		}
	}
	public override void Start ()
	{
		base.Start();
		audio = GetComponent<AudioSource>();
		Capsule = GetComponent<CapsuleCollider2D>();
		if (CoinsManager.Instance != null)
			CoinsManager.Instance.CollectedHealths = 3;
		_sprite = GetComponentInChildren<SpriteRenderer>();
	}
	public override void Death(){
		CoinsManager.Instance.CollectedHealths--;
		Health = 100;
		transform.position = DownPlayer.Me.ReSpawn.transform.position;
		Anim.SetTrigger("idle");
	}
	public void OnDead(){
		if (Dead != null){
			Dead();
		}
	}
	protected override IEnumerator TakeDamage(){	
		if (!isAttack){
			Health -= 10;
			isAttack = true;
			Anim.SetTrigger(!IsDead ? "Damage" : "Dead");
			yield return  new WaitForSeconds(isAttackTime);
			isAttack = false;
		}
		while (isAttack){
			_sprite.enabled = false;
			yield return  new WaitForSeconds(.1f);
			_sprite.enabled = true;
			yield return  new WaitForSeconds(.1f);
		}
	}
	private void Update(){
		if( CharacterEditor.me != null &&  CharacterEditor.me.MainEditor.activeSelf)
			return;
		if (CoinsManager.Instance != null && CoinsManager.Instance.CollectedHealths == 0)
		{
			transform.SetParent(DownPlayer.Me.ReSpawn.transform);
			if(PlayerGame != null && PlayerGame.activeSelf)
				PlayerGame.SetActive(false);
			Application.LoadLevel("GameOver");
		}

		if (!IsGround && !MyAttack)
		{
			Anim.SetTrigger("jump");
			Anim.SetBool("now_move" , false);
		}

		if(health != null)
			health.SetValueBar(Health, 100);
		if (DestroyFire){	
			FireDestroy.SetActive(false);
			DestroyFire = false;
		}
		if (IsDash){
			dashAnim.SetActive(false);
			IsDash = false;
		}

	}
	private void FixedUpdate(){
		if( CharacterEditor.me != null &&  CharacterEditor.me.MainEditor.activeSelf)
			return;
/*		if(IsDead)
			return;*/
		CharAnim();
		IsGround = isGround();
		if (!Jump && PowerJump > 1)
			PowerJump = 1;
		if (!Anim.GetCurrentAnimatorStateInfo(0).IsTag("KinGx_Attack"))
			MovePlayer();
		if (Input.GetKeyDown(KeyCode.Space) && IsGround)
			Jump = true;
		if (Jump)
			JumpPlayer();
		if (IsGround && !Jump)
		{
			Anim.ResetTrigger("jump");
		}
	}
	public bool isGround(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoints.position, _GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++){
			if (colliders[i].gameObject != gameObject)
				return true;
		}
		return false;
	}
	public void CharAnim(){
		if (Input.GetKeyDown(KeyCode.Mouse0)){
			Anim.SetTrigger("Attack");
			IfAttack();
		}
		else if(Input.GetKeyDown(KeyCode.Mouse1)){
			Anim.SetTrigger("Attack2");
			IfAttack();
		}
		else if(Input.GetKeyDown(KeyCode.Q)){
			Anim.SetTrigger("Block");
			IfAttack();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha1)){
			Anim.SetTrigger("Cast");
			IfAttack();
		}
		else if (Input.GetKeyDown(KeyCode.LeftControl) && IsGround){
			move = true;
			if (!IsRight)
				direction = 1;
			else
				direction = -1;
			Anim.SetTrigger("Dash");
			dashAnim.SetActive(true);
		}
		else if(Input.GetKeyDown(KeyCode.E) && _CompleteGame)
		{
			transform.SetParent(DownPlayer.Me.ReSpawn.transform);
			CompleteGame.me.anim.SetTrigger("OpenChest");
		}
		else{
			MyAttack = false;
		}
	}
	public void MovePlayer(){
		_moveX = Input.GetAxis("Horizontal");
		if (!move){
			Anim.SetBool("now_move", _moveX != 0);
			Rb.MovePosition(Rb.position + Vector2.right * _moveX * MoveSpeed * Time.deltaTime / 2);
			if(_moveX!= 0 && !audio.isPlaying && !Jump && IsGround && MoveSpeed != 0)
				audio.Play();
		}
		else {
			Anim.SetBool("now_move", direction != 0);
			Rb.MovePosition(Rb.position + Vector2.right * direction * (MoveSpeed - 2) * Time.deltaTime / 2);
		}
		if(_moveX > 0 && IsRight|| _moveX < 0 && !IsRight || IsRight && direction > 0 ||  !IsRight && direction < 0)
			ChangeDir();
	}
	public void JumpPlayer(){
		Rb.AddForce(Vector2.up * PowerJump);
		if (transform.position.y >= MaxJump.transform.position.y){
			Jump = false;
		}
	} 
	public void IfAttack(){
		Anim.ResetTrigger("jump");
		MyAttack = true;
	}
	public override void FireBall(int value){
		base.FireBall(0);
	}
	public void BtnMove(float direction){
		this.direction = direction;
		move = true;
	}
	public void BtnJump(){
		Jump = true;
	}
	public void BtnAttack(){
		Anim.SetTrigger("Attack");
		IfAttack();	
	}
	public void BtnDash(){
		if(IsGround)
			Anim.SetTrigger("Dash");
	}
	public void BtnFire(){
		Anim.SetTrigger("Cast");
	}
	public void BtnStopMove(){
		direction = 0;
		Anim.SetBool("now_move", false);
		move = false;
	}
	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D(other);
		if (other.CompareTag("EnemyFire"))
			FireDestroy.SetActive(true);
	}
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag("Coin"))
		{
			other.transform.SetParent(transform);
			CoinsManager.Instance.CollectedCoins++;
			Destroy(other.gameObject);
		}
		if (other.gameObject.CompareTag("Health")){
			CoinsManager.Instance.CollectedHealths++;
			Destroy(other.gameObject);
		}
	}
}
