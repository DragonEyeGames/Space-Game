using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 0.0f, .5f);
		ColorRect shaderRect = GetNode<ColorRect>("ColorRect");
		(shaderRect.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, 10.0), (float)GD.RandRange(0.0, 10.0)));
		ColorRect shaderRectTwo = GetNode<ColorRect>("ColorRect2");
		(shaderRectTwo.Material as ShaderMaterial).SetShaderParameter("randomTranslation", new Vector2((float)GD.RandRange(0.0, -10.0), (float)GD.RandRange(0.0, -10.0)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public async void Start() {
		Tween tween = CreateTween();
		tween.TweenProperty(GetNode<ColorRect>("ColorRect3"), "modulate:a", 1.0f, .5f);
		await ToSignal(GetTree().CreateTimer(.5f), SceneTreeTimer.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
}
