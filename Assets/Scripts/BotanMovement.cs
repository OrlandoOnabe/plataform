
using UnityEngine;

public class BotanMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float forcaPulo = 3f;

    public GameObject prefabProjetil;
    public Transform firePoint;
    public float tempoEntreTiros = 0.3f;

    private float proximoTiro = 0f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool chão = true;
    private bool animacaoPuloTerminou = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

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

        // Pulo
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

        // Inicia o ataque com F
        if (Input.GetKeyDown(KeyCode.F) && Time.time >= proximoTiro)
        {
            if (animator != null)
            {
                animator.SetTrigger("atacar");
            }

            proximoTiro = Time.time + tempoEntreTiros;
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

    // Chamado pelo Animation Event de Botan_fire
    public void Atirar()
    {
        if (prefabProjetil == null || firePoint == null)
        {
            Debug.LogWarning(
                "Configure o Prefab Projetil e o Fire Point no Inspector!"
            );
            return;
        }

        float direcao = Mathf.Sign(transform.localScale.x);

        GameObject novoProjetil = Instantiate(
            prefabProjetil,
            firePoint.position,
            Quaternion.identity
        );

        Projetil scriptProjetil =
            novoProjetil.GetComponent<Projetil>();

        if (scriptProjetil != null)
        {
            scriptProjetil.DefinirDirecao(direcao);
        }
        else
        {
            Debug.LogWarning(
                "O prefab precisa ter o componente Projetil."
            );
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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

    public void FinalizarAnimacaoPulo()
    {
        animacaoPuloTerminou = true;
    }
}
