using UnityEngine;

namespace Game_Assets.Scripts
{
	public class Spawner : MonoBehaviour {

		public GameObject[] groups;
		public int nextId;

		// Use this for initialization
		void Start () {
			nextId = Random.Range(0, groups.Length);
			SpawnNext ();
		}

		public GameObject CreateGroup(Vector3 v) {
			GameObject group = Instantiate(groups[nextId], v, Quaternion.identity);
			//group.transform.SetParent(GameObject.FindGameObjectWithTag("Board").transform);
			//group.transform.position *= canvas.scaleFactor; bug bug bug everywhere
			// solved in another way: just adjust the UI HUD to scale and keep this shit constant
			return group;
		}

		// spawnNext group block
		public void SpawnNext() {
			// Spawn Group at current Position
			CreateGroup(transform.position);
			nextId = Random.Range(0, groups.Length);
		}
	}
}