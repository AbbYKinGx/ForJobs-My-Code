using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private Vector3 posA;
    private Vector3 posB;
    
    private Vector3 nexPoss;
    [SerializeField]
    private Transform childTransform;
    [SerializeField] 
    private float speed;
    [SerializeField]
    private Transform _transformB;

    public bool DestoyPlatform;
    private float _Time;
    private float ExitTime = 5f;
    private float ExitTime2 = 3f;
    public bool isBarrier;
    // Start is called before the first frame update
    void Start()
    {
        posA = childTransform.localPosition;
        posB = _transformB.localPosition;
        nexPoss = posB;
    }

    // Update is called once per frame
    void Update(){
        _Time += Time.deltaTime;
        if (_Time >= ExitTime && isBarrier){
            _Time = 0;
            speed = 0;
        }
        else if (speed == 0 && _Time >=ExitTime2 && isBarrier)
             speed = 0.4f;
        Move();
    }

    private void Move(){
        if (!DestoyPlatform){
            childTransform.localPosition =
                Vector3.MoveTowards(childTransform.localPosition, nexPoss, speed * Time.deltaTime);
            if (Vector3.Distance(childTransform.localPosition, nexPoss) <= 0.1)
                ChangeDestination();
        }
        if (nexPoss == posA && DestoyPlatform){
            childTransform.localPosition =
                Vector3.MoveTowards(childTransform.localPosition, nexPoss, speed * Time.deltaTime);
        }
    }

    private void ChangeDestination()
    {
        nexPoss = nexPoss != posA ? posA : posB;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !DestoyPlatform)
            other.transform.SetParent(childTransform , true );
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("GroundPlayer") && DestoyPlatform){
            nexPoss = posB;
            childTransform.localPosition =
                Vector3.MoveTowards(childTransform.localPosition, nexPoss, speed * Time.deltaTime);  
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(DestoyPlatform)
            nexPoss = posA;
    }

    private void OnCollisionExit2D(Collision2D other){
        if (DestoyPlatform){
            nexPoss = posA;
        }
        other.transform.SetParent(DownPlayer.Me.ReSpawn.transform);
    }
}
