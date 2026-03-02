using Godot;
using System;

public partial class HungerBar : Control
{
	[Export] public ProgressBar _progressBar;//进度条
	[Export] private Timer _timer;//计时器
	[Export] private float _hungerRate=1.0f;//饥饿增加速率
	[Export] public float _startValue=100.0f;//初始值
	
	[Signal] public delegate void OnGameOverEventHandler();//游戏结束信号
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timer.Timeout += OnTimerTimeout;
		OnTimerTimeout();
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	//饥饿值变化
	private void OnTimerTimeout()
	{	
		_startValue -=_hungerRate;
		RemainValue();
	}
	
	//饥饿值判断
	private void RemainValue()
	{
		_progressBar.Value = _startValue;
		if(_progressBar.Value<30&&_progressBar.Value>0)
		{
			_progressBar.Modulate=new Color(
				(float)0.8627451, (float)0.078431375, (float)0.23529412, 1);//红色
		}
		else if(_progressBar.Value>=30&&_progressBar.Value<=100)
		{
			_progressBar.Modulate=new Color(
				(float)0.59607846, (float)0.9843137, (float)0.59607846, 1);//绿色
		}
		else if(_progressBar.Value<=0)
		{
			_progressBar.Value=0.0f;
			EmitSignal(SignalName.OnGameOver);
			_timer.Stop();
			SetProcess(false);
		}
	}
}
