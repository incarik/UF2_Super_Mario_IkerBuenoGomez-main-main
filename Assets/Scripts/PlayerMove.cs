using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour

{
    public Vector3 newPosition = new Vector3(50, 5, 0);
    public float movementSpeed = 5;
    private float inputHorizontal;
    public bool jump = false;
    public Rigidbody2D rBody;
    public float jumpForce = 5;
    public GroundSensor sensor;
    public SpriteRenderer render;
    public Animator anim;
    AudioSource source;
    public AudioClip jumpSound;
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    private bool canSoot = true;
    public float timer;
    public float rateOffFire = 1f;
    public Transform hitBox;
    public float hitBoxRadius = 2;

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Teletrasnporta al personaje a la posicion dicha en la varaible
        //transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        //transform.position = transform.position + new Vector3(1, 0, 0) * movementSpeed * Time.deltaTime;
        //transform.position += new Vector3(inputHorizontal, 0, 0) * movementSpeed * Time.deltaTime;

       /* if(jump == true)
        {
            Debug.Log("estoy saltando");
        }
        else if(jump == false)
        {
            Debug.Log("estoy en el suelo");
        }
        else
        {
            Debug.Log("yooooo");
        }*/
        
        Jump();
        
        Movement();

        Shoot();

        if(Input.GetKeyDown(KeyCode.J))
        {
            Attack();
            anim.SetTrigger("IsAttacking");
        }
    }

    void FixedUpdate()
    {
         rBody.velocity = new Vector2(inputHorizontal * movementSpeed, rBody.velocity.y);
        
    }

    void Jump()
    {
         if(Input.GetButtonDown("Jump") && sensor.isGrounded == true)
        {
               rBody.AddForce(new Vector2(0,1) * jumpForce, ForceMode2D.Impulse);   
               anim.SetBool("IsJumping", true);
               source.PlayOneShot(jumpSound);
        }
    }

    void Movement()
    {
         if(inputHorizontal < 0)
        {
            //render.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180,0);
            anim.SetBool("IsRunning", true);
        }
        else if(inputHorizontal > 0)
        {
            //render.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0,0);
            anim.SetBool("IsRunning", true);
        }
        else 
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void Shoot()
    {
        if(!canSoot)
        {
            timer += Time.deltaTime;
            if(timer >= rateOffFire)
            {
                canSoot = true;
                timer = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && canSoot)
        {
            Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            canSoot = false;
        }
    }


    public void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitBox.position, hitBoxRadius);
    

        foreach (Collider2D enemy in enemies)
        {
            if(enemy.gameObject.tag == "Goombas")
            {
                //Destroy(enemy.gameObject);
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                enemyScript.GoombaDeath();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitBox.position, hitBoxRadius);
    }
}