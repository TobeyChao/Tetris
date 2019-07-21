using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game_Assets.Scripts
{
    public class MainMenu : MonoBehaviour {

        // this is the button panels
        public GameObject[] buttons;
        public GameObject highScorePanel;
        public Text highscoreText;
        private int _buttonSelected;
        private int _numberOfButtons;

        void Awake() {
            _numberOfButtons = buttons.Length;
            _buttonSelected = 0;
            SelectNewGame();
            if (Highscore.highscore > 0) {
                highscoreText.text = Highscore.Get();
                highScorePanel.SetActive(true);
            }
        }

        public void NewGame() {
            SceneManager.LoadScene(1);
        }

        public void Exit() {
            Application.Quit();
        }

        void OpenSelected() {
            if (_buttonSelected == 0) {
                NewGame();
            } else if (_buttonSelected == 1) {
                Exit();
            }
        }

        public void SelectNewGame() {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
            _buttonSelected = 0;
        }

        public void SelectExitGame() {
            buttons[1].SetActive(true);
            buttons[0].SetActive(false);
            _buttonSelected = 1;
        }

        void ChangePanel(int direction) {
            buttons[_buttonSelected].SetActive(false);
            //Debug.LogFormat("Old selected: {0}", buttonSelected);
            _buttonSelected = Utils.Mod(_buttonSelected + direction,  _numberOfButtons);
            //Debug.LogFormat("New selected: {0}", buttonSelected);
            buttons[_buttonSelected].SetActive(true);
        }

        public void Update() {
            // arrow browsing menu
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                ChangePanel(-1); // up
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                ChangePanel(1); // down
            } else if (Input.GetKeyDown(KeyCode.Return)) {
                OpenSelected(); // open selected button
            } else if (Input.GetKeyDown(KeyCode.Escape)) {
                Application.Quit(); // quit
            }
        }

    }
}
