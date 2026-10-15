
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 10f;
    public float tempoVida = 3f;

    private float direcao = 1f;

    public void DefinirDirecao(float novaDirecao)
    {
        direcao = novaDirecao;
    }

    void Start()
{
    Debug.Log("Projétil criado!");

    // Desativado temporariamente para testar
    // Destroy(gameObject, tempoVida);
}

    void Update()
{
    // Mantém o projétil parado para testar sua visibilidade
}
}
