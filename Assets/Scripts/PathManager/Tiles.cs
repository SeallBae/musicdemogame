using System;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public GameObject tile;
    // public Sprite PressNote;

    // public GameObject GoodNoti;
    // private int notislayer = -4;

    [SerializeField] MusicCutScript musiccut;
    private double [] TA;
    // private int timeround = 2;

    // private int count;
    private int scrolltime = 1;

    // private float fallspeed = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        //Time stamp of notes array
        TA = new double [musiccut.TimeAppear.Length + 1];
        // count = 0;
        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            TA[i] = musiccut.TimeAppear[i] + scrolltime;
        }
    }

    // Update is called once per frame
    void Update()
    {




        // Time method
        // if ( (count <= TA.Length - 1))
        // {
        //     if (Math.Round(Time.time, timeround) > TA[count])
        //     {
        //         this.tile.transform.Translate(-5, 0, 0);
        //     }    
        // }
    }
}
