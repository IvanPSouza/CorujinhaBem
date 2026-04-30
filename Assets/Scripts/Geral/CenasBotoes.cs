using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasBotoes : MonoBehaviour
{
    public void MudarCena(string Cena)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(Cena);
    }
}
