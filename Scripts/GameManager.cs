using Godot;
using System;

public partial class GameManager : Node
{
	public static Player player;
	public static CustomCamera camera;
	public static int score = 0;
	public static int highScore=0;
	public static string highScorePlayer="";
	public static int highScore2=0;
	public static string highScorePlayer2="";
	public static int highScore3=0;
	public static string highScorePlayer3="";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
