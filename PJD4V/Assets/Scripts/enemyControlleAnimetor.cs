using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyControlleAnimetor : MonoBehaviour
{
    [SerializeField] private float _velocity = 5f;
    [SerializeField] private Animator _animator;
    public Transform _pontA;
    public Transform _pontB;
    public Transform _Extre_A;
    public Transform _Extre_B;
    private Rigidbody2D rb;

    private float progesso = 0;
    private Vector3 direcao;
    private float distancia;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direcao = (_pontB.position - _pontA.position).normalized;
        distancia = Vector3.Distance(_pontA.position, _pontB.position);
    }
    
    void Update()
    {
        move();
    }

    void move()
    {
        progesso += _velocity * Time.deltaTime;
        transform.Translate(direcao * _velocity * Time.deltaTime);
        
        if(progesso >= 1f)
        {
            progesso = 0;
            TrocarPontos();
        }
    }
    void TrocarPontos()
    {
        Transform temp = _pontB;
        _pontA = _pontB;
        _pontB = temp;
        direcao = -direcao;
    }
}
