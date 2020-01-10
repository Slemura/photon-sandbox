using System;
using com.rpdev.character;
using UniRx;
	
namespace com.rpdev.player.model {

	public interface IPlayerContextModel {
		IReactiveProperty<ICharacterFacade> PlayerCharacter { get; }
		void AddPlayerCharacter(ICharacterFacade character);
	}
	
	public class PlayerContextModel : IPlayerContextModel {

		protected readonly IReactiveProperty<ICharacterFacade> player_character = new ReactiveProperty<ICharacterFacade>(null);
		public IReactiveProperty<ICharacterFacade> PlayerCharacter => player_character;
		
		public void AddPlayerCharacter(ICharacterFacade character) {
			player_character.Value = character;
		}
	}
	
}