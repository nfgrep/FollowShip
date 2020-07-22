using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    Vector3 mouseWorldPos;
    Vector2 moveDirection;
    public AudioSource sound;
    private Rigidbody2D rb;
    public float moveForce = 1;
    public float rayDist = 1;
    public Transform rayStartPos;
    public GameObject enemyManager;
    public GameObject explosion;
    public GameObject guiManager;
    private SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //Moves player
        rb.AddForce(moveDirection * moveForce);
    }

    void Update()
    {

        //Stores mouse position in world coordinates
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Stores the direction from the player to the mouse in a vector3
        moveDirection = (mouseWorldPos - transform.position).normalized;
        //Generates Raycast
        RaycastHit2D hit = Physics2D.Raycast(rayStartPos.position, moveDirection, rayDist);
        //Generates angle of rotation in degrees
        float angleInRadians = Mathf.Atan2(moveDirection.y, moveDirection.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        //Sets current rotation to angle in degrees
        transform.eulerAngles = new Vector3(0, 0, angleInDegrees - 90);

        //Destroys enemy on mouseclick
        if (Input.GetButtonDown("Fire1"))
        {
            //Plays sound and animation
            sound.Play();
            anim.Play("Laser");

            //If gameobject hit is an enemy, destroy it
            if (hit && hit.collider.tag == "Enemy")
            {
                Destroy(hit.transform.gameObject);
                //Subtract enemycount
                enemyManager.SendMessage("SubtractEnemyCount");
                //Add to the score on screen
                guiManager.SendMessage("AddScore");
                //And instantiate an explosion
                Instantiate(explosion, hit.transform.position, Quaternion.identity);
            }

            //Do the same as above but without subtracting from enemyCount
            else if (hit && hit.collider.tag == "EnemySmol")
            {
                Destroy(hit.transform.gameObject);
                guiManager.SendMessage("AddScore");
                Instantiate(explosion, hit.transform.position, Quaternion.identity);

            }

            //Enters game from title screen
            else if (hit && hit.collider.tag == "Enter")
            {
                Instantiate(explosion, hit.transform.position, Quaternion.identity);
                Invoke("EnterGame", 0.2f);
            }

            //Sends player back to title screen
            else if (hit && hit.collider.tag == "Respawn")
            {
                Instantiate(explosion, hit.transform.position, Quaternion.identity);
                Invoke("TitleScene", 0.2f);
            }

            else
            {
                Debug.Log("Miss");
            }

        }

        //Stop the ship down on right mouseclick
        if (Input.GetButton("Fire2"))
        {
            moveForce = 0;
            rb.drag = 10;

        }
        else
        {
            moveForce = 20;
            rb.drag = 0.5f;
        }

    }

    //Kill player on enemy collision
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemySmol")
        {
            Instantiate(explosion, this.transform.position, Quaternion.identity);
            spriteRenderer.enabled = false;
            Invoke("KillPlayer", 0.2f);
        }
    }

    //Functions for changing scene based on player actions
    void EnterGame()
    {
        SceneManager.LoadScene("Lvl1");
    }

    void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    void KillPlayer()
    {
        SceneManager.LoadScene("Dead");
    }
}
