using System;
using com.rpdev.character;
using com.rpdev.player;
using Sirenix.OdinInspector;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Zenject;

namespace com.rpdev.camera {

	public class CameraController : MonoBehaviour, IInitializable {
		
	#region Protected
		[SerializeField]
		[HideLabel]
		protected Rect camera_level_bounds;
		[Inject]
		protected IPlayerContextFacade player_context_facade;
		[Inject]
		protected Settings settings;
		
		protected Vector3 velocity;
		protected Camera camera_core;
	#endregion
		
		[Inject]
		public void Initialize() {
			camera_core = GetComponent<Camera>();
			
			if (player_context_facade.PlayerCharacter.Value == null) {
				player_context_facade.PlayerCharacter
				                     .Where(character => character != null)
				                     .Subscribe(InitCameraFollow);				
			} else {
				InitCameraFollow(player_context_facade.PlayerCharacter.Value);
			}
		}

		protected void InitCameraFollow(ICharacterFacade character_facade) {
			
			this.UpdateAsObservable()
			    .Subscribe(_ => {
				     
				     Vector3 point       = camera_core.WorldToViewportPoint(character_facade.Position);
				     Vector3 delta       = character_facade.Position - camera_core.ViewportToWorldPoint(new Vector3(settings.character_offset.x, settings.character_offset.y, point.z));
				     Vector3 destination = transform.position + delta;
				     
				     if (destination.x < camera_level_bounds.x) {
					     destination.x = camera_level_bounds.x;
				     } else if (destination.x > camera_level_bounds.width) {
					     destination.x = camera_level_bounds.width;
				     }

				     if (destination.y < camera_level_bounds.y) {
					     destination.y = camera_level_bounds.y;
				     }
				     
				     transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, settings.follow_damping);
			     })
			    .AddTo(this);
		}

	#region Settings
		[Serializable]
		public class Settings {
			public float follow_damping;
			public Vector2 character_offset; 
		}
	#endregion
	}
	
}