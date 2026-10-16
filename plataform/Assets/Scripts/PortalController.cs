
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PortalController : MonoBehaviour
{
    [Header("Portal")]
    public Animator animator;
    public string cenaDestino = "Fase2";

    [Header("Interação")]
    public TextMeshProUGUI textoInteracao;

    private bool jogadorPerto = false;
    private bool portalAtivado = false;
    private bool portalPronto = false;
    private bool trocandoFase = false;

    void Awake()
    {
        EsconderMensagem();
    }

    void Start()
    {
        EsconderMensagem();
    }

    void Update()
    {
        if (trocandoFase)
            return;

        if (portalAtivado && animator != null)
        {
            AnimatorStateInfo estado =
                animator.GetCurrentAnimatorStateInfo(0);

            if (estado.IsName("Portal_aceso"))
            {
                portalPronto = true;
            }
        }

        GerenciadorPontuacao gerenciador =
            GerenciadorPontuacao.instancia;

        bool temChave =
            gerenciador != null && gerenciador.chaveColetada;

        if (jogadorPerto && !portalAtivado && temChave)
        {
            MostrarMensagem("Pressione E para usar a chave");

            if (Input.GetKeyDown(KeyCode.E))
            {
                AtivarPortal();
            }
        }
        else if (jogadorPerto && portalAtivado && portalPronto)
        {
            MostrarMensagem("Entre no portal");
        }
        else
        {
            EsconderMensagem();
        }

        if (jogadorPerto && portalAtivado && portalPronto)
        {
            CarregarFase();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<BotanMovement>() == null)
            return;

        jogadorPerto = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponentInParent<BotanMovement>() == null)
            return;

        jogadorPerto = true;

        if (portalAtivado && portalPronto)
        {
            CarregarFase();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<BotanMovement>() == null)
            return;

        jogadorPerto = false;
        EsconderMensagem();
    }

    void AtivarPortal()
    {
        GerenciadorPontuacao gerenciador =
            GerenciadorPontuacao.instancia;

        if (gerenciador == null || !gerenciador.chaveColetada)
            return;

        portalAtivado = true;

        // Consome a chave e esconde o ícone da interface.
        gerenciador.ConsumirChave();

        EsconderMensagem();

        if (animator != null)
        {
            animator.SetTrigger("Ativar");
        }
        else
        {
            portalPronto = true;
        }
    }

    void CarregarFase()
    {
        if (trocandoFase || !portalPronto)
            return;

        trocandoFase = true;
        EsconderMensagem();

        SceneManager.LoadScene(cenaDestino);
    }

    void MostrarMensagem(string mensagem)
    {
        if (textoInteracao == null)
            return;

        textoInteracao.text = mensagem;

        if (!textoInteracao.gameObject.activeSelf)
        {
            textoInteracao.gameObject.SetActive(true);
        }
    }

    void EsconderMensagem()
    {
        if (textoInteracao != null)
        {
            textoInteracao.gameObject.SetActive(false);
        }
    }
}
