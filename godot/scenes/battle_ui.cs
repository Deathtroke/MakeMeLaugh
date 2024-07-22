using Godot;
using System;

public partial class battle_ui : CanvasLayer
{
	private Hand _hand;
	public ap_ui _ap_ui;
	private Button _end_turn_button;
	private Button _start_sim_button;
	private Button _start_sim_button_setp;
	public bool isSimulating = false;
	public delegate void EndTurnHandler();
	public event EndTurnHandler EndTurn;

	public delegate void SimButtonHandler();

	public event SimButtonHandler SimStart;
	
	public delegate void SimStepButtonHandler();
	public event SimStepButtonHandler SimStepStart;
	private void _ready()
	{
		_hand = GetNode<Hand>("Hand");
		_ap_ui = GetNode<ap_ui>("APUI");
		_end_turn_button = GetNode<Button>("EndTurnButton");
		_end_turn_button.Pressed += OnEndTurnButtonPressed;
		_start_sim_button = GetNode<Button>("Simulation");
		_start_sim_button.Pressed += OnSimButtonPressed;
		_start_sim_button_setp = GetNode<Button>("SimulationStep");
		_start_sim_button_setp.Pressed += OnSimStepButtonPressed;
	}

	private void OnEndTurnButtonPressed()
	{
		EndTurn?.Invoke();
	}
	
	private void OnSimButtonPressed()
	{
		isSimulating = !isSimulating;
		if (isSimulating)
		{
			_start_sim_button.Text = "stop simulation"; 
		}
		else
		{
			_start_sim_button.Text = "start simulation";
		}

		SimStart?.Invoke();
	}
	
	private void OnSimStepButtonPressed()
	{
		SimStepStart?.Invoke();
	}

	[Export] public CharacterStats Character_stats;
	
	[Export] public CharacterStats Stats
	{
		get { return Character_stats; }
		set { setStats(value); }
	}
	
	private void setStats(CharacterStats value)
	{
		Character_stats = value;
		_ap_ui.char_stats = Character_stats;
		_hand.Character_Stats = Character_stats;
	}
}
