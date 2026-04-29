using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tik;

internal static class Notifier
{
  internal static void Send(string title, string message)
  {
    // Terminal bell as universal fallback
    Console.Write('\a');

    try
    {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        SendLinux(title, message);
      else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        SendWindows(title, message);
    }
    catch
    {
      // Notification is best-effort; don't crash if it fails
    }
  }

  private static void SendLinux(string title, string message)
  {
    Process.Start(new ProcessStartInfo
    {
      FileName = "notify-send",
      ArgumentList = { title, message },
      UseShellExecute = false,
      RedirectStandardOutput = true,
      RedirectStandardError = true
    });
  }

  private static void SendWindows(string title, string message)
  {
    var script = $"""
            [Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null
            $template = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent([Windows.UI.Notifications.ToastTemplateType]::ToastText02)
            $textNodes = $template.GetElementsByTagName('text')
            $textNodes.Item(0).AppendChild($template.CreateTextNode('{EscapePowerShell(title)}')) | Out-Null
            $textNodes.Item(1).AppendChild($template.CreateTextNode('{EscapePowerShell(message)}')) | Out-Null
            $toast = [Windows.UI.Notifications.ToastNotification]::new($template)
            [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('tik').Show($toast)
            """;

    Process.Start(new ProcessStartInfo
    {
      FileName = "powershell",
      ArgumentList = { "-NoProfile", "-NonInteractive", "-Command", script },
      UseShellExecute = false,
      RedirectStandardOutput = true,
      RedirectStandardError = true,
      CreateNoWindow = true
    });
  }

  private static string EscapePowerShell(string input) =>
      input.Replace("'", "''");
}
