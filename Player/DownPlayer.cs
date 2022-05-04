using UnityEngine;

public class DownPlayer : MonoBehaviour
{
	internal static DownPlayer Me;
	public GameObject ReSpawn;

	private void Start()
	{
		Me = this;
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag("Player"))
		{
			CoinsManager.Instance.CollectedHealths--;
			other.transform.position = ReSpawn.transform.position;
		}
	}
}
