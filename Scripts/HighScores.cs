using Godot;
using System;

public partial class HighScores : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<RichTextLabel>("HighScore").Text="Score: " + GameManager.highScore.ToString() + "\nPlayer: " + GameManager.highScorePlayer.ToString();
	}
}
