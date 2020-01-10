using System.Collections;
using System.Collections.Generic;
using com.rpdev.player.controller;
using com.rpdev.player.model;
using UnityEngine;
using Zenject;

namespace com.rpdev.player {

	public class PlayerContextInstaller : MonoInstaller {
		
		public override void InstallBindings() {
			
			Container.BindInterfacesTo<PlayerInputController>().AsSingle();
			Container.BindInterfacesTo<PlayerContextController>().AsSingle();
			Container.BindInterfacesTo<PlayerContextModel>().AsSingle();
			
		}
	}
}