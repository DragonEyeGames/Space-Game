using Godot;
using System;

public partial class Main : Node2D
{
	private PackedScene _enemy = GD.Load<PackedScene>("res://Scenes/enemy.tscn");
	
	public override void _Ready()
	{
		ColorRect shaderRect = GetNode<ColorRect>("ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ColorRect2");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
		SpawnWave();
	}
	private void SpawnWave() {
		Enemy SpawnedEnemy = _enemy.Instantiate() as Enemy;
		AddChild(SpawnedEnemy);
		SpawnedEnemy.Initialize(1, 10);
		SpawnedEnemy.Position = new Vector2 (500, 0);
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
