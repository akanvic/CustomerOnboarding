using System;

namespace Onboarding.Service.Helper
{
    public static class OTP
    {
        internal static string GenerateOtp()
        {
            string numbers = "0123456789";
            Random random = new Random();
            string otp = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                int tempval = random.Next(0, numbers.Length);
                otp += tempval;
            }
            return otp;
        }
    }
}
