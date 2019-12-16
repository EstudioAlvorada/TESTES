using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    //COMPONENTES
    Rigidbody2D corpo2;
    public Transform groundCheck2;
    public LayerMask groundLayer2;
    private Animator anim2;
    public Transform finalGroundCheck2;
    public LayerMask finalGroundLayer2;
    private Vector2 posicaoInicial2;
    private Vector2 posicaoQuandoTomou2;

    //TIPOS PRIMITIVOS
    private float move2 = 0;
    private float vertical2 = 0;
    private float speed2 = 2;
    private float jumpForce2 = 75;
    private bool olhandoDireita2;
    private bool isJumping2;
    private bool onFinalGround2;
    private bool onGround2;
    private bool doubleJump2;
    private bool upDash2;
    private bool tomouDano2 = false;
    public GameObject attackHitbox2;
    bool isAttacking2 = false;



    // Start is called before the first frame update
    void Start()
    {
        corpo2 = GetComponent<Rigidbody2D>();//Chamando o rigidbody do personagem e guardando na variavel corpo.
        anim2 = GetComponent<Animator>();//Chamando o animator na variavel anim para usar os atributos e métodos de animação no código
        posicaoInicial2 = transform.position;//Getting the player position when the game starts to use it when he falls.

    }

    // Update is called once per frame
    void Update()
    {
        //if (pode)
        //{
        //    posicaoInicial = transform.position;
        //    pode = false;
        //}

        onGround2 = Physics2D.Linecast(transform.position, groundCheck2.position, groundLayer2);//Checando se o personagem está no chão
        onFinalGround2 = Physics2D.Linecast(transform.position, groundCheck2.position, finalGroundLayer2);//Checando se está na última plataforma

        //Verificando se está apertando o botão de pular
        if (Input.GetButtonDown("J2Jump")) isJumping2 = true;//Apertando
        if (Input.GetButtonUp("J2Jump")) isJumping2 = false;//Soltando

        posicaoQuandoTomou2 = corpo2.transform.position;

        if (tomouDano2)
        {
            transform.position = /*transform.position*/ posicaoQuandoTomou2 + UnityEngine.Random.insideUnitCircle * .1f;//Makes the character shake
        }
    }

    //public void configuraBotoes(string num)
    //{
    //    horizontal = "Horizontal" + num;
    //    vertical = "Vertical" + num;
    //    pular = "J" + num + "Jump";
    //    correr = "J" + num + "Fire1";
    //    ataque = "J" + num + "Fire3";
    //}

    private void FixedUpdate()
    {


        vertical2 = Input.GetAxis("Vertical2");//pega o eixo vertical
        move2 = Input.GetAxis("Horizontal2");//pega o eixo horizontal
        
        
        corpo2.velocity = new Vector2(move2 * speed2, corpo2.velocity.y);//Setando a velocidade do personagem, se o valor do eixo horizontal, que foi pego na variavel move, for 1 ele vai pra frente na velocidade speed, se for -1 ele vai se mover na velocidade -speed

     
        //Vira a escala do objeto quando ele vira
        if ((move2 == 1 && olhandoDireita2 == true) || (move2 == -1 && olhandoDireita2 == false))
        {
            olhandoDireita2 = !olhandoDireita2;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        //Faz o personagem correr
        if (Input.GetButton("J2Fire1"))
        {
            corpo2.velocity = new Vector2(move2 * (speed2 + (speed2/2)), corpo2.velocity.y);
        }

        //CHAMANDO OS MÉTODOS
        jump2();
        animation2();
        if (corpo2.velocity.y < -15) caiu2();
        attack();

    }


    //MÉTODOS
    void caiu2()
    {
        //Retorna o personagem à posição inicial quando ele cair
        corpo2.velocity = new Vector2(corpo2.velocity.x, 0);
        corpo2.transform.position = posicaoInicial2;
    }

    void jump2()
    { 
        if (onGround2 || onFinalGround2)
        {
            upDash2 = false;
        }
        if (isJumping2 && (onGround2 || onFinalGround2) && vertical2 >= 0)//Verifica se o botão de pular está pressionado, se está em alguma plataforma e se o valor vertical é maior ou igual a zero para poder pular
        {
            corpo2.velocity = new Vector2(corpo2.velocity.x, 0);
            corpo2.AddForce(new Vector2(0f, jumpForce2 * 2));
            isJumping2 = false;
            doubleJump2 = true;
            anim2.Play("generico_pula");
        }
        if (isJumping2 && doubleJump2 && (!onGround2 || onFinalGround2))//Verifica se está no ar e se já pulou para dar o segundo pulo
        {
            corpo2.velocity = new Vector2(corpo2.velocity.x, 0);
            corpo2.AddForce(new Vector2(0f, jumpForce2 * 2));
            doubleJump2 = false;
            upDash2 = true;
            isJumping2 = false;
        }
        if (Input.GetButton("J2Fire3") && vertical2 == 1 && upDash2 && !isJumping2 && !doubleJump2)//Verifica se já deu o segundo pulo e se está no ar para poder dar o dash pra cima
        {
            if (Input.GetButton("J2Jump") && Input.GetButton("J2Fire2")) corpo2.velocity = new Vector2(corpo2.velocity.x, corpo2.velocity.y);
            else
            {
                corpo2.velocity = new Vector2(corpo2.velocity.x, 0);
                corpo2.AddForce(new Vector2(0f, 200));
                upDash2 = false;
            }
        }
    }
    void animation2()
    {
        if (move2 != 1 && move2 != -1 && (onGround2 || onFinalGround2) && !isAttacking2)//Se não está se movendo de nenhuma forma, chama a animação idle
        {
            anim2.Play("generico_idle");
        }
        else if ((move2 == 1 || move2 == -1) && corpo2.velocity.y == 0 && (onGround2 || onFinalGround2) && !isAttacking2)//Se o move não for zero e estiver no chão, chama a animação walk
        {
            anim2.Play("generico_anda");
        }
        else if (!onGround2 && !onFinalGround2 && !isAttacking2) anim2.Play("generico_pula");//Se não estiver no chão, chama a animação jump

        if (isAttacking2) anim2.Play("generico_ataca");

    }

    //ATACANDO
    void attack()
    {
        if (Input.GetButton("J2Fire3") && !isAttacking2)//Se está apertando o botão de ataque e não está atacando, ele passa a estar atacando e aciona a rotina DoAttack()
        {
            //Debug.Log("Atacou");

            isAttacking2 = true;
            StartCoroutine(DoAttack());

        }
    }

    IEnumerator DoAttack()//Ativa a hitbox de ataque por .25 segundos e define a variavel isAttacking para false
    {
        attackHitbox2.SetActive(true);
        yield return new WaitForSeconds(.25f);
        attackHitbox2.SetActive(false);
        isAttacking2 = false;
    }

    //TOMANDO DANO
    private void OnTriggerEnter2D(Collider2D collision)//Se este personagem entrar em colisão com a hitbox de outro personagem, ele ira invocar a função Stopshaking que deixa a variavel tomou dano verdadeira por .1 segundo
    {
        if (collision.gameObject.name == "attackHitbox")
        {
            //Debug.Log("Acertou");
            tomouDano2 = true;
        }
        Invoke("StopShaking", .1f);
    }

    void StopShaking()
    {
        tomouDano2 = false;

    }
}
