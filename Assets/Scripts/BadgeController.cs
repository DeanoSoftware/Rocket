using System.Collections.Generic;

public static class BadgeController
{
    public static List<Enums.Badges> CalculateScoreBadges(UserData userData, int score)
    {
        var badges = new List<Enums.Badges>();

        if (userData.Badges == null)
        {
            userData.Badges = new Dictionary<Enums.Badges, bool>();
        }

        if (score >= 5 && !userData.Badges.ContainsKey(Enums.Badges.ScoreFive))
        {
            badges.Add(Enums.Badges.ScoreFive);
        }

        if (score >= 10 && !userData.Badges.ContainsKey(Enums.Badges.ScoreTen))
        {
            badges.Add(Enums.Badges.ScoreTen);
        }

        if (score >= 20 && !userData.Badges.ContainsKey(Enums.Badges.ScoreTwenty))
        {
            badges.Add(Enums.Badges.ScoreTwenty);
        }

        if (score >= 50 && !userData.Badges.ContainsKey(Enums.Badges.ScoreFifty))
        {
            badges.Add(Enums.Badges.ScoreFifty);
        }

        if (score >= 100 && !userData.Badges.ContainsKey(Enums.Badges.ScoreOneHundred))
        {
            badges.Add(Enums.Badges.ScoreOneHundred);
        }

        if (score >= 200 && !userData.Badges.ContainsKey(Enums.Badges.ScoreTwoHundred))
        {
            badges.Add(Enums.Badges.ScoreTwoHundred);
        }

        return badges;
    }

    public static List<Enums.Badges> CalculateComboBadges(UserData userData, int comboCount)
    {
        var badges = new List<Enums.Badges>();

        if (userData.Badges == null)
        {
            userData.Badges = new Dictionary<Enums.Badges, bool>();
        }

        if (comboCount >= 1 && !userData.Badges.ContainsKey(Enums.Badges.PerfectOne))
        {
            badges.Add(Enums.Badges.PerfectOne);
        }

        if (comboCount >= 3 && !userData.Badges.ContainsKey(Enums.Badges.PerfectThree))
        {
            badges.Add(Enums.Badges.PerfectThree);
        }

        if (comboCount >= 5 && !userData.Badges.ContainsKey(Enums.Badges.PerfectFive))
        {
            badges.Add(Enums.Badges.PerfectFive);
        }

        if (comboCount >= 10 && !userData.Badges.ContainsKey(Enums.Badges.PerfectTen))
        {
            badges.Add(Enums.Badges.PerfectTen);
        }

        if (comboCount >= 15 && !userData.Badges.ContainsKey(Enums.Badges.PerfectFifteen))
        {
            badges.Add(Enums.Badges.PerfectFifteen);
        }

        return badges;
    }

    public static List<Enums.Badges> CalculateActivityBadges(UserData userData)
    {
        var badges = new List<Enums.Badges>();

        if (userData.Badges == null)
        {
            userData.Badges = new Dictionary<Enums.Badges, bool>();
        }

        if (userData.FailCount >= 20 && !userData.Badges.ContainsKey(Enums.Badges.FailTwenty))
        {
            badges.Add(Enums.Badges.FailTwenty);
        }

        if (userData.FailCount >= 50 && !userData.Badges.ContainsKey(Enums.Badges.FailFifty))
        {
            badges.Add(Enums.Badges.FailFifty);
        }

        if (userData.FailCount >= 100 && !userData.Badges.ContainsKey(Enums.Badges.FailOneHundred))
        {
            badges.Add(Enums.Badges.FailOneHundred);
        }

        return badges;
    }

    public static string GetBadgeText(Enums.Badges badge)
    {
        switch (badge)
        {
            case Enums.Badges.ScoreFive:
                return "Score 5";
            case Enums.Badges.ScoreTen:
                return "Score 10";
            case Enums.Badges.ScoreTwenty:
                return "Score 20";
            case Enums.Badges.ScoreFifty:
                return "Score 50";
            case Enums.Badges.ScoreOneHundred:
                return "Score 100";
            case Enums.Badges.ScoreTwoHundred:
                return "Score 200";
            case Enums.Badges.PerfectOne:
                return "Get a perfect";
            case Enums.Badges.PerfectThree:
                return "Perfect x3";
            case Enums.Badges.PerfectFive:
                return "Perfect x5";
            case Enums.Badges.PerfectTen:
                return "Perfect x10";
            case Enums.Badges.PerfectFifteen:
                return "Perfect x15";
            case Enums.Badges.FailTwenty:
                return "Fail x20";
            case Enums.Badges.FailFifty:
                return "Fail x50";
            case Enums.Badges.FailOneHundred:
                return "Fail x100";
            case Enums.Badges.WatchFiveAdverts:
                return "Watch 5 adverts";
            case Enums.Badges.WatchTenAdverts:
                return "Watch 10 adverts";
            default:
                return "";
        }
    }
}
