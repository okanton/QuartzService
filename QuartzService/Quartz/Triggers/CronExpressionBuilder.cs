using QuartzServiceClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuartzService.Quartz.Triggers
{
    public sealed class CronExpressionBuilder
    {
        private readonly IEnumerable<PeriodTimeTypes> periods;
        private PeriodTimeTypes repeatPeriod = PeriodTimeTypes.Ежечасно;
        private DateTime date = DateTime.Now;
        private int repeatCount = 1;

        public CronExpressionBuilder()
        {
            periods = Extentions.EnumToList<PeriodTimeTypes>();
        }

        public CronExpressionBuilder StartDate(DateTime? date)
        {
            if (date is not null) this.date = date.Value;
            return this;
        }

        public CronExpressionBuilder RepeatPeriod(PeriodTimeTypes repeatPeriod)
        {
            this.repeatPeriod = repeatPeriod;
            return this;
        }

        public CronExpressionBuilder RepeatCount(int repeatCount)
        {
            this.repeatCount = repeatCount;
            return this;
        }

        public string Build()
        {
            var template = periods.FirstOrDefault(p => p == repeatPeriod).GetStringTemplate();
            return GetCronExpression(template, date.Minute, date.Hour, date.Day, date.Month, repeatCount, date.GetShortWeekDayName());
        }

        private static string GetCronExpression(string cronTemplate, int minute, int hour, int day, int month, int count, string dayName) =>
            string.Format(cronTemplate, minute, hour, day, month, count, dayName);
    }
}