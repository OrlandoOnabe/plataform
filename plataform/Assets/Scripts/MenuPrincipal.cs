
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Fase1");
    }
}
