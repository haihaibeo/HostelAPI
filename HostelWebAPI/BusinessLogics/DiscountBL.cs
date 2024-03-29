using System;

namespace HostelWebAPI.BL
{
    public interface IDiscountBL
    {
        int CalculateDiscountPercent(DateTime from, DateTime to);
        int CalculateDiscountPercent(int nightCount);
    }

    public class DiscountBL : IDiscountBL
    {
        public int CalculateDiscountPercent(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public int CalculateDiscountPercent(int nightCount)
        {
            const int GET_10_PERCENT = 7;
            if (nightCount >= GET_10_PERCENT) return 10;

            return 0;
        }
    }
}