using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasBotton : MonoBehaviour
{
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadVKontakteVK(){
        Application.OpenURL("https://vk.com/esus_e");
    }
}