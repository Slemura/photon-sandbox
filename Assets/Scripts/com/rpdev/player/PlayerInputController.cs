using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace com.rpdev.player {

	public interface IPlayerInputController {
		IObservable<Vector2> InputAxis { get; }
		IObservable<bool> JumpInput { get; }
	}
	
	public class PlayerInputController : IPlayerInputController, IInitializable, IDisposable {
		
	#region Protected
		protected readonly ReactiveProperty<bool> jump_input = new ReactiveProperty<bool>(false);
		protected readonly Subject<Vector2> input_axis = new Subject<Vector2>();
		protected IDisposable input_stream;
	#endregion

	#region Public
		public IObservable<Vector2> InputAxis => input_axis;
		public IObservable<bool> JumpInput => jump_input;
	#endregion
		
		public void Initialize() {
			input_stream = Observable.EveryUpdate()
			                         .Subscribe(_ => {
				                          jump_input.Value = Input.GetKeyDown(KeyCode.Space);
				                          input_axis.OnNext(new Vector2(Input.GetAxis("Horizontal"),
				                                                        Input.GetAxis("Vertical")));

				                          
				                          if (Input.GetKeyUp(KeyCode.Escape)) {
					                          Application.Quit();
				                          }
			                          });
		}

		public void Dispose() {
			input_stream?.Dispose();
		}
	}
	
}