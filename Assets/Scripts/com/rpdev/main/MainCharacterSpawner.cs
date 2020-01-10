using com.rpdev.character;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace com.rpdev.main {

	public interface IMainCharacterSpawner : IPunPrefabPool {
		void InitDynamicContainer(Transform container);
		ICharacterFacade SpawnCharacter();
	}
	
	public class MainCharacterSpawner : IMainCharacterSpawner {
		
	#region Protected
		protected Random system_random ;
		protected Transform view_container;
	#endregion

	#region Injected
		[Inject]
		protected CharacterFacade.Factory character_factory;
	#endregion	
		
		public void InitDynamicContainer(Transform container) {
			view_container = container;
		}

		public ICharacterFacade SpawnCharacter() {
			
			system_random  = new Random();
			int random_pos = system_random.Next(0, 3);
			return PhotonNetwork.Instantiate("Prefab/Character/Knight_" + random_pos, Vector3.zero, Quaternion.identity).GetComponent<ICharacterFacade>();
		}

		public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation) {
			GameObject character = character_factory.Create(prefabId, Vector2.zero, view_container).GameObject;
			character.SetActive(false);
			return character;
		}

		public void Destroy(GameObject gameObject) {
			GameObject.Destroy(gameObject);
		}
	}
}