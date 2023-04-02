using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text fps;
    [SerializeField] string liveFPS;
    // Update is called once per frame
    [SerializeField] float time = 0;
    void Update()
    {
        time+=Time.deltaTime;
        if(time > 0.5){
            fps.text = liveFPS;
            time = 0;
        }
        liveFPS = "FPS - " + (1f / Time.smoothDeltaTime).ToString("000");
    }
}