﻿using Utils;

ConsoleUtils.RunProgram(
    title: "--- Day 4: Scratchcards ---",
    inputFilePath =>
    {
        double totalPoints = 0;
        var cardCountByCardNumber = new Dictionary<int, int>();

        var currentCardNumber = 0;
        foreach (var line in File.ReadLines(inputFilePath))
        {
            currentCardNumber++;

            var splitLine = line.Split(new char[] { ':', '|' }, count: 3, StringSplitOptions.TrimEntries);
            var winningNumbers = splitLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numbersWeHave = splitLine[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var matchingNumberCount = winningNumbers.Intersect(numbersWeHave).Count();

            // Part One
            if (matchingNumberCount > 0)
            {
                totalPoints += Math.Pow(2, matchingNumberCount - 1);
            }

            // Part Two
            for (int i = 0; i <= matchingNumberCount; i++)
            {
                var countToAdd = i == 0 ? 1 : cardCountByCardNumber[currentCardNumber];
                if (!cardCountByCardNumber.TryAdd(currentCardNumber + i, countToAdd))
                {
                    cardCountByCardNumber[currentCardNumber + i] += countToAdd;
                }
            }
        }

        return $"Step One = {totalPoints}; Step Two = {cardCountByCardNumber.Values.Sum()};";
    });