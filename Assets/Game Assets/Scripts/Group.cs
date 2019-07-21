using UnityEngine;


namespace Game_Assets.Scripts
{
    public class Group : MonoBehaviour {

        // time of the last fall, used to auto fall after 
        // time parametrized by `level`
        private float _lastFall;

        // last key pressed time, to handle long press behavior
        private float _lastKeyDown;
        private float _timeKeyPressed;

        bool IsValidGridPos() {
            //对group的每一个子物体
            foreach (Transform child in transform) {
                Vector2 v = Grid.RoundVector2(child.position);

                // not inside Border?
                if(!Grid.InsideBorder(v)) {
                    return false;
                }

                // Block in grid cell (and not par of same group)?
                if (Grid.grid[(int)(v.x), (int)(v.y)] != null &&
                    Grid.grid[(int)(v.x), (int)(v.y)].parent != transform) {
                    return false;
                }
            }

            return true;
        }

        // update the grid
        void UpdateGrid() {
            // Remove old children from grid
            for (int y = 0; y < Grid.h; ++y) {
                for (int x = 0; x < Grid.w; ++x) {
                    if (Grid.grid[x,y] != null && Grid.grid[x,y].parent == transform) {
                        Grid.grid[x,y] = null;
                    }
                }
            }

            InsertOnGrid();
        }

        void InsertOnGrid() {
            // add new children to grid
            foreach (Transform child in transform) {
                Vector2 v = Grid.RoundVector2(child.position);
                Grid.grid[(int)v.x,(int)v.y] = child;
            }
        }

        void GameOver() {
            Debug.Log("GAME OVER!");
            while (!IsValidGridPos()) {
                //Debug.LogFormat("Updating last group...: {0}", transform.position);
                transform.position  += new Vector3(0, 1, 0);
            } 
            UpdateGrid(); // to not overleap invalid groups
            enabled = false; // disable script when dies
            UIController.GameOver(); // active Game Over panel
            Highscore.Set(ScoreManager.score); // set highscore
            //Music.stopMusic(); // stop Music
        }

        // Use this for initialization
        void Start () {
            _lastFall = Time.time;
            _lastKeyDown = Time.time;
            _timeKeyPressed = Time.time;
            if (IsValidGridPos()) {
                InsertOnGrid();
            } else { 
                Debug.Log("KILLED ON START");
                GameOver();
            }

        }

        void TryChangePos(Vector3 v) {
            // modify position 
            // FIXME: maybe this is idiot? I can create a copy before and only assign to the transform if is valid
            transform.position += v;

            // See if valid
            if (IsValidGridPos()) {
                UpdateGrid();
            } else {
                transform.position -= v;
            }
        }

        void FallGroup() {
            // modify
            transform.position += new Vector3(0, -1, 0);

            if (IsValidGridPos()){
                // It's valid. Update grid... again
                UpdateGrid();
            } else {
                // it's not valid. revert
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.DeleteFullRows();


                FindObjectOfType<Spawner>().SpawnNext();


                // Disable script
                enabled = false;
            }

            _lastFall = Time.time;

        }

        // getKey if is pressed now on longer pressed by 0.5 seconds | if that true apply the key each 0.05f while is pressed
        bool GetKey(KeyCode key) {
            bool keyDown = Input.GetKeyDown(key);
            bool pressed = Input.GetKey(key) && Time.time - _lastKeyDown > 0.5f && Time.time - _timeKeyPressed > 0.05f;

            if (keyDown) {
                _lastKeyDown = Time.time;
            }
            if (pressed) {
                _timeKeyPressed = Time.time;
            }
 
            return keyDown || pressed;
        }


        // Update is called once per frame
        void Update () {
            if (UIController.isPaused) {
                return; // don't do nothing
            }
            if (GetKey(KeyCode.LeftArrow)) {
                TryChangePos(new Vector3(-1, 0, 0));
            } else if (GetKey(KeyCode.RightArrow)) {  // Move right
                TryChangePos(new Vector3(1, 0, 0));
            } else if (GetKey(KeyCode.UpArrow) && !gameObject.CompareTag("Cube")) { // Rotate
                transform.Rotate(0, 0, -90);

                // see if valid
                if (IsValidGridPos()) {
                    // it's valid. Update grid
                    UpdateGrid();
                } else {
                    // it's not valid. revert
                    transform.Rotate(0, 0, 90);
                }
            } else if (GetKey(KeyCode.DownArrow) || (Time.time - _lastFall) >= 1.0 / Mathf.Sqrt(LevelManager.level)) {
                FallGroup();
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                while (enabled) { // fall until the bottom 
                    FallGroup();
                }
            }

        }
    }
}
