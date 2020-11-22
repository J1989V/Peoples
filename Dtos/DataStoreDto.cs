using Peoples.Enums;

namespace Peoples.Dtos
{
	public class DataStoreDto
	{
		public string DataStoreName { get; set; }

		public string DataStoreType { get; set; }

		public string DataStoreLocation { get; set; }

		public string TableTabName { get; set; }

		public string FieldName { get; set; }

		public int FieldRow { get; set; }

		public Categories Category { get; set; }
	}
}