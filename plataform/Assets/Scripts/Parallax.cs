
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Configuração do Parallax")]
    public Transform cameraTransform;
    [Range(0f, 1f)]
    public float velocidadeParallax = 0.3f;

    private Vector3 posicaoAnteriorCamera;

    void Start()
    {

        if (cameraTransform == null)
        {
            Camera cameraPrincipal = Camera.main;

            if (cameraPrincipal != null)
            {
                cameraTransform = cameraPrincipal.transform;
            }
        }

 
        if (cameraTransform != null)
        {
            posicaoAnteriorCamera = cameraTransform.position;
        }
    }

    void Update()
    {

        if (cameraTransform == null)
        {
            return;
        }


        Vector3 deslocamentoCamera =
            cameraTransform.position - posicaoAnteriorCamera;


        transform.position += new Vector3(
            deslocamentoCamera.x * velocidadeParallax,
            deslocamentoCamera.y * velocidadeParallax,
            0f
        );


        posicaoAnteriorCamera = cameraTransform.position;
    }
}
