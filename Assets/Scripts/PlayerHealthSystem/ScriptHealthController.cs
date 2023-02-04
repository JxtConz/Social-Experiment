using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptHealthController : MonoBehaviour, HitPlayer
{
    public float currentHealth;
    public float maxHealth;

    public Image healthBar;
    public Animator animPlayer;

    public AudioSource audioPlayer;
    public AudioSource audioDead;

    int currentSceneIndex;

    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        animPlayer = GetComponent<Animator>();
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

        SceneManager.LoadScene(3);
    }

    public void Player_TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Player Dead!");
            animPlayer.SetTrigger("isDie");
            audioPlayer.Play();
            StartCoroutine(Player_Dead());
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy ATTACK");

            Player_TakeDamage(1);
            audioPlayer.Play();
            animPlayer.SetTrigger("isHurt");
        }
    }
    */

    public void HitPlayer(GameObject source, int amount, HitPlayer.HitType hit)
    {
        //Debug.Log("Enemy ATTACK");
        animPlayer.SetTrigger("isHurt");
        audioDead.Play();
        Player_TakeDamage(amount);
    }
}
