using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Assets.Scripts
{
    public class UIController : MonoBehaviour {
        // gameOverPanel UI for game text
        public GameObject gameOverPanel;
        public GameObject pausePanel;
        public static GameObject gameOverPanelStatic;
        public static float gameOverTime;
        public static bool isPaused;


        void Awake() {
            if (gameOverPanelStatic == null) {
                gameOverPanelStatic = gameOverPanel;
            }
        }
	
        // restart the game
        void restart() {
            Debug.Log("RESTART GAME!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // terminate the game
        public static void GameOver() {
            gameOverPanelStatic.SetActive(true);
            gameOverTime = Time.time;
        }

        void gotoMainMenu() {
            SceneManager.LoadScene("MainMenu");
        }

        void TogglePause() {
            Time.timeScale = Time.timeScale > 0 ? 0f : 1f;
            pausePanel.SetActive(!pausePanel.activeSelf);
            isPaused = !isPaused;
        }

        // Update is called once per frame
        void Update () {
            // NOTE: this doesn't belong to here... this should not here... ARGH
            // exit the game if presed ESC
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit();
            } else if (Input.GetKeyDown(KeyCode.P)) {
                TogglePause();
            }
            else if (Input.GetKeyDown(KeyCode.R)) {
                restart();  
            } else if (gameOverPanel.activeSelf && Input.anyKeyDown && Time.time - gameOverTime >= 0.5f) {
                //gameOverPanel.SetActive(false);
                gotoMainMenu();
            } 
        }
    }
}
