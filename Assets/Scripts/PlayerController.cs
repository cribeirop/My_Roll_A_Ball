using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI lifeText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private int vida;
    private float movementX;
    private float movementY;

    private bool isRamp_2 = false;
    private bool isPass = false;
    private float remainingTime = 10f;
    private float negativeTime = 10f;
    private float loseTime = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        vida = 3;
        
        SetCountText();
        winTextObject.SetActive(false);

        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
    }

    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.y;  
        movementY = -movementVector.x; 
    }

    void SetCountText() 
    {
        countText.text = "Pontos: " + count.ToString();
        lifeText.text = "Vida total: " + vida.ToString();
    }

    public float threshold;

    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 

        if (transform.position.y < threshold)
        {   
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.Sleep();
            vida = vida -1;
            
            if (isRamp_2) {
                timerText.gameObject.SetActive(false);
                SetCountText();
                transform.position = new Vector3(45f, -4.5f, -11);
                negativeTime = 10f;
                remainingTime = 10f;
                isRamp_2 = true;
            }
            else {
                SetCountText();
                transform.position = new Vector3(0, 0.5f, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (other.gameObject.CompareTag("Pass")) {
            isRamp_2 = true;
        }
        else if (other.gameObject.CompareTag("End") && isRamp_2) {
            isPass = true;
            isRamp_2 = false;
        }
        else if (other.gameObject.CompareTag("Propeller")) 
        {
            vida = vida - 1;
            SetCountText();
        }
    }


    void Update()
    {
        if (isRamp_2)
        {   
            timerText.color = Color.yellow;
            timerText.gameObject.SetActive(true);
            remainingTime -= Time.deltaTime;
            negativeTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerText.color = Color.red;
            }
            if (negativeTime <= -1) 
            {
                timerText.gameObject.SetActive(false);
                vida -= 1;
                SetCountText();
                transform.position = new Vector3(45f, -4.5f, -11);
                negativeTime = 10f;
                remainingTime = 10f;
                isRamp_2 = true;
            }
            
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (vida <= 0) 
        {   
            timerText.color = Color.blue;
            timerText.text = "You Lose...";
            loseTime -= Time.deltaTime;
            if (loseTime <= 0) {
                SceneManager.LoadScene(2);
            }
        }

        if (isPass) {
            timerText.gameObject.SetActive(true);
            timerText.color = Color.green;
            timerText.text = "You Win!";
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0) {
                SceneManager.LoadScene(0);
            }
            
        }

        if (count > 2) 
        {
            count = 0;
            vida = vida + 1;
            SetCountText();
        }
    }
}
