using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 1f;
    public float brakeForce = 0.7f;

    float hMove = 0;
    float vMove = 0;

    bool flipSprite;
    int rotator = 0;

    public bool canMove;

    Rigidbody2D rbody;

    public Animator maggieAnimator;
    public GameObject maggieSprite;
    SpriteRenderer maggieRenderer;
    Transform maggieTransform;

    private void Awake()
    {
        canMove = (PlayerPrefs.GetInt("playedIntro") == 1) ? true : false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        maggieRenderer = maggieSprite.GetComponent<SpriteRenderer>();
        maggieTransform = maggieSprite.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            FlipCheck();

            /*Vector2 currentPos = rbody.position;
            float verticalInput = Input.GetAxis("Horizontal");
            float horizontalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

            Vector2 movement = inputVector + movementSpeed;

            Vector2 newPos = currentPos
            */


            hMove = Input.GetButton("Horizontal") ? (Input.GetAxis("Horizontal") * moveSpeed * 1.5f) : (Input.GetAxis("Horizontal") * moveSpeed - (Input.GetAxis("Horizontal") * moveSpeed * brakeForce));
            vMove = Input.GetButton("Vertical") ? (Input.GetAxis("Vertical") * moveSpeed) : (Input.GetAxis("Vertical") * moveSpeed - (Input.GetAxis("Vertical") * moveSpeed * brakeForce));
            rbody.velocity = new Vector2(hMove, vMove);

            AnimatorHandle();
        }
        


    }

    void FlipCheck()
    {
        
        Vector2 scale = maggieTransform.localScale;
        flipSprite = (rotator == 180 ? (Input.GetAxis("Horizontal") < 0) : (Input.GetAxis("Horizontal") > 0));
        if (flipSprite)
        {
            scale.x *= -1;
            
            if (rotator == 180)
            {
                rotator = 0;
                //maggieTransform.localPosition = new Vector2(transform.localPosition.x + ((maggieRenderer.size.x * maggieTransform.localScale.x) / 2), maggieTransform.localPosition.y);
            }
            else
            {
                rotator = 180;
                //maggieTransform.localPosition = new Vector2(transform.localPosition.x - ((maggieRenderer.size.x * maggieTransform.localScale.y) / 2), maggieTransform.localPosition.y);

            }

            //transform.localScale = scale;
            maggieTransform.localRotation = Quaternion.Euler(0, rotator, 0);
        }
       
    }

    void AnimatorHandle()
    {
        if (Input.GetButtonDown("Horizontal")){
            maggieAnimator.SetBool("isAside", true);
            maggieAnimator.SetBool("isFront", false);
            maggieAnimator.SetBool("isBack", false);
        }

        if (Input.GetButton("Vertical") && !Input.GetButton("Horizontal"))
        {
            maggieAnimator.SetBool("isAside", false);
            maggieAnimator.SetBool("isFront", (Input.GetAxis("Vertical") < 0));
            maggieAnimator.SetBool("isBack", (Input.GetAxis("Vertical") > 0));
        }

        maggieAnimator.SetBool("isWalking", (Input.GetButton("Vertical") || Input.GetButton("Horizontal")));

    }
}
