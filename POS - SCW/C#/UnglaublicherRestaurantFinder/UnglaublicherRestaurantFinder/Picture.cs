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
	[Table("pictures")]
	public class Picture
	{
		[Column("ImageID", IsPrimaryKey = true, IsIdentity = true, SkipOnInsert = true, SkipOnUpdate = true)] public long    ImageId { get; set; } // integer
		[Column("Path"                                                                                     )] public string? Path    { get; set; } // text(max)
		[Column("RestID"                                                                                   )] public long?   RestId  { get; set; } // integer
	}
}
