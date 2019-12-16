using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //COMPONENTES
    private Transform player;
    private Transform player2;
    private Vector3 posCam;

    //TIPOS PRIMITIVOS
    private float posx;
    private float posy;
    private float media;
    private float maior;
    private float menor;
    public float distancia;
    // Start is called before the first frame update
    void Start()
    {
        //Pegando a posição dos dois personagens
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Pegando a media da posição dos dois personagens
        posCam.x = (player.position.x + player2.position.x) / 2;
        posCam.y = (player.position.y + player2.position.y) / 2;
        posCam.z = -50;
        media = posCam.x;

        //Descobrindo qual eixo é o maior
        if(player.position.x > player.position.y)
        {
            maior = player.position.x;
            menor = player2.position.x;
        }
        else
        {
            menor = player.position.x;
            maior = player2.position.x;
        }
        //Calculando a distância entre eles
        distancia = maior - menor;


        //Debug.Log(distancia);

        //if(distancia < 4.5f || distancia < -4.5f)
        //{
        //    Camera.current.orthographicSize = 1f;

        //}
        /*else */

        //Aumentando e diminuindo a camera dependendo da distancia entre os dois personagens
        if (distancia > 7f || distancia < -7f)
        {
            Camera.current.orthographicSize = 4f;
        }
        else if (distancia > 3.9f || distancia < -3.9f)
        {
            Camera.current.orthographicSize = 3f;

        }     
        //else
        //{
        //    Camera.current.orthographicSize = 2f;
        //}

        //else if (posCam.x > -9.5f)
        //{
        //    Camera.current.orthographicSize = 2f;
        //}

        //if (posCam.x < -8f)
        //{
        //    Camera.current.orthographicSize = 1f;

        //}

        //Debug.Log("X: " + posCam.x + " Y: " + posCam.y);

        //Setando a posição da camera para ser o meio dos dois personagens
        transform.position = posCam;


    }
}
