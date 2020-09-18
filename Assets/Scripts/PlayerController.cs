using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, RollABallControls.IPlayerActions
{
    public float speed;
    public RollABallControls controls;
    private Rigidbody rb;
    public Vector2 motion;
    private int lives;
    private int count;
    public Text loseText;
    public Text countText;
    public Text winText;
    public Text livesText;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText ();
        winText.text = "";

        lives = 3;
    }

    void Update ()
    {
        if (Input.GetKey("Escape"))
        {
            Application.Quit();
        }
    }

    public void OnEnable() {
        if (controls == null)
        {
            controls = new RollABallControls();

            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        motion = context.ReadValue<Vector2>();
    }

    void FixedUpdate ()
        {
            Vector3 movement = new Vector3(motion.x, 0.0f, motion.y);
            rb.AddForce(movement * speed);
        }
       
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText ();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            lives = lives - 1;
            SetCountText();
        }
        if (count == 12)
        {
            transform.position = new Vector3(50.0f, 0.50f, 0.0f);
        }
           if (lives <= 0)
        {
            loseText.text = "You Lose!";
            Destroy (gameObject);
        }
    }
    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString ();
        
        livesText.text = "Lives: " + lives.ToString ();

        if (count >= 20)
        {
            winText.text = "You Win! Created by Shay Czachowski.";
        }
     
        if (lives >= 0)
        {
            loseText.text = "";
        } 
    }
}