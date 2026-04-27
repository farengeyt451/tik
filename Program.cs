using Timer;

var (value, unit) = ArgParser.Parse(args);
int totalSeconds  = TimeConverter.ToSeconds(value, unit);
TimerEngine.Run(totalSeconds);
