using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CubeCheckerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    
    void Start()
    {
        CubeChecker.OnCubeAdded += UpdateUi;
    }

    private void UpdateUi(object sender, CubeCheckerEventArgs e)
    {
        if(e.cubeChecker.GetCubeCount() == e.cubeChecker.GetMaxCubeCount())
        {
            text.text = $"Congratulation! You have Added all {e.cubeChecker.GetCubeCount()} cubes";
   
        }

        else
        {
            text.text = $"Current Added cube {e.cubeChecker.GetCubeCount()}/{5}";
        }
    }


}
