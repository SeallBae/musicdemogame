using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicCutScript : MonoBehaviour
{
    string [] notes;
    string musicpath, musicfilename;
    public string [] TimeAppear;
    void Start()
    {
        musicfilename = "InfinitePower_TheFatRat_cut.txt";
        musicpath = Application.dataPath + "/AudioDemoWithContent/" + musicfilename;
        ReadFromFile();
    }
    public void ReadFromFile()
    {
        notes = File.ReadAllLines(musicpath);

        int row = 0;
        int col = 0;
        string [] TA = new string[40];

        foreach (string line in notes)
        {
            foreach (string word in line.Split("\t"))
            {
                if((row != 0) && (col == 2)){ //Time Appear row 0 col 2
                    TA[row] = word;
                }
                col++;
            }
            row++;
            col = 0;
        }
        TimeAppear = TA;
        
    }

}
