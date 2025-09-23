using Godot;
using System;

public partial class SaveSystem
{
	private static string SavePath = "user://scores.json";

	public static void SaveScore(string playerName, int score)
	{
		var data = new Godot.Collections.Dictionary<string, Variant>
		{
			{ "playerName", playerName },
			{ "score", score }
		};

		string json = Json.Stringify(data);

		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		file.StoreString(json);
	}

	public static (string playerName, int score) LoadScore()
	{
		if (!FileAccess.FileExists(SavePath))
			return ("", 0);

		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Read);
		string json = file.GetAsText();

		var result = Json.ParseString(json);
		if (result is Godot.Collections.Dictionary dict)
		{
			string name = dict.Contains("playerName") ? dict["playerName"].ToString() : "";
			int score = dict.Contains("score") ? Convert.ToInt32(dict["score"]) : 0;
			return (name, score);
		}

		return ("", 0);
	}
}
