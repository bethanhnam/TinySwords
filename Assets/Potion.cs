using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Potion : MonoBehaviour
{
	public GameObject player;
	public int price;
	public abstract void Use();
	public virtual void Start()
	{
		player = GameObject.Find("Knight");
	}
}
