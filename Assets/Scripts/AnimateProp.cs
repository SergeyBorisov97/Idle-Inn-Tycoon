using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateProp : MonoBehaviour
{
	private Animator animator;
	private new Animation animation;

	public float minDistance = 5f;
	public GameObject player;

	void Start()
	{
		animation = GetComponent<Animation>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public void Animate()
	{
		animation.Play();
	}

	public void StopAnimate()
	{
		if(animation.isPlaying)
		{
			animation.Stop();
			animation.Rewind();
		}
	}

	void Update()
	{
		float reachDistance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if(reachDistance < minDistance)
		{
			Animate();
		}
		else
			StopAnimate();
	}
}