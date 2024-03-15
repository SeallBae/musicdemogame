using UnityEngine;
using System;


public class MovingTiles : MonoBehaviour
{
    public GameObject tiles;
    public float topleft = -200;
    public float topright = 200;
    public float timeBetweenGenerate;
    private float generateTime;
    
    [SerializeField] MusicCutScript musiccut;

    float[] TA;

    int count = 0;
    void OnEnable()
    {
        TA = new float [musiccut.TimeAppear.Length];
        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            float temp = float.Parse(musiccut.TimeAppear[i]);
            TA[i] = temp;
        }
        // print( Math.Round(TA[0], 2));
    
    }
    void Update()
    {
        // print(TA[0]);

        // print(Time.time);

        if ( Math.Round(Time.time,2) == Math.Round(TA[count], 2))
        {
            print("reach");
            Generate();
            generateTime = Time.time + timeBetweenGenerate;
            count++;
        }

        // for (int i = 0; i < TA.Length; i++)
        // {
        //     if (Time.time == TA[i])
        //     {
        //         Generate();
        //     }
        // }
    }

    void Generate()
    {
        GameObject tile = Instantiate(tiles, transform.position + new Vector3(topright, 0, 0), transform.rotation);
    }
}
