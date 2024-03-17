using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic; 

public class CharacterManager : MonoBehaviour
{
    [Header("=== Notifications ===")]
    public GameObject NiceNoti;
    public GameObject GoodNoti;
    public GameObject PerfectNoti;
    public List<GameObject> notis;
    private int notislayer = -4;
    public int point;

    public GameObject Character;
    private GameObject gamescene;

    [Header("=== Music Cut ===")]
    [SerializeField] MusicCutScript musiccut;
    private double [] TA;
    private int timeround = 2;
    private int count;

    private int scrolltime = 1; //approximately the scroll time of the tiles from spawn to character, here is 1 second
    private int pointtodisappear = 1000;

    // Timing values
    private double misstime = 0.22;
    private double nicetime = 0.2;
    private double goodtime = 0.1;
    private double perfecttime = 0.05;

    //Character coordinate
    private int characterx = 200;

    void Start()
    {
        count = 0;
        //Time stamp of notes array
        TA = new double [musiccut.TimeAppear.Length + 1];
        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i] + scrolltime;
        }

        gamescene = GameObject.Find("Game Manager");
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
        for (int i = 0; i < notis.Count; i++) //hide the notis when passed
        {
            if (gamescene.transform.position.y - notis[i].transform.position.y > pointtodisappear)
            {
                notis[i].SetActive(false);
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
                notis.Add(Instantiate(NiceNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation));
                Move();
                count++;
            }
            else if(score == 1) //Good
            {
                notis.Add(Instantiate(GoodNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation));
                Move();
                count++;
            }
            else if(score == 0) //Perfect
            {
                notis.Add(Instantiate(PerfectNoti, transform.position + new Vector3(0, 0, notislayer), transform.rotation));
                Move();
                count++;
            }
            else // Miss
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
        if (Character.transform.position.x == characterx)
        {
            Character.transform.Translate(-(characterx*2), 0, 0); //move left
        }
        else if (Character.transform.position.x == -characterx)
        {
            Character.transform.Translate(characterx*2, 0, 0); //move right
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
