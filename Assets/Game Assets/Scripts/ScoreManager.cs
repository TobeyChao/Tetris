using UnityEngine;
using UnityEngine.UI;

namespace Game_Assets.Scripts
{
	public class ScoreManager : MonoBehaviour {

		public static int score;

		Text _scoreText;

		// Use this when game is started initialization
		void Awake () {
			_scoreText = GetComponent<Text>();
		}

		void Start() {
			score = 0;
		}
	
		// Update is called once per frame
		void Update () {
			_scoreText.text = System.String.Format("{0:D8}", score);
		}
	}
}
