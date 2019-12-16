using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    ////COMPONENTES: OS COMPONENTES DO UNITY
    Rigidbody2D corpo;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    public Transform finalGroundCheck;
    public LayerMask finalGroundLayer;
    public Transform wallCheck;
    private Animator anim;
    private Vector2 posicaoInicial;
    private Vector2 posicaoQuandoTomou;


    //TIPOS PRIMITIVOS: AS VARIAVEL NORMAL
    private float move = 0;
    private float vertical = 0;
    private float speed = 2;
    private float jumpForce = 75;
    private bool olhandoDireita;
    private bool isJumping;
    private bool onWall;
    private bool onGround;
    private bool onFinalGround;
    private bool doubleJump = false;
    private int contJump = 0;
    private bool upDash = false;
    public GameObject attackHitbox;
    bool isAttacking = false;
    bool tomouDano = false;

    [SerializeField]
    bool noTeclado = false;


    //// Start is called before the first frame update
    void Start()
    {
        
        corpo = GetComponent<Rigidbody2D>(); //Chamando o rigidbody do personagem e guardando na variavel corpo. instanciando rigidibody na variavel corpo
        anim = GetComponent<Animator>(); //Chamando o animator na variavel anim para usar os atributos e métodos de animação no código
        posicaoInicial = transform.position; //Getting the player position when the game starts to use it when he falls.

    }

    // Update is called once per frame
    void Update()
    {

        onWall = Physics2D.Linecast(transform.position, wallCheck.position, wallLayer) || Physics2D.Linecast(transform.position, wallCheck.position, finalGroundLayer);//Checando se o personagem está na parede a partir da posição do objeto wallcheck, se ele está ultrapassando na parade, o personagem está pisando.
        onGround = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);//Checando se o personagem está no chão
        onFinalGround = Physics2D.Linecast(transform.position, groundCheck.position, finalGroundLayer);//Checando se está na última plataforma

        ////Verificando se está apertando o botão de pular
        if (Input.GetButtonDown("J1Jump")) isJumping = true;//Apertando
        if (Input.GetButtonUp("J1Jump")) isJumping = false;//Soltando

        posicaoQuandoTomou = corpo.transform.position;

        if (tomouDano)
        {
            transform.position = /*transform.position*/ posicaoQuandoTomou + UnityEngine.Random.insideUnitCircle * .1f;//Makes the character shake
        }

    }

    private void FixedUpdate()
    {

        vertical = Input.GetAxis("J1Vertical");//pega o eixo vertical
        move = Input.GetAxis("J1Horizontal");//pega o eixo horizontal


        //Se o jogador estiver jogando com o teclado (Por enquanto é só colocar true na variavel noTeclado)
        if (noTeclado)
        {
            if (Input.GetKey("a"))
            {
                corpo.velocity = new Vector2(-speed, corpo.velocity.y);
                transform.localScale = -transform.localScale;
            }
            else if (Input.GetKey("d"))
            {
                corpo.velocity = new Vector2(speed, corpo.velocity.y);
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }

            //if (olhandoDireita)
            //{
            //    transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
            //}
            //else if (!olhandoDireita)
            //{
            //    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            //}
        }
        else
        {
            corpo.velocity = new Vector2(move * speed, corpo.velocity.y);//Setando a velocidade do personagem, se o valor do eixo horizontal, que foi pego na variavel move, for 1 ele vai pra frente na velocidade speed, se for -1 ele vai se mover na velocidade -speed


            //Vira a escala do objeto quando ele vira
            if ((move == 1 && olhandoDireita == true) || (move == -1 && olhandoDireita == false))
            {
                olhandoDireita = !olhandoDireita;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        //if (move > 0.5) move = 1;
        //else if (move < 0.5) move = -1;
        //Debug.Log(vertical);
        //if (move != 0) Debug.Log("FOI2");
        //if (move > 0) corpo.velocity = new Vector2(speed, corpo.velocity.y);
        //else if (move == 0)

        //Faz o personagem correr
        if (Input.GetButton("J1Fire1"))
        {
            corpo.velocity = new Vector2(move * (speed + (speed / 2)), corpo.velocity.y);
        }

        //CHAMANDO OS MÉTODOS
        jumping();
        animations();
        if (corpo.velocity.y < -15) caiu();
        //wallJumping();
        attack();

       
    }


    //MÉTODOS
    void caiu()
    {
        //Retorna o personagem à posição inicial quando ele cair
        corpo.velocity = new Vector2(corpo.velocity.x, 0);
        corpo.transform.position = posicaoInicial;
    }

    void jumping()
    {
        if (onGround || onFinalGround)//Se ele estiver em qualquer plataforma, não poderá dar o dash pra cima
        {
            upDash = false;
        }
        if (isJumping && (onGround || onFinalGround) && vertical >= 0)//Verifica se o botão de pular está pressionado, se está em alguma plataforma e se o valor vertical é maior ou igual a zero para poder pular
        {
            corpo.velocity = new Vector2(corpo.velocity.x, 0);
            corpo.AddForce(new Vector2(0f, jumpForce * 2));
            isJumping = false;
            doubleJump = true;
            anim.Play("generico_pula");
        }
        if (isJumping && doubleJump && (!onGround || onFinalGround))//Verifica se está no ar e se já pulou para dar o segundo pulo
        {
            corpo.velocity = new Vector2(corpo.velocity.x, 0);
            corpo.AddForce(new Vector2(0f, jumpForce * 2));
            doubleJump = false;
            upDash = true;
            isJumping = false;
        }
        if (Input.GetButton("J1Fire3") && vertical == 1 && upDash && !isJumping && !doubleJump)//Verifica se já deu o segundo pulo e se está no ar para poder dar o dash pra cima
        {
            if (Input.GetButton("J1Jump") && Input.GetButton("J1Fire2")) corpo.velocity = new Vector2(corpo.velocity.x, corpo.velocity.y);
            else                    
            {
                corpo.velocity = new Vector2(corpo.velocity.x, 0);
                corpo.AddForce(new Vector2(0f, 200));
                upDash = false;
            }
        }

        //Pulo na parede
        if (onWall && Input.GetButton("J1Jump"))
        {
            corpo.velocity = new Vector2(corpo.velocity.x, 0);
            corpo.AddForce(new Vector2(0f, jumpForce * 2));
            
            anim.Play("generico_pula");
        }
    }

    void animations()
    {
        if (move != 1 && move != -1 && (onGround || onFinalGround) && !isAttacking)//Se não está se movendo de nenhuma forma, chama a animação idle
        {
            anim.Play("generico_idle");
        }
        else if ((move == 1 || move == -1) && corpo.velocity.y == 0 && (onGround || onFinalGround) && !isAttacking)//Se o move não for zero e estiver no chão, chama a animação walk
        {
            anim.Play("generico_anda");
        }
        else if (!onGround && !onFinalGround && !isAttacking) anim.Play("generico_pula");//Se não estiver no chão, chama a animação jump

        if (isAttacking) anim.Play("generico_ataca");
    }

    //ATACANDO
    void attack()
    {
        if (Input.GetButton("J1Fire3") && !isAttacking)//Se está apertando o botão de ataque e não está atacando, ele passa a estar atacando e aciona a rotina DoAttack()
        {
            isAttacking = true;
            StartCoroutine(DoAttack());          
        }
    }

    IEnumerator DoAttack()//Ativa a hitbox de ataque por .25 segundos e define a variavel isAttacking para false
    {
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(.25f);
        attackHitbox.SetActive(false);
        isAttacking = false;
    }

    //TOMANDO DANO
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "attackHitbox2")//Se este personagem entrar em colisão com a hitbox de outro personagem, ele ira invocar a função Stopshaking que deixa a variavel tomou dano verdadeira por .1 segundo
        {
            tomouDano = true;
        }
        Invoke("StopShaking", .1f);
    }

    void StopShaking()
    {
        tomouDano = false;

    }

}

