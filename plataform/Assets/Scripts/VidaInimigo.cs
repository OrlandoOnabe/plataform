
using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    public int vidaMaxima = 3;

    private int vidaAtual;

    void Start()
    {
        vidaAtual = vidaMaxima;
    }

    void Update()
    {
        // Atualizações contínuas, se necessárias.
    }

    public void ReceberDano(int dano)
    {
        vidaAtual -= dano;

        Debug.Log("Vida do inimigo: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        if (GerenciadorPontuacao.instancia != null)
        {
            GerenciadorPontuacao.instancia.InimigoDerrotado(100);
        }

        Destroy(gameObject);
    }
}
