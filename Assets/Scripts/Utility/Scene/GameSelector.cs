using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSelector : MonoBehaviour
{
    public string Game_SceneName;

    public void SelectLevel() {
        SceneManager.LoadScene(Game_SceneName);
    }
}
