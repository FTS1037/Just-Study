using Godot;
using System;

public partial class Pillar : Area2D
{
	[Signal] public delegate void ScreenExitedEventHandler();//退出屏幕
	
	[Export] private VisibleOnScreenNotifier2D _visible;//可见性检测器
	[Export] private float _speed=250.0f;//移动速度
	[Export] private Area2D _upPillar;//上柱子
	[Export] private Area2D _downPillar;//下柱子
	[Export] private Area2D _barrier;//碰撞体
	[Export] private AudioStreamPlayer2D _scoreSound;//得分音效
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_upPillar.BodyEntered += _onPillarBodyEntered;
		_downPillar.BodyEntered += _onPillarBodyEntered;
		_barrier.BodyEntered += _onCanGetScore;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position +=new Vector2(-_speed*(float)delta,0);
		//检测是否退出屏幕
		if(_visible.IsOnScreen()==false&&Position.X<0)
		{
			EmitSignal(SignalName.ScreenExited);
			GD.Print("柱子退出屏幕");
			QueueFree();
		}
	}

	private void _onPillarBodyEntered(Node2D body)
	{
		if(body is AliceFly)
		{
			(body as AliceFly).GameOver();
		}
	}

	private void _onCanGetScore(Node2D body)
	{
		if(body is AliceFly)
		{
			_scoreSound.Play();
			GD.Print("得分了！");
		}
	}
}
