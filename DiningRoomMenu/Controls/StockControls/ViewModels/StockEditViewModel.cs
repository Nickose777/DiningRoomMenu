using DiningRoomMenu.Logic.DTO.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.StockControls.ViewModels
{
    public class StockEditViewModel : ObservableObject
    {
        private readonly StockEditDTO stock;

        public StockEditViewModel(StockEditDTO stock)
        {
            this.stock = stock;

            this.IngredientCount = new ObservableCollection<IngredientCount>(stock.IngredientCount);
        }

        public ObservableCollection<IngredientCount> IngredientCount { get; set; }
    }
}
