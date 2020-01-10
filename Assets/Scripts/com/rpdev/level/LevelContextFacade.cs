using UnityEngine;

namespace com.rpdev.level {

	public interface ILevelContextFacade {
		Transform MainDynamicContainer { get; }
	}
	
	public class LevelContextFacade : MonoBehaviour, ILevelContextFacade {
		
		[SerializeField]
		protected Transform dynamic_container;
		public Transform MainDynamicContainer => dynamic_container;
	}	

}