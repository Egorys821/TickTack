using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using TicTacToeBetter;
using TicTakToe;

public partial class Hud : Control, IObserver
{
    public GameScene gameScene;
    private bool added = false;
	Label CurrentPlayerLabel;

	Window EndGameWindow;
	Label EndWindowLabel;

    [Export]
    Button PlayAgainButton;
    [Export]
	Button ExitButton;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        CurrentPlayerLabel = GetNode<Label>("TurnLabel");
        EndGameWindow = GetNode<Window>("EndGameWindow");
		EndWindowLabel = GetNode<Label>("EndGameWindow/Label");
        

        PlayAgainButton.Pressed += PlayAgainButton_Pressed;
        ExitButton.Pressed += ExitButton_Pressed;


        EndGameWindow.Visible = false;
    }
    

    private void ExitButton_Pressed()
    {
        GetTree().Quit();
    }

    private void PlayAgainButton_Pressed()
    {
        ArgumentNullException.ThrowIfNull(gameScene);

        gameScene.CreateNewGame();
        EndGameWindow.Visible = false;
    }

    public void ShowEndWindow(string info)
	{
        EndWindowLabel.Text = info;
        EndGameWindow.Visible = true;
    }
    public void CloseEndWindow()
    {
        EndGameWindow.Visible = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		
		if (!added && Variables.Game is not null)
		{
			Variables.Game.Attach(this);
			added = true;
           
        }
		
	}



	// Implement IObserver
	public void Update(ISubject subject)
	{
		if (subject is Game game)
		{
            CurrentPlayerLabel.Text = game.CurrentPlayer.Type.ToString() + " Turn";

        }
    }
}
