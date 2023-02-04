using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptHealthController : MonoBehaviour
{

    public float currentHealth;
    public float maxHealth;

    public Image healthBar;
    private Animator animHealth;

    void Start()
    {
        animHealth = GetComponent<Animator>();
        maxHealth = currentHealth;
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }

    private IEnumerator Player_Dead()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
    }

    public void Player_TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead!");
            //animHealth.SetTrigger("Player_Dead");
            StartCoroutine(Player_Dead());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy ATTACK");

            Player_TakeDamage(1);

            //animHealth.SetTrigger("Player_Hurt");
        }
    }
}
