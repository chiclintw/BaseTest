using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BaseTest
{
	public class Customer
	{
		static Customer()
		{
			Data = new ObservableCollection<Customer>()
			{
				new Customer {Name="樂支電器有限公司", Telphone="02-78562510", Address="台北市重慶北路二段17號"},
				new Customer {Name="權力電子有限公司", Telphone="02-23659898", Address="台北市武昌街2段55號"},
				new Customer {Name="創泰電子股份有限公司", Telphone="02-26598985", Address="台北市漢中街30號"},
				new Customer {Name="國照股份有限公司", Telphone="02-23689536", Address="台北市成都路3號"},
				new Customer {Name="星際物流有限公司", Telphone="02-23659856", Address="台北市長安西路244號"}
			};
		}
		public string Name { get; set; }
		public string Telphone { get; set; }
		public string Address { get; set; }
		public static IList<Customer> Data { set; get; }
	}
}

