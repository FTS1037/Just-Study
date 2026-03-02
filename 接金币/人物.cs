using Godot;
using System;

public partial class 人物 : Area2D
{
	[Export] float _speed = 300.0f;//速度
	[Export] float _margin = 50.0f;//边界
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// 移动
		if (Input.IsActionPressed("right") == true)
		{
			Position += new Vector2(_speed * (float)delta, 0);
		}
		if (Input.IsActionPressed("left") == true)
		{
			Position -= new Vector2(_speed * (float)delta, 0);
		}

		// 视口边界检测
		Rect2 vpr = GetViewportRect();
		if (vpr.Position.X + _margin > Position.X)
		{
			Position = new Vector2(vpr.Position.X + _margin, Position.Y);
		}
		if (vpr.End.X - _margin < Position.X)
		{
			Position = new Vector2(vpr.End.X - _margin, Position.Y);
		}
	}
}
