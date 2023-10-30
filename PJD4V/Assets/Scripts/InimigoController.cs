using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoController : MonoBehaviour
{
    public Transform pontoDePatrulhaA;
    public Transform pontoDePatrulhaB;
    public Transform pontoDeInteresseA;
    public Transform pontoDeInteresseB;

    public float velocidadePatrulha = 2f;
    public float velocidadePerseguicao = 5f;
    public float distanciaDetecao = 10f;
    private bool jogadorDentroDaAreaDeInteresse = false;

    private Transform pontoDePatrulhaAtual;
    private int indicePontoAtual = 0;

    private void Start()
    {
        // Defina as referências para os pontos de patrulha e interesse.
        pontoDePatrulhaA = transform.Find("PontoDePatrulhaA");
        pontoDePatrulhaB = transform.Find("PontoDePatrulhaB");
        pontoDeInteresseA = transform.Find("PontoDeInteresseA");
        pontoDeInteresseB = transform.Find("PontoDeInteresseB");

        // Inicialize o ponto de patrulha atual.
        pontoDePatrulhaAtual = pontoDePatrulhaA;
    }

    private void Update()
    {
        // Verifique se o jogador está dentro da área de interesse.
        GameObject jogadorObjeto = GameObject.FindWithTag("Player");

        if (jogadorObjeto != null)
        {
            float distanciaJogador = Vector3.Distance(transform.position, jogadorObjeto.transform.position);
            jogadorDentroDaAreaDeInteresse = distanciaJogador <= distanciaDetecao;
        }

        // Controle de movimento com base na área de interesse.
        if (jogadorDentroDaAreaDeInteresse)
        {
            // Perseguir o jogador com a velocidade de perseguição.
            float velocidadeMovimento = velocidadePerseguicao;
            MoverParaJogador(velocidadeMovimento);
        }
        else
        {
            // Retornar aos pontos de patrulha com a velocidade de patrulha.
            float velocidadeMovimento = velocidadePatrulha;
            MoverParaPontoDePatrulha(velocidadeMovimento);
        }
    }

    private void MoverParaJogador(float velocidadeMovimento)
    {
        // Encontre o jogador pelo tag.
        GameObject jogadorObjeto = GameObject.FindWithTag("Player");

        if (jogadorObjeto != null)
        {
            Vector3 direcao = (jogadorObjeto.transform.position - transform.position).normalized;
            transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);
        }
    }

    private void MoverParaPontoDePatrulha(float velocidadeMovimento)
    {
        Vector3 direcao = (pontoDePatrulhaAtual.position - transform.position).normalized;
        transform.Translate(direcao * velocidadeMovimento * Time.deltaTime);

        // Se o inimigo estiver próximo o suficiente ao ponto de patrulha, selecione o próximo ponto.
        float distanciaAoPonto = Vector3.Distance(transform.position, pontoDePatrulhaAtual.position);
        if (distanciaAoPonto < 0.1f)
        {
            // Alterne entre os pontos de patrulha.
            if (pontoDePatrulhaAtual == pontoDePatrulhaA)
            {
                pontoDePatrulhaAtual = pontoDePatrulhaB;
            }
            else
            {
                pontoDePatrulhaAtual = pontoDePatrulhaA;
            }
        }
    }
}
