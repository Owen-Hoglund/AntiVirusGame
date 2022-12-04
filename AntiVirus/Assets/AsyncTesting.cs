using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks; // Necessary to use Task

public class AsyncTesting : MonoBehaviour
{
    void Start(){
        test(); // Begins our asynchronous Function
        Debug.Log("We Are now Waiting for Test to finish"); // Illustrates that test is running in the background
    }
    private async void test(){

        Debug.Log("Operation Starting"); 
        
        await Task.Run(() => waitThreeSeconds());
        
        Debug.Log("Operation Finished");
    }

    private void waitThreeSeconds(){
        System.Threading.Thread.Sleep(3000);
    }
}
