
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
            PlayerControl.Instance.Death();
    }
}
