using System;
using com.rpdev.character.model;
using com.rpdev.player;
using UniRx;
using UnityEngine;
using Zenject;

namespace com.rpdev.character.controller {

	public class CharacterAnimationController : IInitializable, IDisposable {
		
	#region Protected	
		protected CompositeDisposable anim_stream;
	#endregion

	#region Injected
		[Inject]
		protected Animator animator_core;
		[Inject]
		protected IPlayerContextFacade player_context_facade;
		[Inject]
		protected ICharacterModel character_model;
		[Inject]
		protected ICharacterMoveController character_move_controller;
	#endregion
		
		public void Initialize() {
			
			anim_stream = new CompositeDisposable();

			player_context_facade.InputController
			                     .InputAxis
			                     .Where(_ => character_move_controller.CanJump)
			                     .Subscribe(axis => {
				                      animator_core
					                     .SetFloat(character_model.GetAnimationParamByID(CharacterModel.Settings.CharacterAnimationID.WALK),
					                               character_move_controller.CurrentSpeed);
			                      })
			                     .AddTo(anim_stream);

			player_context_facade.InputController.JumpInput
			                     .Where(jump => jump && character_model.IsMasterCharacter)
			                     .Subscribe(_ =>
				                                animator_core.SetTrigger(character_model
					                                                        .GetAnimationParamByID(CharacterModel.Settings.CharacterAnimationID.JUMP)))
			                     .AddTo(anim_stream);
		}

		public void Dispose() {
			anim_stream?.Dispose();
		}
	}
}