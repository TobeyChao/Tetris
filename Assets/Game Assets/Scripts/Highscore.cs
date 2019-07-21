using UnityEngine;

namespace Game_Assets.Scripts
{
    public class Highscore : MonoBehaviour {

        public static int highscore;

        public static void Set(int score) {
            if (score > highscore) {
                highscore = score;
            }
        
        }

        public static string Get() { 
            return System.String.Format("{0:D8}", highscore);
        }

    }
}
