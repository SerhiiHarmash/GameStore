using System;

namespace GameStore.Services.Services
{
    public abstract class BaseService
    {
        protected void CheckForNull(object objectForCheck, string errorMessage)
        {
            if (objectForCheck == null)
            {
                throw new Exception(errorMessage);
            }
        }

        protected void CheckStringIsNullOrWhiteSpace(string str, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new Exception(errorMessage);
            }
        }
    }
}
