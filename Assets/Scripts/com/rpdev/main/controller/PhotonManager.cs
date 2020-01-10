using System;
using System.Collections.Generic;
using com.rpdev.ui;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace com.rpdev.main.controller {

	public interface IPhotonManager {
		
		void StartConnection();
	}

	public class PhotonManager : IPhotonManager, IInitializable, IDisposable, IConnectionCallbacks , IMatchmakingCallbacks , IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback {
		
	#region Injected
		[Inject]
		protected IMainCharacterSpawner character_spawner;
		[Inject]
		protected IMainController main_controller;
		[Inject]
		protected IMainScreenMediator screen_mediator;
		[Inject]
		protected Settings settings;
	#endregion	
		
		public void Initialize() {
			PhotonNetwork.AddCallbackTarget(this);
			PhotonNetwork.GameVersion = settings.version;
			PhotonNetwork.PrefabPool = character_spawner;
		}

		public void Dispose() {
			PhotonNetwork.RemoveCallbackTarget(this);
		}
		
		public void StartConnection() {
			screen_mediator.AppendLog("Start connection");
			PhotonNetwork.ConnectUsingSettings();
		}
		
		public void OnConnectedToMaster() {
			screen_mediator.AppendLog("On connect to master");
			PhotonNetwork.JoinLobby();
		}

		public void OnJoinedLobby() {
			screen_mediator.AppendLog("On join lobby");
			PhotonNetwork.JoinOrCreateRoom(settings.room_name, new RoomOptions {MaxPlayers = settings.max_players},
			                               TypedLobby.Default);
		}
		
		public void OnJoinedRoom() {
			screen_mediator.AppendLog("On joined room");
			screen_mediator.Hide();
			main_controller.LoadLevel();
		}
		
		public void OnJoinRoomFailed(short returnCode, string message) {
			screen_mediator.AppendLog("On Join Room Failed " + message);
		}
		
	#region UnusedPunImplements
		public void OnConnected() {
		}

		public void OnLeftLobby() {
		}

		public void OnDisconnected(DisconnectCause cause) {
		}

		public void OnRegionListReceived(RegionHandler regionHandler) {
		}

		public void OnCustomAuthenticationResponse(Dictionary<string, object> data) {
		}

		public void OnCustomAuthenticationFailed(string debugMessage) {
		}

		public void OnFriendListUpdate(List<FriendInfo> friendList) {
		}

		public void OnCreatedRoom() {
		}

		public void OnCreateRoomFailed(short returnCode, string message) {
		}

		public void OnJoinRandomFailed(short returnCode, string message) {
		}

		public void OnLeftRoom() {
		}

		public void OnPlayerEnteredRoom(Player newPlayer) {
		}

		public void OnPlayerLeftRoom(Player otherPlayer) {
		}

		public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged) {
		}

		public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
		}

		public void OnMasterClientSwitched(Player newMasterClient) {
		}

		public void OnRoomListUpdate(List<RoomInfo> roomList) {
		}

		public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics) {
		}

		public void OnWebRpcResponse(OperationResponse response) {
		}
	#endregion

	#region Settings
		[Serializable]
		public struct Settings {
			public string version;
			public string room_name;
			public byte max_players;
		}
	#endregion
	}
}