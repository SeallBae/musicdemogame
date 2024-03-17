using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;   

public class BokenManager : MonoBehaviour
{
    public GameObject bokenleft;
    public GameObject bokenright;
    public List<GameObject> bokens;

    public GameObject PerfectBoken;
    public GameObject GoodBoken;
    public GameObject NiceBoken;

    private GameObject character;
    private GameObject gamescene;

    private int spawnpointleft = -470;
    private int spawnpointright = 470;

    public float minX = -40;
    public float maxX = 40;

    [SerializeField] MusicCutScript musiccut;
    
    private int pointtodisappear = 1000; //point when bokens disappear;

    private int bokenslayer = -2;
    private double[] TA;
    private int timeround = 2;
    int count;
    int flag;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        //Time stamp of notes array
        TA = new double [musiccut.TimeAppear.Length];

        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i];
        }

        character = GameObject.Find("Character");
        gamescene = GameObject.Find("Game Manager");
    }

    // Update is called once per frame
    void Update()
    {
        //Generate bokens
        if (Math.Round(Time.time, timeround) == TA[count])
        {
            GenerateBoken();
            if (count < TA.Length-1)
            {
                count++;
            }
        }

        //make bokens broke when clicked
        int point = character.GetComponent<CharacterManager>().point;

        if (point == 0)
        {
            print ("boken break perfectly");
        }
        else if (point == 1)
        {
            print ("boken break good");
        }
        else if (point == 2)
        {
            print ("boken break nicely");
        }
        else
        {
            print ("boken wont break");
        }
        //boken disappear when passed
        for (int i = 0; i < bokens.Count; i++)
        {
            if (gamescene.transform.position.y - bokens[i].transform.position.y > pointtodisappear)
            {
                bokens[i].SetActive(false);
            }
        }
    }
    void GenerateBoken()
    {
        if ( UnityEngine.Random.Range(0, 20) % 2 == 0) //even, spawn 2 bokens
        {
            bokens.Add(Instantiate(bokenleft, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointleft, 300, bokenslayer), transform.rotation));
            bokens.Add(Instantiate(bokenright, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointright, 300, bokenslayer), transform.rotation));
            flag = 2;
        }
        else //odd, spawn 1 boken
        {
            if ( UnityEngine.Random.Range(0, 20) % 2 == 0) //even, spawn right
            {
                bokens.Add(Instantiate(bokenright, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointright, 300, bokenslayer), transform.rotation));
                flag = 1;
            }
            else //odd, spawn left
            {
                bokens.Add(Instantiate(bokenleft, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointleft, 300, bokenslayer), transform.rotation));
                flag = 1;   
            }
        }
    }   
}

    

