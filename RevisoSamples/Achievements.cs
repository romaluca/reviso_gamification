using RevisoSamples.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RevisoSamples
{
    public static class Achievements
    {
        public static AchievementLevel ElaborateCustomerAchievement(IList<Customer> customer)
        {
            if (customer == null || !customer.Any())
                return AchievementLevel.Zero;
            if(customer.Count <= 10) // At least one customer!
                return AchievementLevel.One;
            return customer.Count <= 20 ? AchievementLevel.Two : AchievementLevel.Three;
        }

        public static AchievementLevel ElaborateTotalPaied2018Invoice(IList<Invoice> invoice)
        {
            if (invoice == null || !invoice.Any())
                return AchievementLevel.Zero;
            IEnumerable<Invoice> filtered = invoice.Where(x => !String.IsNullOrEmpty(x.date) && DateTime.Parse(x.date) >= new DateTime(2018, 1, 1));
            double sum = filtered.Sum(x => x.netAmount);
            return sum >= 4000 ? AchievementLevel.One : AchievementLevel.Zero;
        }

        public static AchievementLevel ElaborateTotalUnpaied2018Invoice(IList<Invoice> invoice)
        {
            if (invoice == null || !invoice.Any())
                return AchievementLevel.Zero;
            IEnumerable<Invoice> filtered = invoice.Where(x => !String.IsNullOrEmpty(x.date) && DateTime.Parse(x.date) >= new DateTime(2018, 1, 1));
            double sum = filtered.Sum(x => x.netAmount);
            if (sum < 1000) // 0 < sum < 5000
                return AchievementLevel.Zero;
            if (sum < 5000)
                return AchievementLevel.One;
            return sum < 10000 ? AchievementLevel.Two : AchievementLevel.Three;
        }
    }

    public enum AchievementLevel
    {
        [Description("< >  < >  < >")]
        Zero = 0,
        [Description("<#>  < >  < >")]
        One = 1,
        [Description("<#>  <#>  < >")]
        Two = 2,
        [Description("<#>  <#>  <#>")]
        Three = 3
    }
}