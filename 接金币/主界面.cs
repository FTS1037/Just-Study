using Godot;
using System;
using System.Runtime.CompilerServices;
public partial class 主界面 : Node2D
{
	//[Export] private 团子 _foodTuanzi;
	[Export] private PackedScene _FoodScene;//团子场景
	[Export] private Timer _timer;//计时器
	[Export] float _marginFood=50.0f;//边界 
	[Export] private Label _scoreLabel;//得分标签
	[Export] private HungerBar _hungerBar;//饥饿值
	[Export] private float _recoverRate=2.0f;//回复速率
	[Export] private AudioStreamPlayer _backBGM;//背景音乐
	[Export] private AudioStreamPlayer2D _getMusic;//得分音乐


	private int _score=0;//得分
	// Called when the node enters the scene tree for the first time.
	
	 public override void _Ready()
	 {
	    // 团子 FoodTuanzi = GetNode<团子>("团子");
	 	//_foodTuanzi.OnScored += OnScored;

		//连接计时器信号
		_timer.Timeout += SpawnFood;
		SpawnFood();

		//连接游戏结束信号
		_hungerBar.OnGameOver +=OnGameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
	}

	//生成团子
	private void SpawnFood()
	{
		Rect2 vpr = GetViewportRect();
		团子 foodTuanzi=(团子)_FoodScene.Instantiate();
		AddChild(foodTuanzi);

		//随机位置生成
		float rX=(float)GD.RandRange(vpr.Position.X+_marginFood,vpr.End.X-_marginFood);
		foodTuanzi.Position=new Vector2(rX,-100);
		foodTuanzi.OnScored += OnScored;

	}

	// 得分处理
	private void OnScored()
	{
		GD.Print("得分了！");
		_getMusic.Play();
		_score++;
		_scoreLabel.Text=$"{_score:00}";

		//回复
		_hungerBar._startValue += _recoverRate;
		if(_hungerBar._startValue>100)
		{
			_hungerBar._startValue=100.0f;
		}
	}

	private void OnGameOver()
	{
		GD.Print("游戏结束！");
		foreach(Node node in GetChildren())
		{
			node.SetProcess(false);
		}
		_timer.Stop();
		_backBGM.Stop();
		SetProcess(false);
	}
}
