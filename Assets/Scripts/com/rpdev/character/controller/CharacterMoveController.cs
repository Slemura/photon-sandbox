using System;
using com.rpdev.character.model;
using com.rpdev.player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace com.rpdev.character.controller {

	public interface ICharacterMoveController {
		float CurrentSpeed { get; }
		bool CanJump { get; }
	}

	public class CharacterMoveController : MonoBehaviour, ICharacterMoveController, IInitializable {
		
	#region Protected
		protected bool         can_jump = true;
		protected float        start_scale;
	#endregion

	#region Injected
		[Inject]
		protected ICharacterModel character_model;
		[Inject]
		protected Rigidbody2D  body;
		[Inject]
		protected IPlayerContextFacade player_context_facade;
	#endregion

	#region Public
		public float CurrentSpeed => body.velocity.magnitude;
		public bool CanJump => can_jump;
	#endregion
		
		public void Initialize() {
			
		    start_scale = transform.localScale.x;
		    
		    this.OnCollisionEnter2DAsObservable()
		        .Where(collision => collision.transform.CompareTag("Floor"))
		        .Subscribe(_ => can_jump = true)
		        .AddTo(this);

		    this.player_context_facade
		        .InputController
		        .InputAxis
		        .Where(axis => axis != Vector2.zero && character_model.IsMasterCharacter)
		        .Subscribe(input_axis => {
			         
			         if (input_axis.x > 0) {
				         transform.localScale = new Vector3(start_scale, start_scale, 1);
			         } else if (input_axis.x < 0) {
				         transform.localScale = new Vector3(-start_scale, start_scale, 1);
			         }

			         body.velocity = Math.Abs(input_axis.x) > 0 ? 
				                          new Vector2(input_axis.x * character_model.MoveSpeed * Time.fixedDeltaTime, body.velocity.y) : 
				                          new Vector2(0, body.velocity.y);

		         })
		        .AddTo(this);

		    this.player_context_facade
		        .InputController
		        .JumpInput
		        .Where(jump => jump && can_jump && character_model.IsMasterCharacter)
		        .Subscribe(_ => {
			         body.AddForce(new Vector2(0, character_model.JumpForce));
			         can_jump = false;
		         })
		        .AddTo(this);
	    }
	}
}