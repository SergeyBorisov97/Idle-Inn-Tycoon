using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;

    private Vector3 forward, right;

    private Animation charAnimation;
    private AnimationClip[] animClips;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        animClips = AnimationUtility.GetAnimationClips(this.gameObject);
        charAnimation = GetComponent<Animation>();
        charAnimation.clip = animClips[0];
    }

	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("Collided with environment");
        if(charAnimation.isPlaying)
            charAnimation.Stop();
        forward = Vector3.zero;
        right = Vector3.zero;
    }

	void Update()
    {
        #region Old Input system
        if(Input.anyKey)
        {
            MoveWithOldInput();
            charAnimation.Play();
        }
        else
            if(charAnimation.isPlaying)
                charAnimation.Stop();
        #endregion
    }

    void MoveWithOldInput()
	{
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading;
        transform.position += rightMovement;
        transform.position += upMovement;
	}
}