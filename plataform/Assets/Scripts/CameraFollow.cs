
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform personagem;
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public float suavidade = 5f;

    void Start()
    {
        // Inicializações necessárias ao começar o jogo.
    }

    void Update()
    {
        // Não precisamos atualizar a câmera aqui.
    }

    void LateUpdate()
    {
        if (personagem == null)
            return;

        Vector3 destino = personagem.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            destino,
            suavidade * Time.deltaTime
        );
    }
}
