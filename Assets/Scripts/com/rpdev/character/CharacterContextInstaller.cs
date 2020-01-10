using com.rpdev.character.controller;
using com.rpdev.character.model;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace com.rpdev.character {

	public class CharacterContextInstaller : MonoInstaller {
		
		public override void InstallBindings() {
			
			Container.BindInterfacesTo<CharacterFacade>().FromInstance(GetComponent<CharacterFacade>());
			Container.BindInterfacesTo<CharacterModel>().AsSingle();
			Container.BindInterfacesTo<CharacterMoveController>().FromNewComponentOn(gameObject).AsSingle();
			Container.BindInterfacesTo<CharacterAnimationController>().AsSingle().NonLazy();
			
			Container.Bind<PhotonView>().FromInstance(GetComponent<PhotonView>()).AsSingle();
			Container.Bind<Rigidbody2D>().FromInstance(GetComponent<Rigidbody2D>());
		}
	}
}