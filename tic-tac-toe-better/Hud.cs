using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using TicTacToeBetter;
using TicTakToe;

/// <summary>
/// Heads-up display for the game field: shows whose turn it is and, once the game ends,
/// an end-game window with the result and a "play again" option.
/// </summary>
public partial class Hud : Control, IObserver
{
    /// <summary>The scene this HUD belongs to, used to start a new game on "play again".</summary>
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

    /// <summary>Displays the end-game window with the given result text.</summary>
    /// <param name="info">The message to show, e.g. the winner's name or <c>"Draw"</c>.</param>
    public void ShowEndWindow(string info)
	{
        EndWindowLabel.Text = info;
        EndGameWindow.Visible = true;
    }

    /// <summary>Hides the end-game window without changing its text.</summary>
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



	/// <summary>Updates the current-player label whenever the observed <see cref="Game"/> changes.</summary>
	/// <param name="subject">The subject that changed; only <see cref="Game"/> instances are handled.</param>
	public void Update(ISubject subject)
	{
		if (subject is Game game)
		{
            CurrentPlayerLabel.Text = game.CurrentPlayer.Type.ToString() + " Turn";

        }
    }
}
