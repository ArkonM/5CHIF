// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB.Mapping;

#pragma warning disable 1573, 1591
#nullable enable

namespace DataModel
{
	[Table("Router")]
	public class Router
	{
		[Column("ID"     , IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long    Id      { get; set; } // integer
		[Column("Country"                                                                                  )] public string? Country { get; set; } // text(max)
		[Column("Region"                                                                                   )] public string? Region  { get; set; } // text(max)
		[Column("City"                                                                                     )] public string? City    { get; set; } // text(max)
	}
}
