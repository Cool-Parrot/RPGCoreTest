using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartGame()
    {
        SceneManager.LoadScene("GameScene"); // 載入遊戲場景
    }

    public void OnLoadGame()
    {
        Debug.Log("打開讀取進度介面");
        // 這裡可以彈出一個 LoadGamePanel
    }

    public void OnSettings()
    {
        Debug.Log("打開設定介面");
        // 這裡可以彈出一個 SettingsPanel
    }

    public void OnQuit()
    {
        Debug.Log("退出遊戲");
        Application.Quit();
    }
}
