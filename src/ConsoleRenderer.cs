namespace Tik;

internal static class ConsoleRenderer
{
  private const int BarWidth = 20;
  private static int _row;
  private static ConsoleColor _barColor;

  internal static void Init(ConsoleColor barColor)
  {
    _barColor = barColor;
    Console.CursorVisible = false;
    Console.CancelKeyPress += (_, _) => Console.CursorVisible = true;
    _row = Console.CursorTop;
    Console.WriteLine();
    Console.WriteLine();
  }

  internal static void Render(int remaining, int total, int frameIndex)
  {
    double progress = total > 0 ? 1.0 - (double)remaining / total : 1.0;
    int filled = (int)(progress * BarWidth);
    int empty = BarWidth - filled;

    string bar = new string('█', filled) + new string('░', empty);
    string time = FormatTime(remaining);
    string animation = GetAnimationFrame(frameIndex);

    Console.SetCursorPosition(0, SafeRow(_row));
    Console.ForegroundColor = _barColor;
    Console.Write($"[{bar}] ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{time} ");
    Console.ForegroundColor = _barColor;
    Console.Write($"{animation}   ");
    Console.ResetColor();
  }

  private static string GetAnimationFrame(int index)
  {
    char[] frames = ['|', '/', '-', '\\'];
    return $"[{frames[index % frames.Length]}]";
  }

  internal static void ShowCompletion()
  {
    Console.SetCursorPosition(0, SafeRow(_row + 1));
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine();
    Console.WriteLine(" Done! Time is up.   ");
    Console.ResetColor();
    Console.CursorVisible = true;
  }

  private static int SafeRow(int row) =>
      Math.Clamp(row, 0, Console.BufferHeight - 1);

  private static string FormatTime(int totalSeconds)
  {
    int h = totalSeconds / 3600;
    int m = (totalSeconds % 3600) / 60;
    int s = totalSeconds % 60;
    return $"{h:D2}:{m:D2}:{s:D2}";
  }
}
