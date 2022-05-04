
using UnityEngine;

public class Platform : MonoBehaviour{
    public BoxCollider2D box2d;
    public BoxCollider2D box2d_triger;
    void Start(){
        Physics2D.IgnoreCollision(box2d, box2d_triger, true);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
            Physics2D.IgnoreCollision(box2d, other, true);
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"  || other.gameObject.tag == "Enemy")
            Physics2D.IgnoreCollision(box2d, other, false);
    }
}
