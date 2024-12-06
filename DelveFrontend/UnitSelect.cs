using Godot;
using System;

public partial class UnitSelect : Control
{
	private Button BackButton { get; set; }
	private VBoxContainer UnitRows { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BackButton = GetNode<Button>("BackButton");
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnUnitSelect()
	{
		
	}

	private void Back()
	{
		GameManager.Instance.ChangeScene("res://team_select.tscn");
	}
}
