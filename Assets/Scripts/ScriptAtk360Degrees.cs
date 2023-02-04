using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAtk360Degrees : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRenge = 0.5f;
    public LayerMask enemyLayers;

    public bool canrightATK;

    private Animator animPlayer;

    public AudioSource audioAtk;

    private void Start()
    {
        animPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (canrightATK)
            {
                canrightATK = false;
                animPlayer.SetTrigger("isAttack360");
                audioAtk.Play();
                StartCoroutine(RightAttackSpeed());
            }
        }
    }

    private IEnumerator RightAttackSpeed()
    {
        Debug.Log("RightAttack Enemy");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRenge, enemyLayers);

        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<ScriptEnemyController>().TakeDamageEnemy(2);
                Debug.Log("2");
            }
        }
        yield return new WaitForSeconds(10f);
        canrightATK = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRenge);
    }
}
