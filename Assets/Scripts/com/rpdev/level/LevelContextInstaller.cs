using System.Collections;
using System.Collections.Generic;
using com.rpdev.level.controller;
using UnityEngine;
using Zenject;

namespace com.rpdev.level {

	public class LevelContextInstaller : MonoInstaller {
		
		public override void InstallBindings() {
			
			Container.BindInterfacesTo<LevelContextFacade>().FromInstance(GetComponent<LevelContextFacade>()).AsSingle();
			Container.BindInterfacesTo<LevelContextController>().AsSingle().NonLazy();
		}
	}
	
}