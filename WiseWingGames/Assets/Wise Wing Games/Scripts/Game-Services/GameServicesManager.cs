using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


namespace WiseWingGames
{
	public class GameServicesManager : MonoBehaviour
	{

		public static GameServicesManager instance { get; private set; }


		public delegate void AuthenticaitonDelegate ();

		public static event AuthenticaitonDelegate onAuthenticated;



		GameServicesSettings gameServicesSettings;

		void Awake ()
		{
			if (instance != null)
				Destroy (this);
			else
				instance = this;
		}

		void Start ()
		{
			gameServicesSettings = WiseWingGamesSetup.instance.GetGameServicesSettings;

			if (gameServicesSettings.SignInOnStart)
				SignIn ();

		}

		public void SignIn ()
		{
			Social.localUser.Authenticate ((success) => {
				if (success && onAuthenticated != null)
					onAuthenticated ();
			});
		}

		public void SignOut(){
			PlayGamesPlatform.Instance.SignOut();
		}
		public void ShowLeaderboardUI(){
			Social.ShowLeaderboardUI ();
		}

		public void ShowLeaderboardUI(string leaderboardId){
			PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardId);

		}

		public void ShowLeaderboardUI(string leaderboardId, LeaderboardStart lbStart, int rowCount, LeaderboardCollection lbCollection, LeaderboardTimeSpan lbTimeSpan){
			PlayGamesPlatform.Instance.LoadScores(
				leaderboardId,
				lbStart,
				rowCount,
				lbCollection,
				lbTimeSpan,
				(data) =>
				{
					Debug.Log( "Leaderboard data valid: " + data.Valid);
					Debug.Log(" approx:" +data.ApproximateCount + " have " + data.Scores.Length);
				});
			
		}

//		internal void LoadUsersAndDisplay(ILeaderboard lb)
//		{
//			// get the user ids
//			List<string> userIds = new List<string>();
//
//			foreach(IScore score in lb.scores) {
//				userIds.Add(score.userID);
//			}
//			// load the profiles and display (or in this case, log)
//			Social.LoadUsers(userIds.ToArray(), (users) =>
//				{
//					string status = "Leaderboard loading: " + lb.title + " count = " +
//						lb.scores.Length;
//					foreach(IScore score in lb.scores) {
//						IUserProfile user = FindUser(users, score.userID);
//						status += "\n" + score.formattedValue + " by " +
//							(string)(
//								(user != null) ? user.userName : "**unk_" + score.userID + "**");
//					}
//					Debug.log(status);
//				});
//		}

		// Got to look into this

//		public void ShowLeaderboardUIwithOptions(){
//			ILeaderboard lb = PlayGamesPlatform.Instance.CreateLeaderboard();
//			lb.id = "MY_LEADERBOARD_ID";
//			lb.LoadScores(ok =>
//				{
//					if (ok) {
////						LoadUsersAndDisplay(lb);
//					}
//					else {
//						Debug.Log("Error retrieving leaderboardi");
//					}
//				});
//		}

		public void PostScoreOnLeaderBoard(long score, string leaderboardId){
			Social.ReportScore(score, leaderboardId, (bool success) => {
				// handle success or failure
			});
		}

//		public void PostScoreOnLeaderBoard(long score, string leaderboardId, string tag){
//			Social.ReportScore(score, leaderboardId, tag, (bool success) => {
//				// handle success or failure
//			});
//		}


		public void ShowAchievementUI(){
			Social.ShowAchievementsUI ();
		}

		public void UnlockAchievement(string achievementID){
			Social.ReportProgress(achievementID, 100.0f, (bool success) => {
				// handle success or failure
			});

		}

		public void IncrementAchievement(string achievementID, int steps){
			PlayGamesPlatform.Instance.IncrementAchievement(
				achievementID, steps, (bool success) => {
					// handle success or failure
				});
		}

		public void IncrementEvent(string eventID , uint increment){
			PlayGamesPlatform.Instance.Events.IncrementEvent(eventID, increment);

		}


		public void GetUserEmail(){
			Debug.Log("Local user's email is " +
				((PlayGamesLocalUser)Social.localUser).Email);
		}

		public void LoadFriends(){
			Social.localUser.LoadFriends((ok) =>  {
				Debug.Log("Friends loaded OK: " + ok);
				foreach(IUserProfile p in Social.localUser.friends) {
					Debug.Log(p.userName + " is a friend");
				}
			});
		}

	}
}