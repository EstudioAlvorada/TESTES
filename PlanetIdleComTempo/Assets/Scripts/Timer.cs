using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    Text contador;
    [SerializeField]
    GameObject terra;
    [SerializeField]
    GameObject explosao;

    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject pedra;
    [SerializeField]
    GameObject particulas;
    private Vector2 screenBounds;

    public static bool acabou = false;
    
   
    public float startingTime = 10f;
    public float currentTime = 0;
    public bool isCountingDown = true;
    //public void Begin()
    //{
    //    if (!isCountingDown)
    //    {
    //        isCountingDown = true;
    //        timeRemaining = duration;
    //        Invoke("_tick", 1f);
    //    }
    //}
    private void Start()
    {
        currentTime = startingTime;
        StartCoroutine(meteoroAparece());
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));


    }
    private void Update()
    {
        if (Earth.iniciado)
        {
            //Invoke("_tick", 1f);

            currentTime -= 1 * Time.deltaTime;

            int segundo = (int)currentTime % 60;
            int minuto = (int)(currentTime / 60) % 60;

            if (minuto  < 1) contador.color = Color.red;
            
            else contador.color = Color.green;

            contador.text = string.Format("{0:0}:{1:00}", minuto, segundo);

            if (minuto <= 0 && segundo <= 0)
            {
                Instantiate(explosao, transform.position, transform.rotation);
                Destroy(terra);
                GameObject textoMeio = (GameObject.FindGameObjectWithTag("Texto"));
                textoMeio.transform.position = new Vector2(transform.position.x, transform.position.y);
                textoMeio.GetComponent<UnityEngine.UI.Text>().color = Color.white;
                textoMeio.GetComponent<UnityEngine.UI.Text>().fontSize = 80;

                textoMeio.GetComponent<UnityEngine.UI.Text>().text = "FIM DO JOGO!";
                contador.text = " ";

                acabou = true;

                if (segundo < -3)
                    SceneManager.LoadScene(2);

            }
        }

        Vector3 pos = Input.mousePosition;
        Vector2 posicaoMouse = cam.ScreenToWorldPoint(pos);
        Collider2D colisor = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));

        if (Input.GetMouseButtonDown(0))
        {
            if (colisor != null && colisor.CompareTag("Meteoro"))
            {
                Instantiate(particulas, posicaoMouse, Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("Meteoro"));

                currentTime += 25;
            }

        }
           



    }
    //private void _tick()
    //{
    //    timeRemaining--;
    //    if (timeRemaining > 0)
    //    {
    //        Invoke("_tick", 2f);
    //    }
    //    else
    //    {
    //        isCountingDown = false;
    //    }
    //}

    public void meteoro()
    {
        GameObject a = Instantiate(pedra) as GameObject;
        a.transform.position = new Vector2(Random.Range(4, 6), Random.Range(-screenBounds.y + 2, screenBounds.y - 2));

    }
    IEnumerator meteoroAparece()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(35, 80));
            meteoro();
        }
    }

}
