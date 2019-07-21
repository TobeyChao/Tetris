using UnityEngine;

namespace Game_Assets.Scripts
{
    public class Utils : MonoBehaviour {
        public static int Mod (int n, int m){
            return ((n % m) + m) % m;
        }
    }
}
