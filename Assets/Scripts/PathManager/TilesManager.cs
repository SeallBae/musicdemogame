using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;   


public class TilesManager : MonoBehaviour
{
    public GameObject tile;
    public GameObject notetile;
    public GameObject character;

    [SerializeField] Notes notescript;

    private GameObject gamescene;

    public List<GameObject> tiles;
    public List<GameObject> notes;  

    public float topleft = -200;
    public float topright = 200;
    private int tileslayer = -1;
    private int noteslayer = -2;

    private int pointtofall = 600; //point when tiles start to fall;
    private int pointtodisappear = 1000; //point when tiles disappear;
    
    public float timeBetweenGenerate;
    private double generateTime;

    [SerializeField] MusicCutScript musiccut;

    private double[] TA;
    private int timeround = 2;
    int count;
    int flag;

    void Start()
    {
        count = 0;
        flag = 1; //right, 0 is left

        //Array of notes time stamp
        TA = new double [musiccut.TimeAppear.Length];
        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i];
        }

        gamescene = GameObject.Find("Game Manager");
    }
    void Update()
    {
        //Generate tiles until the last notes
        if( (Math.Round(Time.time, timeround) > generateTime))
        {
            if ( Math.Round(Time.time, timeround) <= TA[TA.Length-1])
            {
                if (flag == 1) //if flag = 1, right, else left
                {
                    GenerateTilesRight();
                }
                else
                {
                    GenerateTilesLeft();
                }
                generateTime = Math.Round(Time.time, timeround) + timeBetweenGenerate;
            }
         
        }
        
        //Generate notes
        if (Math.Round(Time.time, timeround) == TA[count])
        {
            if ((count % 2) == 0) //index even
            {
                GenerateNoteRight();
                flag = 0;
            }
            else //index odd
            {
                GenerateNoteLeft();
                flag = 1;
            }
            
            if (count < TA.Length-1)
            {
                count++;
            }
        }

        //make tiles fall when passed
        for (int i = 0; i < tiles.Count; i++)
        {
            if (gamescene.transform.position.y - tiles[i].transform.position.y > pointtofall)
            {
                tiles[i].transform.localScale += new Vector3(-0.5f, -0.5f, 0);
                if (gamescene.transform.position.y - tiles[i].transform.position.y > pointtodisappear)
                {
                    tiles[i].SetActive(false);
                }
            }
            
        }
        
        //make notes fall and blur when passed
        for (int i = 0; i < notes.Count; i++)
        {
            if (gamescene.transform.position.y - notes[i].transform.position.y > pointtofall)
            {
                notes[i].GetComponent<SpriteRenderer>().sprite = notescript.PressNote;
                notes[i].transform.localScale += new Vector3(-0.5f, -0.5f, 0);
                if (gamescene.transform.position.y - notes[i].transform.position.y > pointtodisappear)
                {
                    notes[i].SetActive(false);
                }
            }
        }
    }

    void GenerateNoteRight()
    {
        notes.Add(Instantiate(notetile, transform.position + new Vector3(topright, 0, noteslayer), transform.rotation));
    }
    void GenerateNoteLeft()
    {
        notes.Add(Instantiate(notetile, transform.position + new Vector3(topleft, 0, noteslayer), transform.rotation));
    }
    void GenerateTilesRight()
    {
        tiles.Add(Instantiate(tile, transform.position + new Vector3(topright, 0, tileslayer), transform.rotation));
    }
    void GenerateTilesLeft()
    {
        tiles.Add(Instantiate(tile, transform.position + new Vector3(topleft, 0, tileslayer), transform.rotation));
    }   
}
