using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
	private Animator floatingTextAnimator;
	public Vector3 RandomzeIntensity = new Vector3(0.5f,0,0);
	private void Awake()
	{
		floatingTextAnimator = GetComponent<Animator>();
	}
	private void Start()
	{
		Destroy(gameObject, 1.5f) ;
		transform.localPosition += new Vector3(0, 0.8f, 0);
	}
	private void FixedUpdate()
	{
		transform.localPosition += new Vector3(0, 0.1f, 0);
	}
}
