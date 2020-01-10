using com.rpdev.main.controller;
using Zenject;

namespace com.rpdev.level.controller {

	public class LevelContextController : IInitializable {
		
	#region Injected
		[Inject]
		protected ILevelContextFacade level_context_facade;
		[Inject]
		protected IMainController main_controller;
	#endregion	
		
		public void Initialize() {
			main_controller.StartSpawnCharacters(level_context_facade.MainDynamicContainer);
		}
	}
}