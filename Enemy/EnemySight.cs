using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]    
    private PlayerControl player;
    
    private void FixedUpdate(){
        if (_enemy != null){
            transform.position = _enemy.transform.position + new Vector3(0, -0.6f, 0);
            transform.rotation = _enemy.transform.rotation;
        }
         else if (player != null && !player.Jump)
           transform.position = player.transform.position + new Vector3(0,2f, 0);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player")
            _enemy.Target = other.gameObject;
    }
    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "Player")
            _enemy.Target = null;
    }
}
