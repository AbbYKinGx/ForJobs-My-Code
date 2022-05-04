
using UnityEngine;

public class IgnoreCapsule : MonoBehaviour
{
    private void Awake(){
       if(PlayerControl.Instance !=null)
           Physics2D.IgnoreCollision(GetComponent<Collider2D>() ,PlayerControl.Instance.Capsule, true);
    }
}
