using com.rpdev.camera;
using com.rpdev.character.model;
using com.rpdev.main.controller;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace com.rpdev.main {

	[CreateAssetMenu(fileName = "MainSettings", menuName = "Main settings")]
	public class MainSettings : ScriptableObjectInstaller {
		
	#region Public
		[HideLabel]
		[BoxGroup("Character")]
		public CharacterModel.Settings character_settings;
		
		[HideLabel]
		[BoxGroup("Camera")]
		public CameraController.Settings camera_settings;
		
		[HideLabel]
		[BoxGroup("Levels")]
		public MainController.Settings level_settings;

		[HideLabel]
		[BoxGroup("Photon settings")]
		public PhotonManager.Settings photon_settings;
	#endregion
		
		public override void InstallBindings() {

			Container.Bind<CharacterModel.Settings>().FromInstance(character_settings);
			Container.Bind<CameraController.Settings>().FromInstance(camera_settings);
			Container.Bind<MainController.Settings>().FromInstance(level_settings);
			Container.Bind<PhotonManager.Settings>().FromInstance(photon_settings);
		}
	}
}