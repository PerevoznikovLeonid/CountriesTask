using System;
using Dapper;

namespace CountriesTask;

public class IntToBoolHandler : SqlMapper.TypeHandler<bool>
{
    public override void SetValue(System.Data.IDbDataParameter parameter, bool value) 
        => parameter.Value = value ? 1 : 0;

    public override bool Parse(object value) 
        => Convert.ToInt64(value) != 0;
}