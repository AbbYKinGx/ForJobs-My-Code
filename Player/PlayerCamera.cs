using UnityEngine;

public class PlayerCamera : MonoBehaviour{
    [SerializeField] 
    private float MinY;
    [SerializeField] 
    private float MinX;
    [SerializeField] 
    private float MaxY;
    [SerializeField] 
    private float MaxX;
    private Transform target;
    private void Start(){
       target = GameObject.Find("Player").transform;
    }
    private void LateUpdate(){
     transform.position = new Vector3(Mathf.Clamp(target.position.x , MinX, MaxX),Mathf.Clamp(target.position.y , MinY, MaxY), -4f);
    }
}
