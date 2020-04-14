// <copyright file="FraudRadarTests.cs" company="Payvision">
// Copyright (c) Payvision. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {

        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Where(x => !x.IsFraudulent).Should().HaveCount(1, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "TwoLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(2, "The result should contains the number of lines of the file");
            result.ElementAt(1).IsFraudulent.Should().BeTrue("The second line is fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "ThreeLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(3, "The result should contains the number of lines of the file");
            result.ElementAt(1).IsFraudulent.Should().BeTrue("The second line is fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "FourLines_MoreThanOneFraudulent.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Should().HaveCount(4, "The result should contains the number of lines of the file");
            result.Where(x => x.IsFraudulent).Should().HaveCountGreaterOrEqualTo(2, "The result should contains 2 or more than one fraudulent");
        }

        private static List<FraudResult> ExecuteTest(string filePath)
        {
            var file = new FraudFileProcessor();
            file.ReadFraudFile(filePath);

            var fraudRadar = new FraudRadar();

            return fraudRadar.Check(file.FraudLines).ToList();
        }
    }
}