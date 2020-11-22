using System.Collections.Generic;
using Peoples.Enums;

namespace Peoples.Dtos
{
	public class DataSourceData
	{
		public DataSourceData( )
		{
			ColumnsNames = new List<string>( );
			FieldRows = new List<FieldRow>( );
		}

		public string DataStoreName { get; set; }

		public string DataStoreType { get; set; }

		public string DataStoreLocation { get; set; }

		public string DataStoreTabName { get; set; }

		public List<string> ColumnsNames { get; set; }

		public List<FieldRow> FieldRows { get; set; }
	}

	public class FieldRow
	{
		public FieldRow( )
		{
			Fields = new List<Field>( );
		}

		public List<Field> Fields { get; set; }
	}

	public class Field
	{
		public string Value { get; set; }

		public string Column { get; set; }

		public int Row { get; set; }

		public Categories Category { get; set; }

		public bool Approved { get; set; }
	}
}