
using UnityEngine;

public class Projetil : MonoBehaviour
{
    [Header("Configuração")]
    public float velocidade = 10f;
    public float tempoVida = 3f;
    public int dano = 1;

    private float direcao = 1f;
    private bool projetilDoInimigo = false;

    public void DefinirDirecao(float novaDirecao)
    {
        direcao = novaDirecao;
    }

    public void DefinirComoProjetilDoInimigo()
    {
        projetilDoInimigo = true;
    }

    void Start()
    {
        Destroy(gameObject, tempoVida);
    }

    void Update()
    {
        transform.Translate(
            Vector3.right * direcao * velocidade * Time.deltaTime,
            Space.World
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (projetilDoInimigo)
        {
            // O projétil do inimigo só atinge a Botan.
            VidaBotan vidaBotan =
                other.GetComponentInParent<VidaBotan>();

            if (vidaBotan != null)
            {
                vidaBotan.ReceberDano(dano);
                Destroy(gameObject);
            }
        }
        else
        {
            // O projétil da Botan só atinge inimigos.
            if (other.CompareTag("Inimigo"))
            {
                VidaInimigo vidaInimigo =
                    other.GetComponentInParent<VidaInimigo>();

                if (vidaInimigo != null)
                {
                    vidaInimigo.ReceberDano(dano);
                    Destroy(gameObject);
                }
            }
        }
    }
}
