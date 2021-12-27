using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace BaseTest
{
	public class Pizza
	{
		static Pizza()
		{
			Data = new ObservableCollection<Pizza>()
			{
				new Pizza {Name="珍品海陸", Icon="pizza1.png", Price="小：440元，大：680元",Note="蕃茄培根醬、美乃滋、帆立貝、貴妃鮑、胡桃木煙燻楓糖培根、洋蔥、紅椒、洋香菜葉",Order=false},
				new Pizza {Name="法式蝦蟹鮑魚菇", Icon="pizza2.png", Price="小：480元，大：720元",Note="法蘭西海鮮白醬、鮮蝦、蟹味棒、洋蔥、巴西里葉、鮑魚菇",Order=false},
				new Pizza {Name="炙燒松阪", Icon="pizza3.png", Price="小：430元，大：650元",Note="松阪豬、培根片、杏鮑菇、洋蔥、紅椒",Order=false},
				new Pizza {Name="黃金和風雞", Icon="pizza4.png", Price="小：420元，大：630元",Note="炸雞塊、美乃滋、培根片、海苔粉、鳳梨、洋蔥、黃瓜",Order=false}
			};
		}
		public string Name { get; set; }
		public string Icon { get; set; }
		public string Price { get; set; }
		public string Note { get; set; }
		public bool Order { get; set; }
		public static IList<Pizza> Data { set; get; }
	}
}

