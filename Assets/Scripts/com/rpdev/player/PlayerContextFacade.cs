using com.rpdev.character;
using com.rpdev.player.controller;
using com.rpdev.player.model;
using UniRx;
using UnityEngine;
using Zenject;

namespace com.rpdev.player {

	public interface IPlayerContextFacade {
		
		IPlayerInputController InputController { get; }
		IReactiveProperty<ICharacterFacade> PlayerCharacter { get; }
		
		void SpawnPlayerCharacter();
	}
	
	public class PlayerContextFacade : MonoBehaviour, IPlayerContextFacade {
		
	#region Injected
	
		[Inject]
		protected IPlayerInputController player_input_controller;
		[Inject]
		protected IPlayerContextController player_context_controller;
		[Inject]
		protected IPlayerContextModel player_context_model;
		
	#endregion

	#region Public
		
		public IPlayerInputController InputController => player_input_controller;
		public IReactiveProperty<ICharacterFacade> PlayerCharacter => player_context_model.PlayerCharacter;
		
	#endregion	
		
		public void SpawnPlayerCharacter() {
			player_context_controller.SpawnPlayerCharacter();
		}
	}
}