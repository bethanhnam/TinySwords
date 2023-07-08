using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] HealBar _healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	}
    public void TakeDame(int dmg)
    {
        GameManager.Instance._playerHealth.DmgUnit(dmg);
        Debug.Log(GameManager.Instance._playerHealth.Health);
        _healthBar.SetHealth(GameManager.Instance._playerHealth.Health);
    }
	public void TakeHeal(int helling)
	{
        GameManager.Instance._playerHealth.HealthUnit(helling);
		Debug.Log(GameManager.Instance._playerHealth.Health);
		_healthBar.SetHealth(GameManager.Instance._playerHealth.Health);
	}
}
