using UnityEngine;
	
namespace com.rpdev.utils {

	public class ParallaxEffect : MonoBehaviour {
		
	#region Protected
		
		protected float start_pos;
		[SerializeField]
		protected GameObject level_camera;
		[SerializeField]
		protected float effect_value;
		
	#endregion
		
		protected void Start() {
			start_pos = transform.position.x;
		}

		protected void FixedUpdate() {
			float dist = level_camera.transform.position.x * effect_value;
			transform.position = new Vector3(start_pos + dist, transform.position.y, transform.position.z);
		}
	}
	
}