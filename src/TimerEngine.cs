namespace Tik;

internal static class TimerEngine
{
  public static void Run(int totalSeconds, ConsoleColor barColor)
  {
    ConsoleRenderer.Init(barColor);
    int frameIndex = 0;

    for (int remaining = totalSeconds; remaining >= 0; remaining--)
    {
      ConsoleRenderer.Render(remaining, totalSeconds, frameIndex);
      frameIndex++;

      if (remaining > 0)
        Thread.Sleep(1000);
    }

    ConsoleRenderer.ShowCompletion();
  }
}
