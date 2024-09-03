using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    Animator anim;
    Transform target;

    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            var playerToTheRight = target.position.x > transform.position.x;
            if(playerToTheRight)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }
    }
    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hit");
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
