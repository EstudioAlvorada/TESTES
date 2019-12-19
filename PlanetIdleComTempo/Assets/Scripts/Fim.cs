using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{

    [SerializeField]
    GameObject finalMadeira;
    [SerializeField]
    GameObject finalMina;
    [SerializeField]
    GameObject finalPetroleo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        finalMadeira.GetComponent<UnityEngine.UI.Text>().text = Earth.pontosMadeiraFinais.ToString();
        finalMina.GetComponent<UnityEngine.UI.Text>().text = Earth.pontosMinaFinais.ToString();
        finalPetroleo.GetComponent<UnityEngine.UI.Text>().text = Earth.pontosPetroleoFinais.ToString();
    }

    public void volta()
    {
        SceneManager.LoadScene(0);
    }

    
}
