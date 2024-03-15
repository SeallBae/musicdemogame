using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MusicCutScript : MonoBehaviour
{
    string [] notes;
    string musicpath, musicfilename;
    public string [] TimeAppear;
    int TimeAppearCol = 2;
    int countlines;

    void Awake()
    {
        musicfilename = "InfinitePower_TheFatRat_cut.txt";
        musicpath = Application.dataPath + "/AudioDemoWithContent/" + musicfilename;

        countlines = File.ReadAllLines(musicpath).Length;

        MusicCut(musicpath);
    }

    public void MusicCut(string path)
    {
        notes = File.ReadAllLines(path);

        int row = 0;
        int col = 0;
        string [] TA = new string[countlines - 1]; //exclude the first row

        foreach (string line in notes)
        {
            foreach (string word in line.Split("\t"))
            {
                if((row != 0) && (col == TimeAppearCol)){ //Time Appear row 0 col 2
                    TA[row-1] = word;
                }
                col++;
            }
            row++;
            col = 0;
        }
        TimeAppear = TA;
        // return TA;
    }

    public void test()
    {

    }
}
