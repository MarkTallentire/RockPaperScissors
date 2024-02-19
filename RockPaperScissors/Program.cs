Game game = new Game();
game.StartGame();

public class Game
{
    private Player _player1;
    private Player _player2;
    private List<Round> _rounds = new ();

    public void StartGame()
    {
        Console.Write("Player 1, enter your name: ");
        _player1 = new Player(Console.ReadLine());
        Console.Write("Player 2, enter your name: ");
        _player2 = new Player(Console.ReadLine());
        Console.Clear();
        RunGame();
    }

    private void RunGame()
    {
        var roundNumber = 1;
        
        while (true)
        {
            Console.WriteLine($"STATUS: Player 1 Wins: {_rounds.Count(x=>x.RoundWinner == _player1)}, Player 2 Wins: {_rounds.Count(x=>x.RoundWinner == _player2)}, Draws: {_rounds.Count(x=>x.RoundWinner == null)}");
            
            var round = new Round(_player1, _player2, roundNumber);
            round.PlayRound();
            _rounds.Add(round);
            EndGame();
            
            roundNumber++;
            
        }
    }

    private void EndGame()
    {
        int _player1wins = _rounds.Count(x => x.RoundWinner == _player1);
        int _player2wins = _rounds.Count(x => x.RoundWinner == _player2);

        if (_player1wins >= 3)
        {
            Console.WriteLine($"{_player1.Name} wins the game");
            Environment.Exit(0);
        }
        
        else if (_player2wins >= 3)
        {
            Console.WriteLine($"{_player2.Name} wins the game");
            Environment.Exit(0);
        }
        
    }
    
}

public class Round(Player player1, Player player2, int roundNumber)
{
    private int _roundNumber = roundNumber;
    private readonly Player _player1 = player1;
    private readonly Player _player2 = player2;
    public Player? RoundWinner { get; private set; }
    
    public void PlayRound()
    {
        Console.WriteLine($"Round {roundNumber}");
        Console.WriteLine("FIGHT!!!!!");
        var choice1 = _player1.GetChoice();
        var choice2 = _player2.GetChoice();

        var winner = DetermineOutcome(choice1, choice2);
        RecordResult(winner);
    }

    private void RecordResult(int winner)
    {
        if (winner == 1) RoundWinner = _player1;
        if (winner == 2) RoundWinner = _player2;
        if (winner == 0) RoundWinner = null;
    }

    private int DetermineOutcome(PlayerChoice choice1, PlayerChoice choice2)
    {
        if (choice1 == PlayerChoice.Rock && choice2 == PlayerChoice.Scissors) return 1;
        if (choice1 == PlayerChoice.Rock && choice2 == PlayerChoice.Paper) return 2;
        if (choice1 == PlayerChoice.Rock && choice2 == PlayerChoice.Rock) return 0;

        if (choice1 == PlayerChoice.Paper && choice2 == PlayerChoice.Rock) return 1;
        if (choice1 == PlayerChoice.Paper && choice2 == PlayerChoice.Scissors) return 2;
        if (choice1 == PlayerChoice.Paper && choice2 == PlayerChoice.Paper) return 0;

        if (choice1 == PlayerChoice.Scissors && choice2 == PlayerChoice.Paper) return 1;
        if (choice1 == PlayerChoice.Scissors && choice2 == PlayerChoice.Rock) return 2;
        if (choice1 == PlayerChoice.Scissors && choice2 == PlayerChoice.Scissors) return 0;

        return 0;
    }
}

public class Player(string name)
{
    public string Name { get; private set; } = name;

    public PlayerChoice GetChoice()
    {
        Console.Write($"{Name}, make your choice (rock, paper, scissors): ");
        var choice = Console.ReadLine();
        Console.Clear();
        
        return choice switch
        {
            "rock" => PlayerChoice.Rock,
            "paper" => PlayerChoice.Paper,
            "scissors" => PlayerChoice.Scissors,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}




public enum PlayerChoice {Rock, Paper, Scissors};