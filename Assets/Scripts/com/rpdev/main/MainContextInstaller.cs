using com.rpdev.character;
using com.rpdev.main.controller;
using com.rpdev.player;
using com.rpdev.ui;
using UnityEngine;
using Zenject;

namespace com.rpdev.main {

	public class MainContextInstaller : MonoInstaller {

		public override void InstallBindings() {
			
			Container.BindFactory<string, Vector2, Transform, ICharacterFacade, CharacterFacade.Factory>()
			         .FromFactory<CharacterCustomFactory>()
			         .WhenInjectedInto<MainCharacterSpawner>();
			
			Container.BindInterfacesTo<MainCharacterSpawner>().AsSingle();
			Container.BindInterfacesTo<MainController>().AsSingle().NonLazy();
			
			Container.BindInterfacesTo<PlayerContextFacade>()
			         .FromComponentInNewPrefabResource("Prefab/Context/PlayerContext")
			         .UnderTransform(transform)
			         .AsSingle()
			         .NonLazy();

			Container.BindInterfacesTo<MainScreenMediator>()
			         .FromComponentInNewPrefabResource("Prefab/Context/UICanvas").AsSingle()
			         .NonLazy();
			
			Container.BindInterfacesTo<PhotonManager>().AsSingle();
		}
	}
}