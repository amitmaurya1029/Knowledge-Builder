using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeCheckerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private bool canUpdateUi = true;
    private int maxCubeCount = 5;
    
    void Start()
    {
        CubeChecker.OnCubeAdded += UpdateUi;
    }

    private void UpdateUi(object sender, CubeCheckerEventArgs e)
    {
        Debug.Log("value cube : " + e.cubeChecker.GetCubeCount());
        
        if (!canUpdateUi) {return;}

        if(e.cubeChecker.GetCubeCount() == maxCubeCount)
        {
            text.text = $"You have Added all {e.cubeChecker.GetCubeCount()} cubes";
            canUpdateUi = false;
        }

        else
        {
            text.text = $"Current Added cube {e.cubeChecker.GetCubeCount()}/{5}";
        }
    }

    

}
