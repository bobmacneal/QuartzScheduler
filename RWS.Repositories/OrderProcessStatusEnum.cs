﻿namespace RWS.Repositories
{
    public enum OrderProcessStatusEnum
    {
        Initial = 1,
        Ready = 2,
        Processing = 3,
        Complete = 4,
        Retry = 5,
        Error = 6
    };
}