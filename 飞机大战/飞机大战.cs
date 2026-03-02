using Godot;
using System;

public partial class 飞机大战 : Node2D
{
	private static readonly PackedScene MAIN_SCENE=
			GD.Load<PackedScene>("res://飞机大战/PlaneStartSence.tscn");//主场景
	[Export] private PackedScene _pillarScene;//柱子场景
	[Export] private Timer _spawnTimer;//生成计时器
	[Export] private Marker2D _spwanTop;//生成上边界
	[Export] private Marker2D _spwanFloor;//生成下边界
	[Export] private Node2D _pillerHolder;//柱子容器
	[Export] private AliceFly _aliceFly;//主角
	[Export] private AudioStreamPlayer2D _backMusic;//背景音乐

	private bool _gameOver = false;//游戏结束标志
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//连接计时器信号
		_spawnTimer.Timeout += SpawnPillar;
		SpawnPillar();

		//连接游戏结束信号
		_aliceFly.OnGameOver +=OnGameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("fly") && _gameOver)
		{
			GD.Print("返回");
			ChangeSceneToMain();
		}

		if(Input.IsKeyPressed(Key.Q))
		{
			ChangeSceneToMain();
		}
	}

	//生成柱子
	private void SpawnPillar()
	{
		Pillar pillar=(Pillar)_pillarScene.Instantiate();
		_pillerHolder.AddChild(pillar);

		//随机位置生成
		float rY=(float)GD.RandRange(_spwanTop.Position.Y,_spwanFloor.Position.Y);
		pillar.Position=new Vector2(_spwanTop.Position.X,rY);
	}

	//返回主菜单
	private void ChangeSceneToMain()
	{
		GetTree().ChangeSceneToPacked(MAIN_SCENE);
	}

	//游戏结束处理
	private void OnGameOver()
	{
		foreach(Pillar pillar in _pillerHolder.GetChildren())
		{
			pillar.SetProcess(false);
		}
		_spawnTimer.Stop();
		_backMusic.Stop();
		//SetProcess(false);	
		_gameOver=true;
	}
}
