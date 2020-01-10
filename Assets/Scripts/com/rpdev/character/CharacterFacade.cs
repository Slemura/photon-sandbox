using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace com.rpdev.character {

	public interface ICharacterFacade {
		GameObject GameObject { get; }
		Vector3 Position { get; }
	}
	
	public class CharacterFacade : MonoBehaviour, ICharacterFacade {

	#region Public
	    public class Factory : PlaceholderFactory<string, Vector2, Transform, ICharacterFacade> {}

	    public GameObject GameObject => gameObject;
	    public Vector3 Position => transform.position;
    #endregion
	}
	
}