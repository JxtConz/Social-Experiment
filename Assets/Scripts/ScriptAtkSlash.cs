using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAtkSlash : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRenge = 0.5f;
    public LayerMask enemyLayers;
    public bool canleftATK;

    private Animator animPlayer;

    private void Start()
    {
        animPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        //Left Click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animPlayer.SetTrigger("isAttack");

            if (canleftATK)
            {
                canleftATK = false;
                StartCoroutine(LeftAttackSpeed());
            }
        }
    }

    private IEnumerator LeftAttackSpeed()
    {
        Debug.Log("LeftAttack Enemy");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRenge, enemyLayers);

        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<ScriptEnemyController>().TakeDamageEnemy(1);
                Debug.Log("1");
            }
        }
        yield return new WaitForSeconds(0.5f);
        canleftATK = true;
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRenge);
    }
}
