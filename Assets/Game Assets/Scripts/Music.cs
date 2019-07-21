using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Assets.Scripts
{
    public class Music : MonoBehaviour {
        [FormerlySerializedAs("BGM")] public Object[] bgm;
        public AudioSource audioSource;
        public static AudioSource source;
        private bool _tryingChange;
        public static int nextMusic;

        void Awake() {
            if (source == null) {
                source = audioSource;
                DontDestroyOnLoad(this); // to no restart music on a new game
                RandomInitialization();
                source.Play();
            }
        }

        // get the first music by random number
        void RandomInitialization() {
            nextMusic = Random.Range(0, bgm.Length);
            source.clip = bgm[nextMusic] as AudioClip;
        }

        // select next music and increment nextMusic by circular reference
        void SelectNextMusic(){
            source.clip = bgm[nextMusic] as AudioClip;
            NextCircularPlaylist();
        }

        // Create a circular reference: 0 1 2 0 1 2 0 1 2 ...
        void NextCircularPlaylist() {
            nextMusic = Utils.Mod(nextMusic + 1, bgm.Length);
        }

        // Select the next music and play it
        void PlayNextMusic() {
            SelectNextMusic();
            source.Play();
        }
   
        // this avoid the behavior of start a new music
        // when the unity stop by desfocusing
        // So we wait for 1 second before change for new music
        // after isPLaying is false
        IEnumerator TryChange() {
            _tryingChange = true;
            yield return new WaitForSeconds(1);
            if (!source.isPlaying) {
                PlayNextMusic();
            }
            _tryingChange = false;
        }
   
        // Update is called once per frame
        void Update () {
            if (!_tryingChange && !source.isPlaying) {
                StartCoroutine(TryChange());
            }
        }
    }
}
