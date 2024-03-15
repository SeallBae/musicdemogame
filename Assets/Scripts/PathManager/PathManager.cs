using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    [SerializeField] MusicCutScript musiccut;

    void Start()
    {

        for (int i = 0; i < musiccut.TimeAppear.Length; i++)
        {
            print(musiccut.TimeAppear[i]);
        }

    
    }


}
