using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{   

    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float moveSpeed = 5f;
    // Start is called before the first frame update

    Rigidbody2D rb;
    int maxHealth = 100;
    int currentHealth;

    Animator anim;

    bool isDead = false;
    bool firstDeath = true;

    float moveHorizontal;
    float moveVertical;
    Vector2 movement;
    int facingDirection = 1;

    public static Player instance;

    public GameObject FirstDiePanel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {   
        if (currentHealth > 0)
        {
            isDead = false;
        }

        if(isDead)
        {
            //movement = Vector2.zero;
            //anim.SetFloat("Velocity", 0);
            return;

        }
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        anim.SetFloat("Velocity", movement.magnitude);
        if(movement.x > 0)
        {
            facingDirection = 1;
        }
        else if(movement.x < 0)
        {
            facingDirection = -1;
        }
        transform.localScale = new Vector2(facingDirection, 1);
    }

    private void FixedUpdate()
    {
        if (movement == Vector2.zero)
        {
            rb.velocity = Vector2.zero; // 停止移动
        }
        else
        {
            rb.velocity = movement * moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {   
        anim.SetTrigger("Hit");
        currentHealth -= damage;
        healthText.text = currentHealth.ToString();
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void ResetPlayer()
    {
        //currentHealth = maxHealth;
        //healthText.text = currentHealth.ToString();
        isDead = false;
    }

    public void Die()
    {
        isDead = true;
        Time.timeScale = 0; //暂停游戏
        if (firstDeath)
        {
            FirstDiePanel.SetActive(true);
        } else
        {

        }
        firstDeath = false;
    }

}
