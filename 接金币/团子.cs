using Godot;
using System;
using System.Numerics;

public partial class 团子 : Area2D
{
	[Export] float _speed = 100.0f;//速度
	[Signal] public delegate void OnScoredEventHandler();//得分信号
	[Signal] public delegate void OnGetMusicEventHandler();//得分音乐信号
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// 连接碰撞信号
		AreaEntered += OnAreaEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	// 自动下落
	public override void _Process(double delta)
	{
		Position += new Godot.Vector2(0,_speed * (float)delta);
		OnHitButtion();
	}

	//触底检测
	private void OnHitButtion()
	{
		Rect2 vpr = GetViewportRect();
		if(vpr.End.Y < Position.Y)
		{
			GD.Print("超出边界，删除团子");
			QueueFree();
		}
	}

	// 碰撞检测
	private void OnAreaEntered(Area2D area)
	{
		GD.Print("碰撞到团子");
		EmitSignal(SignalName.OnScored);
		EmitSignal(SignalName.OnGetMusic);
		QueueFree();
	}
}
