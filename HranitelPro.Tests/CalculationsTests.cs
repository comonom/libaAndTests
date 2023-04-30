using SF2022User08Lib;
using System;
using Xunit;

namespace HranitelPro.Tests
{
    public class CalculationsTests
    {
        [Fact]
        public void GetAvailables_ShouldReturnCorrectResult()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calculations = new Calculations();

            //Act
            var result = calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime);

            //Result
            var excepted = new string[]
            {
                "08:00-08:30",
                "08:30-09:00",
                "09:00-09:30",
                "09:30-10:00",
                "11:30-12:00",
                "12:00-12:30",
                "12:30-13:00",
                "13:00-13:30",
                "13:30-14:00",
                "14:00-14:30",
                "14:30-15:00",
                "15:40-16:10",
                "16:10-16:40",
                "17:30-18:00"
            };
            Assert.Equal(excepted, result);
        }

        [Fact]
        public void GetAvailables_BadCountOfDurationsAndStartTimes_ThrowsException()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calculations = new Calculations();

            //Act and Assert
            Assert.Throws<ArgumentException>(
                () => calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime));
        }

        [Fact]
        public void GetAvailables_BeginWorkTimeBiggerThanEndWorkTime_ThrowsException()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan end = new TimeSpan(8, 0, 0);
            TimeSpan start = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            Calculations calculations = new Calculations();

            //Act and Assert
            Assert.Throws<ArgumentException>(
                () => calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime));
        }

        [Fact]
        public void GetAvailables_ConsultationTimesLessOrEqualThanZero_ThrowsException()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan start = new TimeSpan(8, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);
            int consultationTime = -1;
            Calculations calculations = new Calculations();

            //Act and Assert
            Assert.Throws<ArgumentException>(
                () => calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime));
        }

        [Fact]
        public void GetAvailables_TimeSpanHaveDays_ThrowsException()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(1, 10, 0, 0),
                new TimeSpan(11, 0, 0),
                new TimeSpan(15, 0, 0),
                new TimeSpan(15, 30, 0),
                new TimeSpan(16, 50, 0),
            };

            var busyDurations = new int[]
            {
                60, 30, 10, 10, 40
            };

            TimeSpan end = new TimeSpan(18, 0, 0);
            TimeSpan start = new TimeSpan(8, 0, 0);
            int consultationTime = 30;
            Calculations calculations = new Calculations();

            //Act and Assert
            Assert.Throws<ArgumentException>(
                () => calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime));
        }

        [Fact]
        public void GetAvailables_FullDayBusy_ReturnsEmptyList()
        {
            //Arrange
            var busySpans = new TimeSpan[]
            {
                new TimeSpan(8, 0, 0),
            };

            var busyDurations = new int[]
            {
                60
            };

            TimeSpan end = new TimeSpan(9, 0, 0);
            TimeSpan start = new TimeSpan(8, 0, 0);
            int consultationTime = 30;
            Calculations calculations = new Calculations();

            //Act
            var result = calculations.AvailablePeriods(busySpans, busyDurations, start, end, consultationTime);

            //Act and Assert
            Assert.Empty(result);
        }
    }
}
