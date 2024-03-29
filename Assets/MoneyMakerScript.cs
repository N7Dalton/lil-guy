using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMakerScript : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager gameManagerScript;
    public float Salary;
    public float TimeToMakeMoney;
    private float TimerToMakeMoney;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        TimerToMakeMoney = TimeToMakeMoney;
    }

    // Update is called once per frame
    void Update()
    {
        TimerToMakeMoney -= Time.deltaTime; 
        if(TimerToMakeMoney <= 0 )
        {
            MakeMoney();
            TimerToMakeMoney = TimeToMakeMoney;
        }
    }
    private void MakeMoney()
    {
        gameManagerScript.Money += Salary;
    }
}
