using Godot;
using System;

public partial class EndScreen : Control
{
	private SaveGame save;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 0.0f, .5f);
		GetNode<RichTextLabel>("Score").Text = "Score: " + GameManager.score.ToString();
		string ScoreDisplay = "";
		if(GameManager.highScore!=0) {
			ScoreDisplay = ScoreDisplay + GameManager.highScore.ToString() + ": " + GameManager.highScorePlayer;
			if(GameManager.highScore2!=0) {
				ScoreDisplay = ScoreDisplay + "\n" + GameManager.highScore2.ToString() + ": " + GameManager.highScorePlayer2;
				if(GameManager.highScore2!=0) {
					ScoreDisplay = ScoreDisplay + "\n" + GameManager.highScore3.ToString() + ": " + GameManager.highScorePlayer3;
				}
			}
		}
		if(GameManager.score>GameManager.highScore) {
			GetNode<Button>("Add High Score").Visible=true;
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible=true;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=false;
			GetNode<Button>("Continue").Visible=false;
		} else if(GameManager.score>GameManager.highScore2) {
			GetNode<Button>("Add High Score").Visible=true;
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible=true;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=false;
			GetNode<Button>("Continue").Visible=false;
		} else if(GameManager.score>GameManager.highScore3) {
			GetNode<Button>("Add High Score").Visible=true;
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible=true;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=false;
			GetNode<Button>("Continue").Visible=false;
		} else {
			GetNode<Button>("Add High Score").Visible=false;
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible = false;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=true;
			GetNode<Button>("Continue").Visible=true;
		}
		GetNode<RichTextLabel>("Score List").Text = ScoreDisplay;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public async override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Shoot") && GetNode<ColorRect>("Add High Score/ColorRect4").Visible) {
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible=false;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=false;
			NewScore();
		}
		if(Input.IsActionJustPressed("Shoot") && GetNode<ColorRect>("Continue/ColorRect4").Visible) {
			GetNode<ColorRect>("Add High Score/ColorRect4").Visible=false;
			GetNode<ColorRect>("Continue/ColorRect4").Visible=false;
			Tween tween = CreateTween();
			tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 1.0f, .5f);
			await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
			GetTree().ChangeSceneToFile("res://Scenes/menu.tscn");
		}
	}
	
	public void NewScore() {
		GetNode<Button>("Add High Score").Visible=false;
		GetNode<Control>("NameSelect").Visible=true;
	}
	
	public void SubmitName() {
		string name = GetNode<RichTextLabel>("NameSelect/1/Letter").Text + GetNode<RichTextLabel>("NameSelect/2/Letter").Text + GetNode<RichTextLabel>("NameSelect/3/Letter").Text + GetNode<RichTextLabel>("NameSelect/4/Letter").Text + GetNode<RichTextLabel>("NameSelect/5/Letter").Text;
		save = new SaveGame();
		if(GameManager.score>GameManager.highScore) {
			save.highScore=GameManager.score;
			save.highScoreName=name;
			save.highScore2=GameManager.highScore;
			save.highScoreName2=GameManager.highScorePlayer;
			save.highScore3=GameManager.highScore2;
			save.highScoreName3=GameManager.highScorePlayer2;
			GetNode<Button>("Continue").Visible=false;
			GetNode<Button>("Add High Score").Visible=true;
		} else if(GameManager.score>GameManager.highScore2) {
			save.highScore=GameManager.highScore;
			save.highScoreName=GameManager.highScorePlayer;
			save.highScore2=GameManager.score;
			save.highScoreName2=name;
			save.highScore3=GameManager.highScore2;
			save.highScoreName3=GameManager.highScorePlayer2;
			GetNode<Button>("Continue").Visible=false;
			GetNode<Button>("Add High Score").Visible=true;
		} else if(GameManager.score>GameManager.highScore3) {
			save.highScore=GameManager.highScore;
			save.highScoreName=GameManager.highScorePlayer;
			save.highScore2=GameManager.highScore2;
			save.highScoreName2=GameManager.highScorePlayer2;
			save.highScore3=GameManager.score;
			save.highScoreName3=name;
			GetNode<Button>("Continue").Visible=false;
			GetNode<Button>("Add High Score").Visible=true;
		} else {
			save.highScore=GameManager.highScore;
			save.highScoreName=GameManager.highScorePlayer;
			save.highScore2=GameManager.highScore2;
			save.highScoreName2=GameManager.highScorePlayer2;
			save.highScore3=GameManager.highScore3;
			save.highScoreName3=GameManager.highScorePlayer3;
			GetNode<Button>("Continue").Visible=true;
			GetNode<Button>("Add High Score").Visible=false;
		}
		save.WriteSavegame();
		GetNode<Control>("NameSelect").Visible=false;
		GetNode<Button>("Add High Score").Visible=false;
		GetNode<Control>("Continue").Visible=true;
		GetNode<ColorRect>("Continue/ColorRect4").Visible=true;
	}
}
