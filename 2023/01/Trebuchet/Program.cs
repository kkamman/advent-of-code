using Trebuchet;
using Utils;

ConsoleUtils.RunProgram(
    title: "--- Day 1: Trebuchet?! ---",
    inputFilePath => new CalibrationDocumentParser(ConsoleUtils.QueryBoolean("Use spelled out digits?"))
        .ParseDocument(File.ReadLines(inputFilePath))
        .Result.ToString());