using System;
using System.IO;
using com.rpdev.player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace com.rpdev.main.controller {

	public interface IMainController {
		void LoadLevel();
		void StartSpawnCharacters(Transform container);
	}
	
	public class MainController : IMainController {
		
	#region Injected
		[Inject]
		protected IPlayerContextFacade player_context_facade;
		[Inject]
		protected ZenjectSceneLoader scene_loader;
		[Inject]
		protected Settings settings;
		[Inject]
		protected IMainCharacterSpawner spawner;
	#endregion	
		
		public void LoadLevel() {
			scene_loader.LoadSceneAsync(settings.game_levels[0].scene, LoadSceneMode.Additive, (container) => {}, LoadSceneRelationship.Child);
		}

		public void StartSpawnCharacters(Transform container) {
			spawner.InitDynamicContainer(container);
			player_context_facade.SpawnPlayerCharacter();
		}

	#region Settings
		[Serializable]
		public struct Settings {

			public SceneInfo[] game_levels;
			
			[Serializable]
			public struct SceneInfo {
				[ValueDropdown("GetScenesList")]
				public string scene;

			#region UnityEditor
			#if UNITY_EDITOR
				private ValueDropdownList<string> GetScenesList() {

					ValueDropdownList<string> scenes = new ValueDropdownList<string>();

					for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
						string scene_path = SceneUtility.GetScenePathByBuildIndex(i);
						
						scenes.Add(Path.GetFileNameWithoutExtension(scene_path));
					}

					return scenes;
				}
			#endif
			#endregion
			}
		
		}
	#endregion
	}
}