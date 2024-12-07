using Godot;
using System;
using DelveInternals;
using DelveInternals.Units;

public partial class UnitSelect : Control
{
	private Button BackButton { get; set; }
	private VBoxContainer UnitRows { get; set; }
	private Label _goldDisplay;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BackButton = GetNode<Button>("BackButton");
		UnitRows = GetNode<VBoxContainer>("unit_list/unit_rows");
		_goldDisplay = GetNode<Label>("player_gold");

		BackButton.Pressed += Back;
		
		var unitDisplay = GD.Load<PackedScene>("res://unit_display.tscn");
		var currentRow = new HBoxContainer();
		currentRow.SizeFlagsHorizontal = SizeFlags.ExpandFill;
		var screenWidth = GetViewportRect().Size.X;
		
		UnitRows.AddChild(currentRow);	
		foreach (Unit unit in UnitManager.Units)
		{
			var displayBox = new VBoxContainer();
			displayBox.SizeFlagsHorizontal = SizeFlags.ExpandFill;
			
			var display = (UnitDisplay)unitDisplay.Instantiate();
			displayBox.AddChild(display);
			
			var selectButton = new Button();
			selectButton.Text = "Select";
			selectButton.Pressed += () => OnUnitSelect(unit);
			displayBox.AddChild(selectButton);
			
			if (screenWidth - currentRow.Size.X < display.Size.X)
			{
				currentRow = new HBoxContainer(); 
				UnitRows.AddChild(currentRow);
			} 
			currentRow.AddChild(displayBox);
			// this has to be called after it enters the scene tree or it won't have called _Ready() yet and there will be a NullRef	
			display.SetUnit(unit);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnUnitSelect(Unit selectedUnit)
	{
		// use the static variables to update state and then swap back to TeamSelect
		GD.Print("selected a unit!");
		GameManager.Instance.CurrentTeam[TeamSelect.CurrentModifyingSlot] = selectedUnit;
		Back();
	}

	private void Back()
	{
		GameManager.Instance.ChangeScene("res://team_select.tscn");
	}
}
