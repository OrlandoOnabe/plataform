
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public float velocidade = 2f;
    public float distanciaDeteccao = 6f;

    private Transform jogador;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject objetoJogador =
            GameObject.Find("botan_idle-sheet_0_0");

        if (objetoJogador != null)
        {
            jogador = objetoJogador.transform;
        }
    }

    void FixedUpdate()
    {
        if (jogador == null)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            jogador.position
        );

        if (distancia <= distanciaDeteccao)
        {
            float direcao = Mathf.Sign(
                jogador.position.x - transform.position.x
            );

            rb.linearVelocity = new Vector2(
                direcao * velocidade,
                rb.linearVelocity.y
            );

            Vector3 escala = transform.localScale;
            escala.x = Mathf.Abs(escala.x) * direcao;
            transform.localScale = escala;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }
}
