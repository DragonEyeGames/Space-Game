using Godot;
using System;

public partial class Main : Node2D
{
	private PackedScene _enemy = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
	private PackedScene _boss = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _form1 = GD.Load<PackedScene>("res://Enemy Formations/Form1.tscn");
	private PackedScene _form2 = GD.Load<PackedScene>("res://Enemy Formations/Form2.tscn");
	private PackedScene _form3 = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _form4 = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	private PackedScene _form5 = GD.Load<PackedScene>("res://Scenes/boss.tscn");
	
	public override void _Ready()
	{
		ColorRect shaderRect = GetNode<ColorRect>("ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ColorRect2");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
		SpawnWave();
	}
	private async void SpawnWave() {
		var random = GD.RandRange(1, 5);
		if(random<=3){
			GD.Print("Yes");
			Node2D SpawnedEnemy = _form1.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.GlobalPosition = new Vector2 (0, -100);
		} else if(random>=3){
			GD.Print("Yes");
			Node2D SpawnedEnemy = _form2.Instantiate() as Node2D;
			AddChild(SpawnedEnemy);
			SpawnedEnemy.GlobalPosition = new Vector2 (0, -150);
		} else if(random==3){
			for (int i = 0; i < 5; i++){
				Enemy SpawnedEnemy = _enemy.Instantiate() as Enemy;
				AddChild(SpawnedEnemy);
				SpawnedEnemy.Initialize(1, 10);
				SpawnedEnemy.Position = new Vector2 (450+(100*i), 200);
			}
		} else if(random==4){
			for (int i = 0; i < 5; i++){
				Enemy SpawnedEnemy = _enemy.Instantiate() as Enemy;
				AddChild(SpawnedEnemy);
				SpawnedEnemy.Initialize(1, 10);
				SpawnedEnemy.Position = new Vector2 (450+(100*i), 200);
			}
		} else {
			Boss SpawnedBoss = _boss.Instantiate() as Boss;
			AddChild(SpawnedBoss);
			SpawnedBoss.Initialize(0, 200);
			SpawnedBoss.Position = new Vector2 (630, 300);
			SpawnedBoss.camera=GetNode<CustomCamera>("Camera2D");
		}
		
		await ToSignal(GetTree().CreateTimer(10f), SceneTreeTimer.SignalName.Timeout);
		SpawnWave();
		
	}
	public override void _Process(double delta)
	{
		RumbleController.vibrationTimeLeft-=(float)delta;
		if(RumbleController.vibrationTimeLeft<=0) {
			RumbleController.vibrationTimeLeft=0;
			RumbleController.currentPower=0;
		}
	}
}
