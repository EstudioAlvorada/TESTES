using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    
    
    public void inicia()
    {
       
        Earth.recomecou = true;
        
        SceneManager.LoadScene(1);
        
    }

    //public void reseta()
    //{
    //    Earth.pontos = 0;
    //    Earth.pontosMadeira = 0;
    //    Earth.pontosMina = 0;
    //    Earth.pontosPetroleo = 0;
    //    Earth.quantDobro = 0;
    //    Earth.quantMina = 0;
    //    Earth.quantPetroleo = 0;
    //}

}
