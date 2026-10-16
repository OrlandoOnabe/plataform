
using UnityEngine;

public class InimigoAtirador : MonoBehaviour
{
    [Header("Detecção")]
    public float distanciaDeteccao = 6f;

    [Header("Disparo")]
    public GameObject prefabProjetil;
    public Transform pontoDisparo;
    public float intervaloTiros = 2f;
    public float velocidadeProjetil = 8f;

    private Transform jogador;
    private float proximoTiro = 0f;

    void Start()
    {
        GameObject objetoJogador =
            GameObject.Find("botan_idle-sheet_0_0");

        if (objetoJogador != null)
        {
            jogador = objetoJogador.transform;
        }
    }

    void Update()
    {
        if (jogador == null)
        {
            GameObject objetoJogador =
                GameObject.Find("botan_idle-sheet_0_0");

            if (objetoJogador != null)
            {
                jogador = objetoJogador.transform;
            }

            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            jogador.position
        );

        if (distancia > distanciaDeteccao)
            return;

        float direcao = Mathf.Sign(
            jogador.position.x - transform.position.x
        );

        if (direcao == 0)
            direcao = 1f;

        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x) * direcao;
        transform.localScale = escala;

        if (Time.time >= proximoTiro)
        {
            Atirar(direcao);
            proximoTiro = Time.time + intervaloTiros;
        }
    }

    void Atirar(float direcao)
    {
        if (prefabProjetil == null || pontoDisparo == null)
            return;

        GameObject novoProjetil = Instantiate(
            prefabProjetil,
            pontoDisparo.position,
            Quaternion.identity
        );

        Projetil scriptProjetil =
            novoProjetil.GetComponent<Projetil>();

        if (scriptProjetil != null)
        {
            scriptProjetil.DefinirDirecao(direcao);
            scriptProjetil.DefinirComoProjetilDoInimigo();
        }
        else
        {
            Debug.LogError(
                "O prefab do tiro precisa ter o componente Projetil!"
            );

            Destroy(novoProjetil);
            return;
        }

        // Ajusta a velocidade do projétil criado.
        scriptProjetil.velocidade = velocidadeProjetil;
    }
}
