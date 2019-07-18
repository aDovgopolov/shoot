using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{	
	[SerializeField]
	private float _speed = 5f;

	void Start()
    {
		transform.position = new Vector3(0, 6, 0);
	}
	
    void Update()
    {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -4f)
		{
			transform.position = new Vector3(Random.Range(-10f, 10f), 6, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{	
		if(other.tag == "Laser")
		{
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}

		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null) {
				player.DamagePlayer();
			}
			Destroy(this.gameObject);
		}
	}
}
