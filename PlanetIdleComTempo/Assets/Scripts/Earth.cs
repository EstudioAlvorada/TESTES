using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Earth : MonoBehaviour
{
   
    float speed = 1.0f; 
    float amount = 1.0f; 
    Vector3 posicaoIncial;
    public int pontos { get; set; } = 0;
    int quantMadeireiras = 0;
    int quantPetroleo = 0;
    int quantMina = 0;
    int quantDobro = 1;
    public static bool recomecou = false;
    
    [SerializeField]
    GameObject texto;
    [SerializeField]
    public GameObject particulas;

    //CONTAGEM
    float ultimoTempoMadeira = 0f;
    float ultimoTempoMina = 0f;
    float ultimoTempoPetroleo = 0f;
    float velocidadeMadeira = 1f;
    float velocidadeMina = 3f;
    float velocidadePetroleo = 5f;
    [SerializeField]
    Camera cam;

    int pontosMadeira = 0;
    [SerializeField]
    GameObject textoPontosMadeira;

    int pontosMina = 0;
    [SerializeField]
    GameObject textoPontosMina;

    int pontosPetroleo = 0;
    [SerializeField]
    GameObject textoPontosPetroleo;

    [SerializeField]
    GameObject terra;

    [SerializeField]
    GameObject explosao;

    [SerializeField]
    AudioClip clipTerra;
    [SerializeField]
    AudioSource toqueTerra;

    [SerializeField]
    AudioClip clipCompra;
    [SerializeField]
    AudioSource toqueCompra;




    int valorMadeira = 35;
    int valorMina = 150;
    int valorPetroleo = 350;
    int valorDobro = 5000;

    public GameObject particulas2;

    int pontuacao = 0;

    public static bool iniciado = false;

    //Butões
    [SerializeField]
    GameObject btnMinera;
    [SerializeField]
    GameObject btnPetroleo;
    [SerializeField]
    GameObject btnMadeira;
    [SerializeField]
    GameObject btnDobro;


    //Textos de custo
    [SerializeField]
    GameObject custoMadeiraTxt;
    [SerializeField]
    GameObject custoMinaTxt;
    [SerializeField]
    GameObject custoPetroleoTxt;
    [SerializeField]
    GameObject custoDobroTxt;
    [SerializeField]
    GameObject madeiraParaMina;
    [SerializeField]
    GameObject madeiraParaPetroleo;
    [SerializeField]
    GameObject minaParaPetroleo;

    ////Textos de quantidades
    [SerializeField]
    GameObject quantMadeiraTxt;
    [SerializeField]
    GameObject quantMinaTxt;
    [SerializeField]
    GameObject quantPetroleoTxt;
    [SerializeField]
    GameObject quantDobroTxt;

    [SerializeField]
    GameObject textoPontosMadeira2;

    //pontuação que será mostrada no fim do jogo
    public static int pontosMadeiraFinais = 0;
    public static int pontosMinaFinais = 0;
    public static int pontosPetroleoFinais = 0;

    public Random rand = new Random();

    [SerializeField]
    public GameObject pedra;

    public float velocidade = 10.0f;
    private Vector2 screenBounds;
    public Rigidbody2D rb;

    public bool meteoroSaiu;
    

    // Start is called before the first frame update
    void Start()
    {
        posicaoIncial = transform.position;

        toqueTerra = GetComponent<AudioSource>();

        toqueCompra = GetComponent<AudioSource>();

        rb = pedra.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        toqueTerra.volume = 0.351f;
        toqueTerra.pitch = 1.40f;
        toqueCompra.volume = 0.351f;

    }


    // Update is called once per frame
    private void Update()
    {

        rb.AddForce(new Vector2(-velocidade, 0));

        pontosMadeiraFinais = pontosMadeira;
        pontosPetroleoFinais = pontosPetroleo;
        pontosMinaFinais = pontosMina;

        if (Input.GetMouseButtonDown(0))
        {            
            Vector3 pos = Input.mousePosition;
            Vector2 posicaoMouse = cam.ScreenToWorldPoint(pos);
            Collider2D colisor = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(pos));
            if (colisor != null && colisor.CompareTag("Player"))
            {
                if (GameObject.FindGameObjectWithTag("Texto"))
                {
                    GameObject textoMeio = (GameObject.FindGameObjectWithTag("Texto"));
                    textoMeio.GetComponent<UnityEngine.UI.Text>().text = "";
                    
                    iniciado = true;
                }

               
                StartCoroutine(Shake());
                Instantiate(particulas, posicaoMouse, Quaternion.identity);
                pontos += 1 * quantDobro;
                
            }        

        }

        //if (pontos >= 100)
        //{
            
            
        //}
       
        if (quantMadeireiras > 0)
        {
            //Invoke("madeireiraPontos", 3f);
            
            if(Time.time - ultimoTempoMadeira >= velocidadeMadeira)
            {
                ultimoTempoMadeira = Time.time;
                madeireiraPontos();
            }
        }
        if (quantMina > 0)
        {
            //Invoke("madeireiraPontos", 3f);          
            if(Time.time - ultimoTempoMina >= velocidadeMina)
            {
                ultimoTempoMina = Time.time;
                minaPontos();
            }
        }
        if (quantPetroleo > 0)
        {
            //Invoke("madeireiraPontos", 3f);
            
            if(Time.time - ultimoTempoPetroleo >= velocidadePetroleo)
            {
                ultimoTempoPetroleo = Time.time;
                petroleoPontos();


            }
        }
        string dinheiro = "$";
        texto.GetComponent<UnityEngine.UI.Text>().text = pontos.ToString() + "  $";
        textoPontosMadeira.GetComponent<UnityEngine.UI.Text>().text = pontosMadeira.ToString();
        textoPontosMina.GetComponent<UnityEngine.UI.Text>().text = pontosMina.ToString();
        textoPontosPetroleo.GetComponent<UnityEngine.UI.Text>().text = pontosPetroleo.ToString();
        
    }

    //IEnumerator gameOver(float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene(0);
    //}

    
        
    IEnumerator Shake()
    {
        transform.position = posicaoIncial + UnityEngine.Random.insideUnitSphere * .2f;

        yield return new WaitForSeconds(.1f);
        toqueTerra.clip = clipTerra;
        toqueTerra.Play();
        transform.position = posicaoIncial;
    }
    void madeireiraPontos()
    {
        
        pontosMadeira += quantMadeireiras * quantDobro;
        pontos += quantMadeireiras * quantDobro;
        Instantiate(particulas, transform.position, transform.rotation);
        
        StartCoroutine(Shake());

    }
    void minaPontos()
    {
        pontos+=quantMina * quantDobro * 5;
        pontosMina += (quantMina * 3) * quantDobro;
        Instantiate(particulas, transform.position, transform.rotation);
        

        StartCoroutine(Shake());


    }
    void petroleoPontos()
    {
        
        pontosPetroleo += (quantPetroleo * 15) * quantDobro;
        pontos+= (quantPetroleo) * quantDobro * 15;
        Instantiate(particulas, transform.position, transform.rotation);
        

        StartCoroutine(Shake());


    }





    //--------------- -------- --------- ------------ ----------- ---------- -------- -------------- ----------- --------------- ------------ ----------- ------- 


    



    public void FixedUpdate()
    {
        pontuacao = pontos;

        if (Timer.acabou)
        {
            iniciado = false;
        }

        if (int.Parse(custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().text) > pontuacao)
        {
            custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().color = Color.red;
            custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().text = valorMadeira.ToString();
        }
        else if (int.Parse(custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().text) <= pontuacao)
        {
            custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().color = Color.green;
            custoMadeiraTxt.GetComponent<UnityEngine.UI.Text>().text = valorMadeira.ToString();

        }

        if (int.Parse(custoMinaTxt.GetComponent<UnityEngine.UI.Text>().text) > pontuacao)
        {
            custoMinaTxt.GetComponent<UnityEngine.UI.Text>().color = Color.red;
            custoMinaTxt.GetComponent<UnityEngine.UI.Text>().text = valorMina.ToString();
        }
        else if (int.Parse(custoMinaTxt.GetComponent<UnityEngine.UI.Text>().text) <= pontuacao)
        {
            custoMinaTxt.GetComponent<UnityEngine.UI.Text>().color = Color.green;
            custoMinaTxt.GetComponent<UnityEngine.UI.Text>().text = valorMina.ToString();

        }

        if (int.Parse(custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().text) > pontuacao)
        {
            custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().color = Color.red;
            custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().text = valorPetroleo.ToString();
        }
        else if (int.Parse(custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().text) <= pontuacao)
        {
            custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().color = Color.green;
            custoPetroleoTxt.GetComponent<UnityEngine.UI.Text>().text = valorPetroleo.ToString();

        }

        if (int.Parse(custoDobroTxt.GetComponent<UnityEngine.UI.Text>().text) > pontuacao)
        {
            custoDobroTxt.GetComponent<UnityEngine.UI.Text>().color = Color.red;
            custoDobroTxt.GetComponent<UnityEngine.UI.Text>().text = valorDobro.ToString();
        }
        else if (int.Parse(custoDobroTxt.GetComponent<UnityEngine.UI.Text>().text) <= pontuacao)
        {
            custoDobroTxt.GetComponent<UnityEngine.UI.Text>().color = Color.green;
            custoDobroTxt.GetComponent<UnityEngine.UI.Text>().text = valorDobro.ToString();

        }

        if (int.Parse(madeiraParaMina.GetComponent<UnityEngine.UI.Text>().text) < pontosMadeira)
        {
            madeiraParaMina.GetComponent<UnityEngine.UI.Text>().color = Color.green;

        }
        else if (int.Parse(madeiraParaMina.GetComponent<UnityEngine.UI.Text>().text) >= pontosMadeira)
        {
            madeiraParaMina.GetComponent<UnityEngine.UI.Text>().color = Color.red;


        }

        if (int.Parse(madeiraParaPetroleo.GetComponent<UnityEngine.UI.Text>().text) < pontosMadeira)
        {
            madeiraParaPetroleo.GetComponent<UnityEngine.UI.Text>().color = Color.green;

        }
        else if (int.Parse(madeiraParaPetroleo.GetComponent<UnityEngine.UI.Text>().text) >= pontosMadeira)
        {
            madeiraParaPetroleo.GetComponent<UnityEngine.UI.Text>().color = Color.red;


        }

        if (int.Parse(minaParaPetroleo.GetComponent<UnityEngine.UI.Text>().text) < pontosMina)
        {
            minaParaPetroleo.GetComponent<UnityEngine.UI.Text>().color = Color.green;

        }
        else if (int.Parse(minaParaPetroleo.GetComponent<UnityEngine.UI.Text>().text) >= pontosMina)
        {
            minaParaPetroleo.GetComponent<UnityEngine.UI.Text>().color = Color.red;


        }
        quantMadeiraTxt.GetComponent<UnityEngine.UI.Text>().text = "Quant: " + quantMadeireiras;
        quantPetroleoTxt.GetComponent<UnityEngine.UI.Text>().text = "Quant: " + quantPetroleo;
        quantMinaTxt.GetComponent<UnityEngine.UI.Text>().text = "Quant: " + quantMina;
        quantDobroTxt.GetComponent<UnityEngine.UI.Text>().text = "Quant: " + (quantDobro - 1);



        if (iniciado == true)
        {
            btnMadeira.SetActive(true);
            btnMinera.SetActive(true);
            btnPetroleo.SetActive(true);
            btnDobro.SetActive(true);
        }

        if (Timer.acabou)
        {
            btnMadeira.SetActive(false);
            btnMinera.SetActive(false);
            btnPetroleo.SetActive(false);
            btnDobro.SetActive(false);
            Timer.acabou = false;
        }
    }
    public void liberaMadeireia()
    {
        if (pontuacao >= valorMadeira)
        {
            Vector3 pos = Input.mousePosition;
            Vector2 posicaoMouse = cam.ScreenToWorldPoint(pos);
            pontos -= valorMadeira;
            toqueCompra.clip = clipCompra;
            toqueCompra.Play();


            quantMadeireiras += 1;
            valorMadeira += 5;
            //Handheld.Vibrate();

        }
    }

    public void liberaMina()
    {
        if (pontuacao >= valorMina && pontosMadeira >= 50)
        {
            Vector3 pos = Input.mousePosition;
            Vector2 posicaoMouse = cam.ScreenToWorldPoint(pos);

            pontos -= valorMina;
            pontosMadeira -= 50;
            quantMina += 1;
            valorMina += 10;
            toqueCompra.clip = clipCompra;
            toqueCompra.Play();

            //Handheld.Vibrate();
        }
    }
    public void liberaPetroleo()
    {
        if (pontuacao >= valorPetroleo && pontosMadeira >= 250 && pontosMina >= 150)
        {
            Vector3 pos = Input.mousePosition;
            Vector2 posicaoMouse = cam.ScreenToWorldPoint(pos);

            pontos -= valorPetroleo;
            pontosMadeira -= 250;
            pontosMina -= 150;
            quantPetroleo += 1;
            valorPetroleo += 20;
            toqueCompra.clip = clipCompra;
            toqueCompra.Play();

            //Handheld.Vibrate();
        }
    }
    public void liberaDobro()
    {
        if (pontuacao >= valorDobro)
        {

            pontos -= valorDobro;
            quantDobro += 1;
            valorDobro += 1000;
            valorMadeira += 10;
            valorMina += 20;
            valorPetroleo += 50;
            toqueCompra.clip = clipCompra;
            toqueCompra.Play();

            //Handheld.Vibrate();
        }
    }

}
