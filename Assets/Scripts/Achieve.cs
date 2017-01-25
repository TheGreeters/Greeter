using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public enum Achievement
{
	FirstWave,
	MaxSatisfaction,
	TogaParty,
	Alien,
	BingoNight,
}

public class Achieve : MonoBehaviour {

	private static Dictionary<Achievement, string> AchievementIds = new Dictionary<Achievement, string>()
	{
		{ Achievement.FirstWave, GPGSIds.achievement_first_wave },
		{ Achievement.MaxSatisfaction, GPGSIds.achievement_max_satisfaction },
		{ Achievement.TogaParty, GPGSIds.achievement_toga_party },
		{ Achievement.Alien, GPGSIds.achievement_extraterrestrial_encounter },
		{ Achievement.BingoNight, GPGSIds.achievement_bingo_night_here_i_come },
	};

	public static void UnlockAchievement(Achievement achievementId)
	{
#if UNITY_ANDROID
		if (PlayGamesPlatform.Instance.IsAuthenticated() && AchievementIds.ContainsKey(achievementId))
		{
			Social.ReportProgress(AchievementIds[achievementId], 100, (bool success) => {
				// handle success or failure
			});
		}
#endif
	}
}
