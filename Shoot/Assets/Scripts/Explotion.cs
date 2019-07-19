using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotion : MonoBehaviour
{
	[SerializeField]
	private AudioClip _asteroidExplotionClip;
	private AudioSource _asteroidExplotionAudioSource;
	
	void Start()
    {
		_asteroidExplotionAudioSource = GetComponent<AudioSource>();
		_asteroidExplotionAudioSource.clip = _asteroidExplotionClip;

		_asteroidExplotionAudioSource.Play();

		Destroy(this.gameObject, 2.3f);
	}
	
}
