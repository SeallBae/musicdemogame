using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;   

public class BokenManager : MonoBehaviour
{
    [Header("=== Left And Right Boken ===")]
    public GameObject bokenleft;
    public GameObject bokenright;

    public List<GameObject> bokens;
    public List<GameObject> brokenbokens;

    [Header("=== Boken Status ===")]
    public GameObject PerfectBoken;
    public GameObject GoodBoken;
    public GameObject NiceBoken;

    private GameObject character;
    private GameObject gamescene;

    private int spawnpointleft = -470;
    private int spawnpointright = 470;

    private int halfscreen = 960; 
    private int bokenaheadofnote = 300; // the coordinate make boken always spawn after notes  

    public float minX = -40;
    public float maxX = 40;

    [SerializeField] MusicCutScript musiccut;
    
     private int pointtofall = 600;
    private int pointtodisappear = 1000; //point when bokens disappear;

    private int bokenslayer = -2;
    private double[] TA;
    private int timeround = 2;
    int count;
    int bokencount;
    int flag;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        bokencount = 0;

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
            if (Math.Round(Time.time, timeround) <= TA[TA.Length-1])
            {
                GenerateBoken();
                if (count < TA.Length-1)
                {
                    count++;
                }
            }
        }

        //make bokens broke when clicked
        int point = character.GetComponent<CharacterManager>().point;

        if (point == 0)//perfect hit
        {
            brokenbokens.Add(Instantiate(
                PerfectBoken, transform.position + new Vector3(
                    bokens[bokencount].transform.position.x, 
                    -halfscreen - bokenaheadofnote -(gamescene.transform.position.y - bokens[bokencount].transform.position.y), 
                    bokenslayer), 
                    transform.rotation));
            
            HideBoken(bokens[bokencount]);
            print ("boken break perfectly");
            bokencount++;
        }
        else if (point == 1)//good hit
        {
            brokenbokens.Add(Instantiate(
                GoodBoken, transform.position + new Vector3(
                    bokens[bokencount].transform.position.x, 
                    -halfscreen - bokenaheadofnote -(gamescene.transform.position.y - bokens[bokencount].transform.position.y), 
                    bokenslayer), 
                    transform.rotation));

            HideBoken(bokens[bokencount]);
            print ("boken break good");
            bokencount++;
        }
        else if (point == 2)//nice hit
        {
            brokenbokens.Add(Instantiate(
                NiceBoken, transform.position + new Vector3(
                    bokens[bokencount].transform.position.x, 
                    -halfscreen - bokenaheadofnote -(gamescene.transform.position.y - bokens[bokencount].transform.position.y), 
                    bokenslayer), 
                    transform.rotation));

            HideBoken(bokens[bokencount]);
            print ("boken break nicely");
            bokencount++;
        }
        else //miss
        {
            if (gamescene.transform.position.y - bokens[bokencount].transform.position.y > pointtofall)
            {
                print ("boken wont break");
                bokencount++;
            }
        }

        //boken disappear when passed
        for (int i = 0; i < bokens.Count; i++)
        {
            if (gamescene.transform.position.y - bokens[i].transform.position.y > pointtodisappear)
            {
                HideBoken(bokens[i]);
            }
        }
        for (int i = 0; i < brokenbokens.Count; i++)
        {
            if (gamescene.transform.position.y - brokenbokens[i].transform.position.y > pointtodisappear)
            {
                HideBoken(brokenbokens[i]);
            }
        }
    }
    void GenerateBoken()
    {   
        if ( UnityEngine.Random.Range(0, 20) % 2 == 0) //even, spawn right
        {
            bokens.Add(Instantiate(bokenright, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointright, 0, bokenslayer), transform.rotation));
            flag = 0; //right
        }
        else //odd, spawn left
        {
            bokens.Add(Instantiate(bokenleft, transform.position + new Vector3(UnityEngine.Random.Range(minX, maxX) + spawnpointleft, 0, bokenslayer), transform.rotation)); 
            flag = 1; //left
        }
    }
    void HideBoken(GameObject obj)
    {
        obj.SetActive(false);
    }   
}

    

