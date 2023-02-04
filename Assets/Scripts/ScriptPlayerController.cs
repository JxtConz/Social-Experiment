using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlayerController : MonoBehaviour
{

    private float moveSpeed = 5f;
    public SpriteRenderer moveFlip;

    private Vector3 moveDir;

    private Rigidbody2D rb2d;
    private Animator anim;

    /*public Transform attackPoint;
    public float attackRenge = 0.5f;
    public LayerMask enemyLayers;*/

    //private int playerAttack;

    //public bool canleftATK;
    //public bool canrightATK;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        

        /*anim.SetFloat("Horizontal", moveDir.x);
        anim.SetFloat("Vertical", moveDir.y);
        anim.SetFloat("moveSpeed", moveDir.sqrMagnitude);*/

        /*if (moveDir.x == -1f)
        {
            anim.SetTrigger("isLeft");
        }
        else if (moveDir.x == 1f)
        {
            anim.SetTrigger("isRight");
        }
        else if (moveDir.y == 1f)
        {
            anim.SetTrigger("isBack");
        }
        else if (moveDir.y == -1f)
        {
            anim.SetTrigger("isTop");
        }*/

        moveDir.Normalize();
        rb2d.velocity = moveDir * moveSpeed;

        //Left Click
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (canleftATK)
            {
                canleftATK = false;
                StartCoroutine(LeftAttackSpeed());
            }
        }*/

        //Right Click
        /*if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            if (canrightATK)
            {
                canrightATK = false;
                StartCoroutine(RightAttackSpeed());
            }
        }*/
    }

    

    /* private IEnumerator LeftAttackSpeed()
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
     }*/

    /*private IEnumerator RightAttackSpeed()
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
    }*/

    /*public void AttackEnemy()
    {
        //anim.SetTrigger("Player_Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRenge, enemyLayers);

        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<ScriptEnemyController>().TakeDamageEnemy(1);
            }
        }
    }*/

    /*private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRenge);
    }*/


}
