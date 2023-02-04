using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptEnemyController : MonoBehaviour
{
    public ScriptHealthController enemyDamage;

    [SerializeField] private float moveSpeed;

    private Vector3 movement;
    private Transform target;

    public int enemyHealth;
    public int enemycurrentHealth;

    [SerializeField] public GameObject floatTextPrefab;

    private Rigidbody2D rb2d;
    public BoxCollider2D boxCollider;

    private Animator anim;

    public AudioSource audioRoots;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        enemycurrentHealth = enemyHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (target)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb2d.rotation = angle;
            direction.Normalize();
            movement = direction;
        }

        if (enemycurrentHealth <= 0)
        {

            moveSpeed = 0;
            transform.rotation = Quaternion.Euler(Vector3.forward);
            StartCoroutine(Delay_EnemyDead());
        }
    }

    private IEnumerator Delay_EnemyDead()
    {
        audioRoots.Play();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        MovingEnemy(movement);
    }

    public void MovingEnemy(Vector3 direction)
    {
        if (target)
        {
            rb2d.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
        }
    }

    public void TakeDamageEnemy(int damage)
    {
        enemycurrentHealth -= damage;

        if (floatTextPrefab && enemycurrentHealth > 0)
        {
            ShowDamage();
        }
    }

    public void ShowDamage()
    {
        var text = Instantiate(floatTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshProUGUI>().text = enemycurrentHealth.ToString();
    }
}
