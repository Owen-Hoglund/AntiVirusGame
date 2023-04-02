using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class swapTime : MonoBehaviour
{
    [SerializeField] private SwapLogic swapper;
    // [SerializeField] private float time;
    [SerializeField] private TMP_Text time;
     

    // Update is called once per frame
    void Update()
    {
        time.text = "Swap Time Remaining: " + swapper.getTime().ToString("00.00"); 
    }
}
