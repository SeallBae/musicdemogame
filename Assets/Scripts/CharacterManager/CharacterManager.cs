using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject Character;
    public GameObject NiceNoti;
    public GameObject GoodNoti;
    public GameObject PerfectNoti;
    
    public int point;
    
    [SerializeField] MusicCutScript musiccut;
    private double [] TA;
    private int timeround = 2;

    private int count;

    private int notislayer = -4;

    private int scrolltime = 1; //approximately the scroll time of the tiles from spawn to character, here is 1 second

    // Timing values
    private double misstime = 0.22;
    private double nicetime = 0.2;
    private double goodtime = 0.1;
    private double perfecttime = 0.05;

    void Start()
    {
        //Time stamp of notes array
        TA = new double [musiccut.TimeAppear.Length + 1];
        count = 0;
        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i] + scrolltime;
        }
    }

    void Update()
    {
        point = Click();
        if ( (count <= TA.Length - 1))
        {
            if (Math.Round(Time.time, timeround) > TA[count]){
                if(Math.Round(Time.time, timeround) - TA[count] > misstime) //passed the note without click
                {
                    Move();
                    count++;
                }
            }    
        }
    }
    //Click function
    public int Click()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int score = Score(Math.Round(Time.time, timeround), TA[count]);
            if (score == 2) //Nice
            {
                
                Instantiate(NiceNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation);
                Move();
                count++;
                
            }
            else if(score == 1) //Good
            {
                Instantiate(GoodNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation);
                Move();
                count++;
                
            }
            else if(score == 0) //Perfect
            {
                Instantiate(PerfectNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation);
                Move();
                count++;
            }
            else
            {
                print("YOU MISS !!! ");
            }
            return score;
        }
        else
        {
            return 4; // passed, no click
        }
    }
    //Move function
    private void Move()
    {
        if (Character.transform.position.x == 200)
        {
            Character.transform.Translate(-400, 0, 0); //move left
        }
        else if (Character.transform.position.x == -200)
        {
            Character.transform.Translate(400, 0, 0); //move right
        }
    }
    //Score function
    private int Score(double ClickTime, double TargetTime)
    {
        if ( Math.Abs( Math.Round(ClickTime, timeround) - Math.Round(TargetTime, timeround)) <= perfecttime)
        {
            return 0; // Perfect
        }
        else if (Math.Abs( Math.Round(ClickTime, timeround) - Math.Round(TargetTime, timeround)) <= goodtime)
        {
            return 1; // Good
        }
        else if (Math.Abs( Math.Round(ClickTime, timeround) - Math.Round(TargetTime, timeround)) <= nicetime)
        {
            return 2; // Nice
        }
        else
        {
            return 3; // Miss
        }
    }
}
