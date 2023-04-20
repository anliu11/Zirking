using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerHealthText : MonoBehaviour
{
    [SerializeField] private int playerHP;

    public PlayerController player;
    public TextMeshProUGUI healthNumber;

    
    // Updates player health text everytime
    private void Update()
    {
        playerHP = GameObject.Find("Player").GetComponent<PlayerController>().hP;
        healthNumber.text = playerHP.ToString() + " /100";
    }
}
