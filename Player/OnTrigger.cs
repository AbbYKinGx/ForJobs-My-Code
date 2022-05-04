using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    [SerializeField] 
    private GameObject dustParticals;
    [SerializeField] 
    private GameObject JumpParticals;
    [SerializeField]
    private Animator camAnim;
    
    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Ground")){
            /*camAnim.SetTrigger("Shake");*/
            Instantiate(dustParticals, transform.position, dustParticals.transform.rotation);	
        }
    }
    private void Update(){
        if (PlayerControl.Instance._moveX != 0 && PlayerControl.Instance.isGround())
            Instantiate(dustParticals, transform.position, dustParticals.transform.rotation);
        if (!PlayerControl.Instance.isGround()){
            Instantiate(JumpParticals, transform.position + new Vector3(0, 1, 0), JumpParticals.transform.rotation);
        }
    }
}
