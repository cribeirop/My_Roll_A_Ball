// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class Timer : MonoBehaviour
// {
//     [SerializeField] TextMeshProUGUI timerText;
//     private float remainingTime = 15f; 
//     private PlayerController playerControllerInstance; 

//     void Start()
//     {
//         playerControllerInstance = FindObjectOfType<PlayerController>();
//     }

//     void Update()
//     {   
//         if (playerControllerInstance != null && playerControllerInstance.state == "Ramp_2")
//         {   
//             timerText.gameObject.SetActive(true);
//             remainingTime -= Time.deltaTime;

//             if (remainingTime <= 0)
//             {
//                 remainingTime = 0;
//                 timerText.color = Color.red;
//             }
            
//             int minutes = Mathf.FloorToInt(remainingTime / 60);
//             int seconds = Mathf.FloorToInt(remainingTime % 60);

//             timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
//         }
//         else
//         {
//             timerText.gameObject.SetActive(false);
//         }
//     }
// }
