using System.Diagnostics;

Console.WriteLine("Hello, World!");
Console.WriteLine();

# region Run Void Task
// Running a task without a return value
await Task.Run( () => TaskAfterTime("voidTask", logInside:true, consoleLine:"Executing a task without a return value."));
Console.WriteLine();
Console.WriteLine();
#endregion


#region Run Task With Return
// Starting a stopwatch to measure elapsed time
Stopwatch stopwatch = Stopwatch.StartNew();
// Running a task with a return value
string result = await Task.Run<string>(()=>TaskAfterTime("Returning a value from a task."));
string result1 = await TaskAfterTime("Returning a value from a task.");
// Introducing a delay of 500 milliseconds
await Task.Delay(500);
// Stopping the stopwatch
stopwatch.Stop();
// Getting the elapsed time
TimeSpan ts = stopwatch.Elapsed;

// Formatting the elapsed time
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
    ts.Hours, ts.Minutes, ts.Seconds,
    ts.Milliseconds / 10);

// Printing the result and elapsed time to the console
Console.WriteLine($"{result} Elapsed Time : {elapsedTime}");
Console.WriteLine();
Console.WriteLine();
#endregion


#region Run Tasks And Wait All
// Creating tasks and running them all at once
Task<string> task1All = TaskAfterTime("task1All", 0,true, consoleLine: $"the {nameof(task1All)} already completed.");
Task<string> task2All = TaskAfterTime("task2All", 5);
Task<string>[] tasksAll = [ task1All, task2All ];
string[] resultsAll = await Task.WhenAll(tasksAll);

// Printing the results of all tasks
foreach (string taskResult in resultsAll)
{
    Console.WriteLine( taskResult);
}

Console.WriteLine();
Console.WriteLine();
#endregion


#region Run Tasks Wait First To Complete
// Creating tasks and running them until the first one completes
Task<string> task1Any = TaskAfterTime("task1Any",1000);
Task<string> task2Any = TaskAfterTime("task2Any",4);
var completedTask = await Task.WhenAny<string>(task1Any, task2Any);
Console.WriteLine($"First completed task: {completedTask.Result}");
Console.WriteLine();
Console.WriteLine();
#endregion


#region Run Tasks And Wait Each Separately And Execute Something For Each One
// Creating tasks and running them one by one
Task<string> task1Each = TaskAfterTime("task1Each");
Task<string> task2Each = TaskAfterTime("task2Each", 4);
Task<string> task3Each = TaskAfterTime("task3Each", 3);

IAsyncEnumerable<Task<string>> allTasks = Task.WhenEach<string>(task1Each, task2Each, task3Each);
await foreach (var taskResult in allTasks)
{
    Console.WriteLine(await taskResult);
}
#endregion


#region Create Task Method
// Defining a method that runs a task after a certain delay
static async Task<string> TaskAfterTime(string text, int multiplyDelay = 1, bool logInside = false, string consoleLine = "")
{
    var taskDelay = multiplyDelay*500;
    await  Task.Delay(taskDelay);
    if (logInside)
    {
        Console.WriteLine($"Task {text} conclude after {taskDelay} milliseconds.");
        Console.WriteLine($"{consoleLine}");
    }
    return $"Task {text} conclude after {taskDelay} milliseconds.";
}
#endregion