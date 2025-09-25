using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.score = 0;
		var loadedSave = SaveGame.LoadSavegame();
		if (loadedSave != null)
		{
			GameManager.highScorePlayer=loadedSave.highScoreName;
			GameManager.highScore=loadedSave.highScore;
			GameManager.highScorePlayer2=loadedSave.highScoreName2;
			GameManager.highScore2=loadedSave.highScore2;
			GameManager.highScorePlayer3=loadedSave.highScoreName3;
			GameManager.highScore3=loadedSave.highScore3;
			
		}
		else
		{
			GD.Print("No save found.");
		}
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 0.0f, .5f);
		ColorRect shaderRect = GetNode<ColorRect>("ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ColorRect2");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
		GetNode<RichTextLabel>("Previous High Score").Text="High Score: " + GameManager.highScore.ToString();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("MenuDown")) {
			if(GetNode<ColorRect>("Start/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Options/ColorRect4").Visible=true;
				GetNode<ColorRect>("Start/ColorRect4").Visible=false;
			}
			else if(GetNode<ColorRect>("Options/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Options/ColorRect4").Visible=false;
				GetNode<ColorRect>("Scores/ColorRect4").Visible=true;
			}
			else if(GetNode<ColorRect>("Scores/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Scores/ColorRect4").Visible=false;
				GetNode<ColorRect>("Credits/ColorRect4").Visible=true;
			}
			else if(GetNode<ColorRect>("Credits/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Credits/ColorRect4").Visible=false;
				GetNode<ColorRect>("Start/ColorRect4").Visible=true;
			}
		}
		if(Input.IsActionJustPressed("MenuUp")) {
			if(GetNode<ColorRect>("Start/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Credits/ColorRect4").Visible=true;
				GetNode<ColorRect>("Start/ColorRect4").Visible=false;
			}
			else if(GetNode<ColorRect>("Credits/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Credits/ColorRect4").Visible=false;
				GetNode<ColorRect>("Scores/ColorRect4").Visible=true;
			}
			else if(GetNode<ColorRect>("Scores/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Scores/ColorRect4").Visible=false;
				GetNode<ColorRect>("Options/ColorRect4").Visible=true;
			}
			else if(GetNode<ColorRect>("Options/ColorRect4").Visible==true) {
				GetNode<ColorRect>("Options/ColorRect4").Visible=false;
				GetNode<ColorRect>("Start/ColorRect4").Visible=true;
			}
		}
		if(Input.IsActionJustPressed("Shoot")) {
			if(GetNode<ColorRect>("Start/ColorRect4").Visible==true) {
				Start();
			}
			if(GetNode<ColorRect>("Scores/ColorRect4").Visible==true) {
				Scores();
			}
		}
	}
	
	public async void Start() {
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 1.0f, .5f);
		await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
	
	public async void Scores() {
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 1.0f, .5f);
		await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://Scenes/HighScores.tscn");
	}
}
