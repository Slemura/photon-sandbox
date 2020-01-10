using System;
using System.Linq;
using Photon.Pun;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace com.rpdev.character.model {

	public interface ICharacterModel {
		bool IsMasterCharacter { get; }
		float MoveSpeed { get; }
		float JumpForce { get; }
		string GetAnimationParamByID(CharacterModel.Settings.CharacterAnimationID animation);
	}
	
	public class CharacterModel : ICharacterModel {
		
	#region Injected
		[Inject]
		protected Settings character_settings;
		[Inject]
		protected PhotonView photon_view;
	#endregion

	#region Public
		public bool IsMasterCharacter => photon_view.IsMine;
		public float MoveSpeed => character_settings.move_speed;
		public float JumpForce => character_settings.jump_force;
	#endregion	
		
		public string GetAnimationParamByID(Settings.CharacterAnimationID animation) {
			return character_settings.animations.FirstOrDefault(anim => anim.anim_flag == animation).animation;
		}

	#region Settings
	    [Serializable]
	    public class Settings {
		    
		    public float move_speed;
		    public float jump_force;
		    public CharacterAnimationInfo[] animations;
		    
		    [Serializable]
		    public struct CharacterAnimationInfo {

			    public CharacterAnimationID anim_flag;
			    
			    [ValueDropdown("GetAnimationParams")]
			    public string animation;

		    #region Editor
		    #if UNITY_EDITOR
			    public ValueDropdownList<string> GetAnimationParams() {
				    UnityEditor.Animations.AnimatorController character_anim_controller = Resources.Load<UnityEditor.Animations.AnimatorController>("Animation/LegsAnimationController");
				    
				    ValueDropdownList<string> list = new ValueDropdownList<string>();

				    if (character_anim_controller == null ) return list;

				    character_anim_controller.parameters
				                             .Select(param => param.name)
				                             .ForEach(name => { list.Add(name, name); });

				    return list;
			    }
		    #endif
		    #endregion
		    }
		    
		    
		    public enum CharacterAnimationID {
			    WALK,
			    JUMP
		    }
	    }
    #endregion
	}
}