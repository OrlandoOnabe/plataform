
using UnityEngine;
using System.Collections;

public class BotanMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 3f;

    [Header("Disparo")]
    public GameObject prefabProjetil;
    public Transform firePoint;
    public float tempoEntreTiros = 0.5f;
    public float atrasoDisparo = 0.3f;

    private float proximoTiro = 0f;
    private bool disparoPendente = false;

    private Rigidbody2D rb;
    private Animator animator;
    private VidaBotan vidaBotan;

    private bool chão = true;
    private bool animacaoPuloTerminou = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        vidaBotan = GetComponent<VidaBotan>();

        if (animator != null)
        {
            animator.SetBool("chão", chão);
        }
    }

    void Update()
    {
        float movimento = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(
            movimento * velocidade,
            rb.linearVelocity.y
        );

        if (movimento != 0)
        {
            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x) * Mathf.Sign(movimento);
            transform.localScale = escala;
        }

        if (Input.GetKeyDown(KeyCode.Space) && chão)
        {
            chão = false;
            animacaoPuloTerminou = false;

            if (animator != null)
            {
                animator.ResetTrigger("pular");
                animator.SetTrigger("pular");
                animator.SetBool("chão", false);
            }

            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                forcaPulo
            );
        }

        if (Input.GetKeyDown(KeyCode.F) &&
            Time.time >= proximoTiro &&
            !disparoPendente)
        {
            if (animator != null)
            {
                animator.SetTrigger("atacar");
            }

            disparoPendente = true;
            proximoTiro = Time.time + tempoEntreTiros;

            StartCoroutine(DispararComAtraso());
        }

        if (animator != null)
        {
            animator.SetFloat("velocidade", Mathf.Abs(movimento));
            animator.SetBool("chão", chão);

            if (animacaoPuloTerminou && chão)
            {
                animator.SetBool("chão", true);
            }
        }
    }

    IEnumerator DispararComAtraso()
    {
        yield return new WaitForSeconds(atrasoDisparo);

        Atirar();

        disparoPendente = false;
    }

    public void Atirar()
    {
        if (prefabProjetil == null || firePoint == null)
        {
            return;
        }

        float direcao = transform.localScale.x >= 0 ? 1f : -1f;

        Vector3 posicao = firePoint.position;

        GameObject novoProjetil = Instantiate(
            prefabProjetil,
            posicao,
            Quaternion.identity
        );

        novoProjetil.SetActive(true);

        Projetil scriptProjetil =
            novoProjetil.GetComponent<Projetil>();

        if (scriptProjetil != null)
        {
            scriptProjetil.DefinirDirecao(direcao);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
        {
            if (vidaBotan != null)
            {
                vidaBotan.ReceberDano(1);
            }
        }

        foreach (ContactPoint2D contato in collision.contacts)
        {
            if (contato.normal.y > 0.5f)
            {
                chão = true;

                if (animator != null)
                {
                    animator.SetBool("chão", true);
                }

                break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chave"))
        {
            if (GerenciadorPontuacao.instancia != null)
            {
                GerenciadorPontuacao.instancia.ColetarChave();
            }
        }
    }

    public void FinalizarAnimacaoPulo()
    {
        animacaoPuloTerminou = true;
    }
}
