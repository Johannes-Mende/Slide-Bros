using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerMovementController : NetworkBehaviour
{
    public GameObject PlayerModel;

    private Rigidbody2D rb2D;

    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float jumpForce = 60f;
    [SerializeField]
    private bool isJumping = false;

    private float moveHorizontal;
    private float moveVertical;



    void Start()
    {
        PlayerModel.SetActive(false);

        rb2D = gameObject.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (SceneManager.GetActiveScene().name == "Game")
        {
            if(PlayerModel.activeSelf == false)
            {
                SetPosition();
                PlayerModel.SetActive(true);
            }
            /*if(hasAuthority)
            {
                Movement();
            }*/
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (hasAuthority)
            {
                Movement();
            }
        }
       
    }

    public void SetPosition()
    {
        transform.position = new Vector3(Random.Range(-5, 5), 0.8f, 0);
    }

    public void Movement()
    {
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if(!isJumping &&moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2( 0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isJumping = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            isJumping = true;
        }
    }
}
