using System.Collections;
using System.Collections.Generic;
using com.rpdev.character;
using com.rpdev.main;
using com.rpdev.player.model;
using UnityEngine;
using Zenject;

namespace com.rpdev.player.controller {

	public interface IPlayerContextController {
		void SpawnPlayerCharacter();
	}
	
	public class PlayerContextController : IPlayerContextController {
		
	#region Injected
	
		[Inject]
		protected IMainCharacterSpawner character_spawner;
		[Inject]
		protected IPlayerContextModel player_context_model;
		
	#endregion	
		
		public void SpawnPlayerCharacter() {
			player_context_model.AddPlayerCharacter(character_spawner.SpawnCharacter());
		}
	}
}