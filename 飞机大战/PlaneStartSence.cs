using Godot;
using System;

public partial class PlaneStartSence : Control
{
	private static readonly PackedScene GAME_SCENE=
			GD.Load<PackedScene>("res://飞机大战/飞机大战.tscn");
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("fly"))
		{
			GetTree().ChangeSceneToPacked(GAME_SCENE);
		}
	}
}
