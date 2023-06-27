using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public EnemyAI thisEnemy;
	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

	public void TakeDamage(int damage)
	{
		if (currentHealth>0)
        {
			currentHealth -= damage;

			healthBar.SetHealth(currentHealth);

			if (currentHealth <= 0)
            {
				thisEnemy.Die();
            }
		}
	}
}
