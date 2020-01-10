using System.Collections;
using System.Collections.Generic;
using com.rpdev.main.controller;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace com.rpdev.ui {

	public interface IMainScreenMediator {
		void AppendLog(string text);
		void Hide();
	}
	
	public class MainScreenMediator : MonoBehaviour, IMainScreenMediator {
		
	#region Protected
		
		[SerializeField]
		protected Text log_text;
		[Inject]
		protected IPhotonManager photon_manager;
		
	#endregion	
	
		public void StartConnection() {
			photon_manager.StartConnection();
		}

		public void AppendLog(string text) {
			log_text.text = log_text.text + text + "\n";
		}

		public void Hide() {
			gameObject.SetActive(false);
		}
	}
}