using UnityEngine;

namespace Game_Assets.Scripts
{
    public class NextSpawner : MonoBehaviour {

        private Spawner _spawner;
        private GameObject _currentGroupObject;
        private int _currentGroupId; 

        // Use this for initialization
        void Awake () {
            _spawner = FindObjectOfType<Spawner>();
        }
    
        void CreateStoppedGroup () {
            _currentGroupObject = _spawner.CreateGroup(transform.position);
            _currentGroupId = _spawner.nextId;
            var group = (Group) _currentGroupObject.GetComponent(typeof(Group));
            group.enabled = false;
        }
    
        void DeleteCurrentGroup() {
            Destroy(_currentGroupObject);
        }

        void Start() {
            CreateStoppedGroup();
        }
	
        // Update is called once per frame
        void Update () {
            if (_currentGroupId != _spawner.nextId) {
                DeleteCurrentGroup();
                CreateStoppedGroup();
            }
        }
    }
}
