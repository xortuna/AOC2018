using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC4
{
    class Program
    {
        enum EventType
        {
            StartShift,
            FallAsleep,
            WakeUp
        }

        class TimelineEvent
        {
            public DateTime Time;
            public int GuardId;
            public EventType Type;
        }

        class SleepTime
        {
            public DateTime start;
            public DateTime finish;
            public int GetDuration()
            {
                return (int)(finish.Minute - start.Minute);
            }

        }


        static void Main(string[] args)
        {
            Console.WriteLine("Enter file:");
            string inputFile = Console.ReadLine();
            Regex parser = new Regex(@"\[(\d\d\d\d-\d\d-\d\d\s\d\d:\d\d)\]\s(.+)");
            Regex guardParser = new Regex(@".+#(\d+)");
            List<TimelineEvent> events = new List<TimelineEvent>();
            using (StreamReader sr = new StreamReader(inputFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var match = parser.Match(line);
                    if (!match.Success)
                        Console.WriteLine("Did not understand line " + line);

                    var dateStr = match.Groups[1].Value;
                    var gudardDate = DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm", null);

                    var details = match.Groups[2].Value;
                    EventType et = EventType.FallAsleep;
                    int id = -1;
                    if (details.Contains("wakes"))
                        et = EventType.WakeUp;
                    else if (details.Contains("asleep"))
                        et = EventType.FallAsleep;
                    else if(details.Contains("shift"))
                    {
                        et = EventType.StartShift;
                        var guardDetails = guardParser.Match(details);
                        if (!guardDetails.Success)
                            Console.WriteLine("Did not understand guard details: " + details);
                        id = int.Parse(guardDetails.Groups[1].Value);

                    }
                    events.Add(new TimelineEvent { Time = gudardDate, GuardId = id, Type = et });
                }
            }
            events.Sort((a, b) => { return  a.Time.CompareTo(b.Time); });
            int currentGuard = -1;
            SleepTime currentSleep = null;
            Dictionary<int, List<SleepTime>> guardSleepPatterns = new Dictionary<int, List<SleepTime>>();
            foreach(var e in events)
            {
                switch (e.Type)
                {
                    case EventType.StartShift:
                        currentGuard = e.GuardId;
                        currentSleep = null;
                        break;
                    case EventType.FallAsleep:
                        if (currentSleep != null)
                            Console.WriteLine("Sleep not null");
                        currentSleep = new SleepTime() { start = e.Time };
                        break;
                    case EventType.WakeUp:
                        if (currentSleep == null)
                            Console.WriteLine("Sleep is null");
                        currentSleep.finish = e.Time;
                        if (!guardSleepPatterns.ContainsKey(currentGuard))
                            guardSleepPatterns.Add(currentGuard, new List<SleepTime>());
                        guardSleepPatterns[currentGuard].Add(currentSleep);
                        currentSleep = null;
                        break;
                }
            }

            var mostAsleep = guardSleepPatterns.OrderByDescending(t => t.Value.Sum(f => f.GetDuration())).First();
            Console.WriteLine("Most asleep: " + mostAsleep.Key);
            var bestMinute = WorkOutMostSleptMinute(mostAsleep.Value, out int noTimes);
            Console.WriteLine("Best minute: " + bestMinute);
            Console.WriteLine("Answer 1: " + (bestMinute * mostAsleep.Key));
            KeyValuePair<int, List<SleepTime>> bestGuard;
            var bestTimes = 0;
            bestMinute = -1;
            foreach (var guard in guardSleepPatterns)
            {
                var tempMinute  = WorkOutMostSleptMinute(guard.Value, out noTimes);
                if(noTimes > bestTimes)
                {
                    bestTimes = noTimes;
                    bestGuard = guard;
                    bestMinute = tempMinute;
                }
            }
            Console.WriteLine("Most regular guard: " + bestGuard.Key + " Slept: " + bestTimes + " times at minute " + bestMinute);
            Console.WriteLine("Answer2: " + (bestMinute * bestGuard.Key));

            Console.Read();
        }

        static int WorkOutMostSleptMinute(List<SleepTime> sleeptimes, out int noTimes)
        {
            var minuteByMinute = new int[60];
            foreach (var asleep in sleeptimes)
            {
                for (int i = asleep.start.Minute; i < asleep.finish.Minute; ++i)
                {
                    minuteByMinute[i]++;
                }
            }
            int bestMinuteVal = 0;
            int bestMinute = -1;
            for (int i = 0; i < 60; ++i)
            {
                if (minuteByMinute[i] > bestMinuteVal)
                {
                    bestMinute = i;
                    bestMinuteVal = minuteByMinute[i];
                }
            }
            noTimes = bestMinuteVal;
            return bestMinute;
        }

    }
}
