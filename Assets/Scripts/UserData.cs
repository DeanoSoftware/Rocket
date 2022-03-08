using System.Collections.Generic;

[System.Serializable]
public class UserData
{
    public int HighScore { get; set; }
    public int FailCount { get; set; }

    // Themes
    public int SelectedTheme { get; set; }

    // Unlocked themes
    public bool ThemeBlue { get; set; }
    public bool ThemeGreen { get; set; }
    public bool ThemeRed { get; set; }
    public bool ThemePurple { get; set; }

    // Badges
    public Dictionary<Enums.Badges, bool> Badges { get; set; }
    /*
    // Score
    public bool ScoreFive { get; set; }
    public bool ScoreTen { get; set; }
    public bool ScoreTwenty { get; set; }
    public bool ScoreFifty { get; set; }
    public bool ScoreOneHundred { get; set; }
    public bool ScoreTwoHundred { get; set; }

    // Combi
    public bool ComboOne { get; set; }
    public bool CombiFive { get; set; }
    public bool CombiTen { get; set; }
    public bool CombiFifteen { get; set; }
    public bool CombiTwenty { get; set; }
    public bool CombiTwentyFive { get; set; }
    public bool CombiThirty { get; set; }

    // Activity
    public bool RecoverFromHalf { get; set; }
    public bool NearDeath { get; set; }
    public bool WatchTenAdverts { get; set; }
    public bool WatchTwentyAdverts { get; set; }
    */
}
