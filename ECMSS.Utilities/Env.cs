﻿namespace ECMSS.Utilities
{
    public static class Env
    {
#if DEBUG
        public const bool IS_DEVELOPMENT = true;
#else
        public const bool IS_DEVELOPMENT = false;
#endif
    }
}