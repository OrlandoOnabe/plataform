
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaBotan : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 3;

    private int vidaAtual;
    private bool derrotada = false;

    [Header("Corações na tela")]
    public Image[] coracoes;

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarCoracoes();
    }

    void Update()
    {
    }

    public void ReceberDano(int dano)
    {
        if (derrotada)
            return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);

        AtualizarCoracoes();

        if (vidaAtual <= 0)
        {
            derrotada = true;
            SceneManager.LoadScene("Derrota");
        }
    }

    void AtualizarCoracoes()
    {
        if (coracoes == null)
            return;

        for (int i = 0; i < coracoes.Length; i++)
        {
            if (coracoes[i] != null)
            {
                coracoes[i].enabled = i < vidaAtual;
            }
        }
    }
}
