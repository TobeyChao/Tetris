using UnityEngine;
using UnityEngine.UI;

namespace Game_Assets.Scripts
{
    public class LevelManager : MonoBehaviour {

        public static int level;

        Text _levelText;

        // Use this for initialization
        void Awake () {
            _levelText = GetComponent<Text>();
        }

        void Start() {
            level = 1;
        }

        // Update is called once per frame
        void Update () {
            if (ScoreManager.score >= level * 1000) {
                level += 1;
            }
            _levelText.text = level.ToString();
        }
    }
}
