using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject player;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private bool jump = false;
    public float jumpHeight = 100;
    private int jumpTimerMax = 60;
    private int jumpTimerCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    void FixedUpdate() 
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (jump && jumpTimerCount < jumpTimerMax)
        {
            Vector3 jumping = new Vector3(0.0f, jumpHeight, 0.0f);
            rb.AddForce(jumping);
            jumpTimerCount++;
        }
        else
        {
            jump = false;
            jumpTimerCount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Jump"))
        {
            jump = true;
        }
        else if (other.gameObject.CompareTag("Respawn"))
        {
            player.SetActive(false);
            loseTextObject.SetActive(true);

        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            winTextObject.SetActive(true);
        }
        
    }

}