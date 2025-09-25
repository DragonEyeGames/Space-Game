using Godot;
using System;

public partial class SaveGame : Resource
{
	const string SAVE_GAME_PATH = "user://savegame.tres";
	[Export] public int highScore { get; set; } = 0;
	[Export] public string highScoreName { get; set; } = "";
	[Export] public int highScore2 { get; set; } = 0;
	[Export] public string highScoreName2 { get; set; } = "";
	[Export] public int highScore3 { get; set; } = 0;
	[Export] public string highScoreName3 { get; set; } = "";
	
	public void WriteSavegame()
	{
		Error err = ResourceSaver.Save((Resource)this, "user://savegame.tres");
		if (err != Error.Ok) {
			GD.PrintErr("Failed to save game: ", err);
		}
	}

	public static SaveGame LoadSavegame()
	{
		if (ResourceLoader.Exists(SAVE_GAME_PATH))
		{
			return ResourceLoader.Load<SaveGame>(SAVE_GAME_PATH);
		}
		return null;
	}
}
