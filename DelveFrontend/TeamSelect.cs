using Godot;
using System;
using System.Collections.Generic;
using DelveInternals;

public partial class TeamSelect : Control
{
	// keeping track of which unit slot is being changed requires a static variable,
	// and it makes more sense for TeamSelect to keep track of that information than the GameManager singleton
	// since it isn't relevant to the gamestate as a whole, only this scene.
	public static int CurrentModifyingSlot = -1;
	
	private Button _swap1;
	private Button _swap2;
	private Button _swap3;
	private Button _startBattle;
	private Button _upgrade1;
	private Button _upgrade2;
	private Button _upgrade3;

	private UnitDisplay _unit1;
	private UnitDisplay _unit2;
	private UnitDisplay _unit3;

	private Label _goldDisplay;
	
	public override void _Ready()
	{
		GD.Print("entering team select!");
		// now that the scene is properly loaded we can fetch all the nodes we need to update dynamically.
		_swap1 = GetNode<Button>("TeamUnits/Unit1/Swap Button");
		_swap2 = GetNode<Button>("TeamUnits/Unit2/Swap Button");
		_swap3 = GetNode<Button>("TeamUnits/Unit3/Swap Button");
		_upgrade1 = GetNode<Button>("TeamUnits/Unit1/Upgrade");
		_upgrade2 = GetNode<Button>("TeamUnits/Unit2/Upgrade2");
		_upgrade3 = GetNode<Button>("TeamUnits/Unit3/Upgrade3");
		_startBattle = GetNode<Button>("start_battle_button");
		_unit1 = GetNode<UnitDisplay>("TeamUnits/Unit1/unit_display");
		_unit2 = GetNode<UnitDisplay>("TeamUnits/Unit2/unit_display2");
		_unit3 = GetNode<UnitDisplay>("TeamUnits/Unit3/unit_display3");
		_goldDisplay = GetNode<Label>("player_gold");
		
		// using lambdas for the event handlers lets us avoid creating 3 different functions for three similar buttons
		_swap1.Pressed += () => SwapPressed(0);
		_swap2.Pressed += () => SwapPressed(1);
		_swap3.Pressed += () => SwapPressed(2);
		_upgrade1.Pressed += () => UpgradePressed(0);
		_upgrade2.Pressed += () => UpgradePressed(1);
		_upgrade3.Pressed += () => UpgradePressed(2);
		_startBattle.Pressed += StartBattle;
		
		// scenes don't save state by default, so we preserve the state of the current team in our singleton and reload it
		// when we enter the scene.
		_unit1.SetUnit(GameManager.Instance.CurrentTeam[0]);
		_unit2.SetUnit(GameManager.Instance.CurrentTeam[1]);
		_unit3.SetUnit(GameManager.Instance.CurrentTeam[2]);

		_goldDisplay.Text = $"Gold: {GameManager.Instance.Gold}";
	}

	public override void _Process(double delta)
	{
	}

	private void SwapPressed(int n)
	{
		CurrentModifyingSlot = n;
		GameManager.Instance.ChangeScene("res://unit_select.tscn");	
	}

	private void UpgradePressed(int n)
	{
		CurrentModifyingSlot = n;
		GameManager.Instance.ChangeScene("res://upgrade_unit.tscn");
	}

	private void StartBattle()
	{
		var sim = new Simulation(GameManager.Instance.CurrentTeam);
		var eventLog = new List<string>();
		foreach (var log in sim.RunSimulation())
		{
			eventLog.Add(log);	
			GD.Print(log);
		}
		GD.Print($"Total Rooms Cleared: {sim.TotalRooms} | Total Gold Earned: {sim.TotalGoldCollected}");
		// update singleton for battle scene to use when it loads	
		GameManager.Instance.LastGoldCollected = sim.TotalGoldCollected;
		GameManager.Instance.LastRoomsCleared = sim.TotalRooms;
		GameManager.Instance.Gold += sim.TotalGoldCollected;
		
		GameManager.Instance.ChangeScene("res://battle.tscn");
	}
}
