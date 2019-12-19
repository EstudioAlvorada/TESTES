using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fundo : MonoBehaviour
{
    SpriteRenderer imagem;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        imagem = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play("fundo");
    }
}
