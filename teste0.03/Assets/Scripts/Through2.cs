using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Through2 : MonoBehaviour
{
    //COMPONENTES
    private CapsuleCollider2D col2;
    public Transform groundCheck2;
    public LayerMask groundLayer2;

    //TIPOS PRIMITIVOS
    private bool onGround2;
    float tempo2;
    bool downJump2 = false;
    float vertical2;
    // Start is called before the first frame update


    void Start()
    {
        col2 = GetComponent<CapsuleCollider2D>();//Pegando o collider do personagem e guardando na variavel col

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(vertical);
        onGround2 = Physics2D.Linecast(transform.position, groundCheck2.position, groundLayer2);//Checa se está no chão
        vertical2 = Input.GetAxis("Vertical2");//Recebe o valor do eixo vertical

        //Verifica se está no chão, se o valor vertical é menor do que zero e se está pressionando para pular
        if (vertical2 < 0 && onGround2 && Input.GetButton("J2Jump"))
        {
            //Chama a rotina Cai();
            StartCoroutine(Cai());
        }

    }
    IEnumerator Cai()
    {
        //Desabilita o colisor por .3 segundos  
        col2.enabled = false;
        yield return new WaitForSeconds(.3f);
        col2.enabled = true;
    }

}

