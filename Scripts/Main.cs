using Godot;
using System;

public partial class Main : Node2D
{
	private PackedScene _enemy = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
	private PackedScene _boss = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _form1 = GD.Load<PackedScene>("res://Enemy Formations/Form1.tscn");
	private PackedScene _form2 = GD.Load<PackedScene>("res://Enemy Formations/Form2.tscn");
	private PackedScene _form3 = GD.Load<PackedScene>("res://Enemy Formations/Form3.tscn");
	private PackedScene _form4 = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _form5 = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _healthkitpath = GD.Load<PackedScene>("res://Scenes/health_kit_path.tscn");
	private Node2D currentWave;
	
	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 0.0f, .5f);
		GameManager.player = GetNode<Player>("Player") as Player;
		GameManager.camera = GetNode<CustomCamera>("Camera2D") as CustomCamera;
		ColorRect shaderRect = GetNode<ColorRect>("ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ColorRect2");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
		PowerSpawn();
	}
	private async void SpawnWave() {
		var random = GD.RandRange(1, 8);
		if(random<=2){
			Node2D SpawnedEnemy = _form2.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.GlobalPosition = new Vector2 (0, 0);
			currentWave=SpawnedEnemy;
		} else if(random<=4){
			Node2D SpawnedEnemy = _form2.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.GlobalPosition = new Vector2 (0, 0);
			currentWave=SpawnedEnemy;
		} else if(random<=6){
			Node2D SpawnedEnemy = _form2.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.GlobalPosition = new Vector2 (0, 0);
			currentWave=SpawnedEnemy;
		} else {
			Node2D SpawnedEnemy = _boss.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.Position = new Vector2 (0, 0);
			currentWave=SpawnedEnemy;
		}
		
	}
	public override void _Process(double delta)
	{
		GetNode<RichTextLabel>("RichTextLabel").Text="Score: " + GameManager.score.ToString();
		RumbleController.vibrationTimeLeft-=(float)delta;
		if(RumbleController.vibrationTimeLeft<=0) {
			RumbleController.vibrationTimeLeft=0;
			RumbleController.currentPower=0;
		}
		if(!GodotObject.IsInstanceValid(currentWave)) {
			SpawnWave();
		}
	}
	public async void PowerSpawn()
	{
		float ranNum = GD.RandRange(60, 120);
		await ToSignal(GetTree().CreateTimer(ranNum), SceneTreeTimer.SignalName.Timeout);
 		Node2D spawnedHealth = _healthkitpath.Instantiate() as Node2D;
		AddChild(spawnedHealth);
		PowerSpawn();
	}
	
	public async void Leave() {
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 1.0f, .5f);
		await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://Scenes/endScreen.tscn");
	}
}
