
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GerenciadorPontuacao : MonoBehaviour
{
    public static GerenciadorPontuacao instancia;

    public TextMeshProUGUI textoPontuacao;

    [Header("Chave da fase")]
    public GameObject chaveMagica;
    public Transform jogador;
    public Vector3 deslocamentoChave = new Vector3(1f, 0f, 0f);

    [Header("Ícone da chave na interface")]
    public Image iconeChave;

    private int pontuacao = 0;
    private int inimigosDerrotados = 0;
    private bool chaveLiberada = false;

    public bool chaveColetada = false;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        AtualizarTexto();

        if (chaveMagica != null)
        {
            chaveMagica.SetActive(false);
        }

        AtualizarIconeChave();
    }

    public void AdicionarPontos(int pontos)
    {
        pontuacao += pontos;
        AtualizarTexto();
    }

    public void InimigoDerrotado(int pontos)
    {
        AdicionarPontos(pontos);

        if (chaveLiberada)
        {
            return;
        }

        inimigosDerrotados++;

        if (inimigosDerrotados >= 1)
        {
            LiberarChave();
        }
    }

    void LiberarChave()
    {
        if (chaveMagica == null)
        {
            Debug.LogError(
                "Associe o objeto Key no campo Chave Magica!"
            );
            return;
        }

        if (jogador == null)
        {
            Debug.LogError(
                "Associe a Botan no campo Jogador!"
            );
            return;
        }

        chaveMagica.transform.position =
            jogador.position + deslocamentoChave;

        chaveMagica.SetActive(true);
        chaveLiberada = true;
    }

    public void ColetarChave()
    {
        if (chaveMagica != null && !chaveColetada)
        {
            chaveColetada = true;
            chaveMagica.SetActive(false);

            AtualizarIconeChave();
        }
    }

    public void ConsumirChave()
    {
        chaveColetada = false;
        AtualizarIconeChave();
    }

    void AtualizarIconeChave()
    {
        if (iconeChave != null)
        {
            iconeChave.gameObject.SetActive(chaveColetada);
        }
    }

    void AtualizarTexto()
    {
        if (textoPontuacao != null)
        {
            textoPontuacao.text = "Pontos: " + pontuacao;
        }
    }
}
