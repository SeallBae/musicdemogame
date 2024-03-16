using UnityEngine;
using System;


public class MovingTiles : MonoBehaviour
{
    public GameObject tiles;
    public GameObject notetiles;
    public float topleft = -200;
    public float topright = 200;
    private int tileslayer = -1;
    private int noteslayer = -2;
    
    public float timeBetweenGenerate;
    private double generateTime;

    [SerializeField] MusicCutScript musiccut;

    private double[] TA;
    private int timeround = 2;
    int count;
    int flag;

    void Start()
    {
        //Time stamp of notes array
        count = 0;
        flag = 1; //right, 0 is left
        TA = new double [musiccut.TimeAppear.Length];

        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i];
        }
    
    }
    void Update()
    {
        //Generate tiles until the last notes
        if( (Math.Round(Time.time, timeround) > generateTime))
        {
            if ( Math.Round(Time.time, timeround) <= TA[TA.Length-1])
            {
                print(count);
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
    }

    void GenerateNoteRight()
    {
        Instantiate(notetiles, transform.position + new Vector3(topright, 0, noteslayer), transform.rotation);
    }
    void GenerateNoteLeft()
    {
        Instantiate(notetiles, transform.position + new Vector3(topleft, 0, noteslayer), transform.rotation);
    }
    void GenerateTilesLeft()
    {
        Instantiate(tiles, transform.position + new Vector3(topleft, 0, tileslayer), transform.rotation);
    }   
    void GenerateTilesRight()
    {
        Instantiate(tiles, transform.position + new Vector3(topright, 0, tileslayer), transform.rotation);
    }
}
