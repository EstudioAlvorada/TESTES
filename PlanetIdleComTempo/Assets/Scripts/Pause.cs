using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField]
    public GameObject canvas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void chamaPause()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void retomaJogo()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
}
