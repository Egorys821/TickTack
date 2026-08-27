using Godot;
using System;
using TicTakToe;

public partial class Main : Control
{
    // Called when the node enters the scene tree for the first time.

    [Export]
    LineEdit FirstPlayerLineEdit;

    [Export]
    LineEdit SecondPlayerLineEdit;

    [Export]
    SpinBox GameSizeBox;

    [Export]
    CheckBox FixedGameLength;

    [Export]
    ButtonGroup WinLenButtonGroup;

    public override void _Ready()
    {
        NodePath StartButtonPath = new NodePath("StartButton");
       
        Button StartButton = GetNode<Button>(StartButtonPath);

        //StartButton.Pressed
        StartButton.Pressed += StartButtonPressed;
        StartButton.Pressed += PrintNickNames;
    }

    private void StartButtonPressed()
    {
        //save Names
        string name1 = FirstPlayerLineEdit.Text;
        string name2 = SecondPlayerLineEdit.Text;

        Variables.WinLen = int.Parse(((Button)WinLenButtonGroup.GetPressedButton()).Text);
        Variables.FixedGameSize = FixedGameLength.ButtonPressed;
        Variables.GameSize = (int)GameSizeBox.Value;

        Variables.Player1 = new Player(name1, FieldType.X);
        Variables.Player2 = new Player(name2, FieldType.O);

        GetTree().ChangeSceneToFile(GamePaths.GameScenePath);
    }

    public void PrintNickNames()
    {
        string name1 = GetNode<LineEdit>("FirstPlayerName").Text;
        string name2 = GetNode<LineEdit>("SecondPlayerName").Text;

        GetNode<LineEdit>("FirstPlayerName").Text = "Net ne ty";

        GD.Print("first  player name: " + name1);
        GD.Print("second player name: " + name2);
        Console.WriteLine("Hello from console");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }


}
