﻿namespace Host.Auxiliary
{
    public interface IConfigurationHelper
    {
        Dictionary<string, string> Configuration { get; }
        string GetSqlServerName { get; }
    }
}