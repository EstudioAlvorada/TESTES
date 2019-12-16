using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSettings : MonoBehaviour
{
    private string horizontal;
    private Player2 controle = new Player2();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 1; i <= 2; i++)
        {
            if (Input.GetButton("J" + i + "Fire1"))
            {
               
             
                
            }




        }
    }
}
