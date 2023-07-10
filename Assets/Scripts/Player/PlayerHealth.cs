using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] protected HealBar _healthBar;
	[SerializeField] public int currentHealth;
	[SerializeField] public int maxHealth;
	// Start is called before the first frame update
	void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

	}
    public void TakeDame(int dmg)
    {
        currentHealth -= dmg;
        Debug.Log(currentHealth);
        _healthBar.SetHealth(currentHealth);
    }
	public void TakeHeal(int helling)
	{
        currentHealth += helling;
		Debug.Log(currentHealth);
		_healthBar.SetHealth(currentHealth);
	}
}
