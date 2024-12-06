using Godot;
using System;

public partial class TeamSelect : Node2D
{
	public static int CurrentModifyingSlot = 0;
	
	private Button Swap1;
	private Button Swap2;
	private Button Swap3;
	
	public override void _Ready()
	{
		Swap1 = GetNode<Button>("TeamUnits/Unit1/Swap Button");
		Swap2 = GetNode<Button>("TeamUnits/Unit2/Swap Button");
		Swap3 = GetNode<Button>("TeamUnits/Unit3/Swap Button");

		Swap1.Pressed += () => SwapPressed(1);
		Swap2.Pressed += () => SwapPressed(2);
		Swap3.Pressed += () => SwapPressed(3);
	}

	public override void _Process(double delta)
	{
	}

	private void SwapPressed(int n)
	{
		CurrentModifyingSlot = n;
		GameManager.Instance.ChangeScene("res://unit_select.tscn");	
	}
}
