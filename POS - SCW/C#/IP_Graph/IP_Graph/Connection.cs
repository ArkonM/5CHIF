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
	[Table("Connection")]
	public class Connection
	{
		[Column("ID"              , IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long   Id               { get; set; } // integer
		[Column("Endpoint1"                                                                                         )] public long   Endpoint1        { get; set; } // integer
		[Column("Endpoint2"                                                                                         )] public long   Endpoint2        { get; set; } // integer
		[Column("TransmissionTime"                                                                                  )] public double TransmissionTime { get; set; } // real
	}
}