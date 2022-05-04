using UnityEngine;

public class Fireball : MonoBehaviour
{
    private static Fireball instance;
    public static Fireball Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Fireball>();
            }

            return instance;
        }
    }

    internal bool Destroy;
    public float speedFire;
    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if (Destroy){
            Destroy(gameObject);
            Destroy = false;
        }
       FireAttack();
    }
    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
    private void FireAttack(){
        _rb.velocity = transform.right * speedFire;
    }
}
