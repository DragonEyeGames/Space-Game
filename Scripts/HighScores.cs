using Godot;
using System;

public partial class HighScores : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("Fog"), "modulate:a", 0.0f, .5f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public async override void _Process(double delta)
	{
		GetNode<RichTextLabel>("HighScore").Text="Score: " + GameManager.highScore.ToString() + "\nPlayer: " + GameManager.highScorePlayer.ToString();
		GetNode<RichTextLabel>("HighScore2").Text="Score: " + GameManager.highScore2.ToString() + "\nPlayer: " + GameManager.highScorePlayer2.ToString();
		GetNode<RichTextLabel>("HighScore3").Text="Score: " + GameManager.highScore3.ToString() + "\nPlayer: " + GameManager.highScorePlayer3.ToString();
		if(Input.IsActionJustPressed("Shoot") || Input.IsActionJustPressed("Leave")) {
			Tween tween = CreateTween();
			tween.TweenProperty(GetNode<ColorRect>("Fog"), "modulate:a", 1.0f, .5f);
			await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}
}
