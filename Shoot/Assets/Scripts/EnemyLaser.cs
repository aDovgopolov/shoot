using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser :MonoBehaviour
{
	[SerializeField]
	private float _speed = 8f;

	void Start()
	{
		Debug.Log("Start()");
	}

	void Update()
	{
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -5f)
		{
			Destroy(this.gameObject);
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("OnTriggerEnter2D(Collider2D other)");
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			Debug.Log($"{player}");
			if (player != null)
			{
				player.DamagePlayer();
			}			
			_speed = 0;
			Destroy(this.gameObject);
		}
	}
}
