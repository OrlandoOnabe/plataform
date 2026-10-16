
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuVitoria : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Menu");
    }
}
