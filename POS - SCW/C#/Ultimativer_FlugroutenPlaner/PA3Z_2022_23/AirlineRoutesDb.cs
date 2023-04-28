// ---------------------------------------------------------------------------------------------------
// <auto-generated>
// This code was generated by LinqToDB scaffolding tool (https://github.com/linq2db/linq2db).
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
// ---------------------------------------------------------------------------------------------------

using LinqToDB;
using LinqToDB.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable 1573, 1591
#nullable enable

namespace DataModel
{
	public partial class AirlineRoutesDb : DataConnection
	{
		public AirlineRoutesDb()
		{
			InitDataContext();
		}

		public AirlineRoutesDb(string configuration)
			: base(configuration)
		{
			InitDataContext();
		}

        public AirlineRoutesDb(string v, string configuration)
            : base(v, configuration)
        {
            InitDataContext();
        }

        public AirlineRoutesDb(DataOptions<AirlineRoutesDb> options)
			: base(options.Options)
		{
			InitDataContext();
		}

		partial void InitDataContext();

		public ITable<Airline>  Airlines  => this.GetTable<Airline>();
		public ITable<Airport>  Airports  => this.GetTable<Airport>();
		public ITable<Route>    Routes    => this.GetTable<Route>();
		public ITable<Alliance> Alliances => this.GetTable<Alliance>();
	}

	public static partial class ExtensionMethods
	{
		#region Table Extensions
		public static Airline? Find(this ITable<Airline> table, long id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static Task<Airline?> FindAsync(this ITable<Airline> table, long id, CancellationToken cancellationToken = default)
		{
			return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		}

		public static Airport? Find(this ITable<Airport> table, long id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static Task<Airport?> FindAsync(this ITable<Airport> table, long id, CancellationToken cancellationToken = default)
		{
			return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		}

		public static Route? Find(this ITable<Route> table, long id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static Task<Route?> FindAsync(this ITable<Route> table, long id, CancellationToken cancellationToken = default)
		{
			return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		}

		public static Alliance? Find(this ITable<Alliance> table, long id)
		{
			return table.FirstOrDefault(e => e.Id == id);
		}

		public static Task<Alliance?> FindAsync(this ITable<Alliance> table, long id, CancellationToken cancellationToken = default)
		{
			return table.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
		}
		#endregion
	}
}
