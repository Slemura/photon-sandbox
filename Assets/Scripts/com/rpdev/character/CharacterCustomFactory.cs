using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Zenject;
using Random = System.Random;

namespace com.rpdev.character {

	public class CharacterCustomFactory : IFactory<string, Vector2, Transform, ICharacterFacade> {

		[Inject]
		protected DiContainer container;
		
		public ICharacterFacade Create(string prefab_path, Vector2 position, Transform parent) {
			return container.InstantiatePrefabResourceForComponent<ICharacterFacade>(prefab_path, position, Quaternion.identity, parent);
		}
	}
}