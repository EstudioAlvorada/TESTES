using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Through : MonoBehaviour
{
    //COMPONENTES
    private CapsuleCollider2D col;
    public Transform groundCheck;
    public LayerMask groundLayer; 
    
    //TIPOS PRIMITIVOS
    private bool onGround;
    float tempo;
    bool downJump = false;
    float vertical;


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();//Pegando o collider do personagem e guardando na variavel col

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(vertical);
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);//Checa se está no chão
        vertical = Input.GetAxis("J1Vertical");

        if (vertical < 0 && onGround && Input.GetButton("J1Jump"))
        {
            StartCoroutine(Cai());
        }

    }
    IEnumerator Cai()
    {
        col.enabled = false;
        yield return new WaitForSeconds(.3f);
        col.enabled = true;
    }
}
