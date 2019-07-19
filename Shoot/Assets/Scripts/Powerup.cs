using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3f;
	[SerializeField]
	private AudioClip _powerUpClip;
	//private AudioSource _powerUpAudioSource;

	[SerializeField]
	private int powerupID;

	//private void Start()
	//{

	//	_powerUpAudioSource = GetComponent<AudioSource>();
	//	_powerUpAudioSource.clip = _powerUpClip;
	//}

	void Update()
    {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -4)
			Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();
			if (player != null)
			{
				AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);
				switch (powerupID)
				{
					case 0:
						player.ActivateTripleShop();
						break; 
					case 1:
						player.ActivateSpeed();
						break;
					case 2:
						player.ActivateShield();
						break;
					default:
						Debug.Log("Default");
						break;
				}
			}
			Destroy(this.gameObject);
		}
	}
}
